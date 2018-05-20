using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace LocalDevice
{
    public class LocalDeviceClass : ILocalDevice
    {
        public int LocalDeviceCode { get; set; }

        public long Timestamp { get; set; }

        public string ActualValue { get; set; }

        public void Ispis()
        {
            Console.WriteLine("ZIV SAM I ZOVEM SE LOCAL DEVICE.");
        }
    }
}
