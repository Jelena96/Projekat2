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

        long Timestamp { get; }
        string ActualValue { get; set; }
        [OperationContract]
        void Ispis();
    }
}
