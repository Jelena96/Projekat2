using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LocalControler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hehe");
            ServiceHost svc = new ServiceHost(typeof(LocalControlerClass));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;
            
            svc.AddServiceEndpoint(typeof(ILocalControler), binding, new Uri(String.Format("net.tcp://localhost:6767/LocalControlerClass")));
            svc.Open();

            Console.ReadLine();
            
        }
    }
}
