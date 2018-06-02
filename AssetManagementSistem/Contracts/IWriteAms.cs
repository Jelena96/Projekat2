using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IWriteAms
    {
        [OperationContract]
        bool WriteAMSxml2(LocalDeviceClass s);
        [OperationContract]
        bool ReadXML();
        [OperationContract]
        bool WriteAMSxml(string s);
    }
}
