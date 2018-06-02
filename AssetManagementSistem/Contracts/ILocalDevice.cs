
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Contracts
{
    [ServiceContract]
    public interface ILocalDevice
    {


        int LocalDeviceCode { get; set; }

        DateTime TimeStamp { get; }

        string DeviceType { get; set; }

        DeviceEnum ActualValue { get; set; }
        DeviceEnum ActualState { get; set; }

        string SendTo { get; set; }
        [OperationContract]
        bool CreateXML(string o);
        int AnalogActualValue { get; set; }
        int IdControler { get; set; }
       

    }

    [DataContract]
    public class LocalDeviceClass
    {
        [DataMember]
        public int LocalDeviceCode { get; set; }
        [DataMember]
        public int IdControler { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        public int AnalogActualValue { get; set; }
        [DataMember]
        public DeviceEnum ActualValue { get; set; }
        [DataMember]
        public string DeviceType { get; set; }
        [DataMember]
        public string SendTo { get; set; }
        [DataMember]
        public DeviceEnum ActualState { get; set; }

        
        public LocalDeviceClass()
        {

        }

    }
}
