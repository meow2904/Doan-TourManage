using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.IServices;
using TourManagement.Business.Services;

namespace ConsoleApp1
{
    class @Program
    {
        private static IDestinatioRepository destinatioRepository= new DestinationRepository();
        private static ITourRepository tourRepository= new TourRepository();
        private static IEmployeeRepository hotelRepository= new EmployeeRepository();
        static void Main(string[] args)
        {

            var e = tourRepository.GetById(1);

            Console.WriteLine(e.TimeStart.Value.Date.ToString("MM/dd/yyyy"));

            Console.ReadKey();

        }
    }
}
