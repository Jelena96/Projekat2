using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LocalDevice;
using System.IO;
using System.Xml;
using System.Windows.Controls;

namespace LocalControler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("u kontroleru sam");

            LocalControlerClass lc = new LocalControlerClass();

            
            
            
            OpenServer();
            Console.WriteLine("Da li zelite da saljete AMS-u?");
            string unos = Console.ReadLine();
         

            if (unos == "da")
            {

                bool uspesno = lc.ReadXML();
                lc.DeleteControllers();
            }

            Console.ReadLine();
            
        }

        static void OpenServer() {

            ServiceHost svc = new ServiceHost(typeof(LocalControlerClass));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(ILocalControler), binding, new Uri(String.Format("net.tcp://localhost:10100/LocalControlerClass")));
            svc.Open();

        }
    }
}
