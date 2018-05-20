using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LocalDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new NetTcpBinding();
            binding.TransactionFlow = true;
            ChannelFactory<ILocalControler> factory = new ChannelFactory<ILocalControler>(binding, new EndpointAddress(String.Format("net.tcp://localhost:6767/LocalControlerClass")));
            ILocalControler proksi = factory.CreateChannel();
            proksi.Ispis1();

            Console.ReadLine();
        }
    }
}
