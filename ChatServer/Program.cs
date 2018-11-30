using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {

        const string Server_URL = "http://localhost:8080";


        static void Main(string[] args)
        {
            Console.WriteLine("Drücke Taste zum Starten des Servers: ");
            Console.ReadKey();

            try
            {
                using (IDisposable server = WebApp.Start<Startup>(Server_URL))
                {
                    Console.ReadKey();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
           
           
            Console.ReadKey();
        }
    }
}
