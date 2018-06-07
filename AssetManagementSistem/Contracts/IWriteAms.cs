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
        void WriteAMSxml2(LocalDeviceClass s);
        [OperationContract]
        void ReadXML(string p,int id);
        [OperationContract]
        void WriteAMSxml(string s,int id, DateTime vreme);
    }
}
