using Contracts;
using LocalControler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSistem
{
    public class AMSClass
    {
         void OpenServer()
        {

            ServiceHost svc = new ServiceHost(typeof(LocalControlerClass));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(ILocalControler), binding, new Uri(String.Format("net.tcp://localhost:10100/LocalControlerClass")));
            svc.Open();

        }

        void OpenServerFromDevice()
        {
            ServiceHost svc = new ServiceHost(typeof(LocalDeviceClass));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(ILocalDevice), binding, new Uri(String.Format("net.tcp://localhost:10100/LocalDeviceClass")));
            svc.Open();

        }
    }
}
