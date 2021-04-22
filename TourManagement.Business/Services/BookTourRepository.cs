using System;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class BookTourRepository : IBookTourRepository
    {
        private readonly IGenericRepository<OrderTour> _orderTourRepository;
        private readonly IGenericRepository<OrderTourDetail> _orderTourDetailRepository;
        private readonly IGenericRepository<Tour> _tourRepository;
        public BookTourRepository(IGenericRepository<OrderTour> orderTourRepository,
            IGenericRepository<OrderTourDetail> orderTourDetailRepository,
            IGenericRepository<Tour> tourRepository)
        {
            _orderTourRepository = orderTourRepository;
            _orderTourDetailRepository = orderTourDetailRepository;
            _tourRepository = tourRepository;
        }
        public void BookTour(OrderTour orderTour, OrderTourDetail orderTourDetail)
        {
            orderTour.OrderDate = DateTime.Now.Date;
            _orderTourRepository.Add(orderTour);

            //update quantity of tour
            var tour = _tourRepository.GetById(orderTourDetail.TourId);
            tour.QuantityPeople -= (orderTourDetail.QuantityAdult + orderTourDetail.QuantityChild);

            _tourRepository.Update(tour);

            orderTourDetail.OrderTour = orderTour;
            _orderTourDetailRepository.Add(orderTourDetail);
        }
    }
}
