using System;
using System.Collections.Generic;
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

            var e = hotelRepository.GetById(1010);

            Console.WriteLine(e.BirthDate);
            //Console.WriteLine(e.Name);

            
        }

        private static void swap( int x, int y)
        {
            int tem =x;
            x = y;
            y = tem;
        }
    }

    class @Var
    {

    }
}
