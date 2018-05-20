using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LocalDevice
{
    public class LocalDeviceClass : ILocalDevice
    {
        public int LocalDeviceCode { get; set; }

        public DateTime Timestamp { get; set; }

        public DeviceEnum ActualValue { get; set; }

        public string DeviceType { get; set; }

        public string SendTo { get; set; }
      //  public AnalogDeviceEnum AnalogDevice { get; set; }
        
    }
}
