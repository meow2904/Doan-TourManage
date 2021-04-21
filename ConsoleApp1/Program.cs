using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.IServices;
using TourManagement.Business.Services;

namespace ConsoleApp1
{
    class Program
    {
        private static IDestinatioRepository destinatioRepository= new DestinationRepository();
        private static ITourRepository tourRepository= new TourRepository();
        private static IUserRepository userRepository= new UserRepository();

        static void Main(string[] args)
        {
            //var rs = tour.GetAll().Where(td => td.Id == 1002);

            //var destinations = destinatioRepository.GetAll().Where(d => rs.Any(x => x. == d.Id));

            //foreach (var item in destinatioRepository.GetDestinationByidTour(1002))
            //{
            //    Console.WriteLine(item.Name +" " );

            //}
            //foreach (var item in rs)
            //{
            //    Console.WriteLine(item.Name);
            //}
            var rs = userRepository.GetUserLogin("Admin@gmail.com","aaaa" );
            Console.WriteLine("a "+ rs.Name + " a");
            Console.ReadLine();
        }
    }
}
