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
        
        long TimeStamp { get; set; }

        //[OperationContract]
        //bool ReadXML();
        //[OperationContract]
        //bool WriteAMSxml(string s);
       
        [OperationContract]
        void Procitaj(LocalDeviceClass P);
        [OperationContract]
        bool CreateXML(string s, Dictionary<int, List<LocalDeviceClass>> d , LocalDeviceClass l);
    }
}
