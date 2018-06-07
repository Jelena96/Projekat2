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
        bool ReadXML(string p,int id, DateTime vreme);
        [OperationContract]
        bool WriteAMSxml(string s,int id, DateTime vreme);
    }
}
