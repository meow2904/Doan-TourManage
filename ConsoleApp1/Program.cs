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
        private static IDestinatioRepository destinatioRepository = new DestinationRepository();
        private static ITourRepository tourRepository = new TourRepository();
        private static IEmployeeRepository hotelRepository = new EmployeeRepository();

        static void Main(string[] args)
        {
            //var e = tourRepository.GetById(1);

            //Console.WriteLine(e.TimeStart.Value.Date.ToString("MM/dd/yyyy"));
            //Console.WriteLine();
            try
            {
                int a = 0;
                if(a == 0)
                {
                    throw new Exception("hihih");
                }
            }
            catch (Exception e1)
            {

                 new Exception(e1.Message);
            }

            Console.ReadKey();

        }
    }

    public class Test
    {

        public void ABC(Exception e1)
        {
            throw new Exception(e1.Message);
        }
    }

}
