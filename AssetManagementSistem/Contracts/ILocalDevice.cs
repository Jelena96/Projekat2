
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface ILocalDevice
    {
        int LocalDeviceCode { get; set; }

        DateTime Timestamp { get; }

        string DeviceType { get; set; }

        DeviceEnum ActualValue { get; set; }

        string SendTo { get; set; }

        bool CreateXML(string o);

        
    }
}
