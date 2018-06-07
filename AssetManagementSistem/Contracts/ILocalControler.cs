using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface ILocalControler
    {
  
        int LocalControlerCode { get; set; }
        DateTime TimeStamp { get; set; }       
        [OperationContract]
        void Procitaj(LocalDeviceClass P);
        [OperationContract]
        bool CreateXML(string s, Dictionary<int, List<LocalDeviceClass>> d , LocalDeviceClass l);
    }
}
