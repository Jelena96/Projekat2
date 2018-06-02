using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LocalDevice
{
    [DataContract]
    public class LocalDeviceClass1 
    {
        //[DataMember]
        //public int localDeviceCode;
        //public int LocalDeviceCode {

        //    get {

        //        return localDeviceCode;

        //    }
        //    set {

        //        localDeviceCode = value;
        //    }
        //}
        //[DataMember]
        //public int idControler;

        //public int IdControler
        //{

        //    get
        //    {

        //        return idControler;

        //    }
        //    set
        //    {

        //        idControler = value;
        //    }
        //}
        //[DataMember]
        //public DateTime timestamp;

        //public DateTime TimeStamp
        //{

        //    get
        //    {

        //        return timestamp;

        //    }
        //    set
        //    {

        //        timestamp = value;
        //    }
        //}
        //[DataMember]
        //public int analogActualValue;

        //public int AnalogActualValue
        //{

        //    get
        //    {

        //        return analogActualValue;

        //    }
        //    set
        //    {

        //        analogActualValue = value;
        //    }
        //}
        //[DataMember]
        //public DeviceEnum actualValue;
        //public DeviceEnum ActualValue
        //{

        //    get
        //    {

        //        return actualValue;

        //    }
        //    set
        //    {

        //        actualValue = value;
        //    }
        //}
        //[DataMember]

        //public string deviceType;
        //public string DeviceType
        //{

        //    get
        //    {

        //        return deviceType;

        //    }
        //    set
        //    {

        //        deviceType = value;
        //    }
        //}
        //[DataMember]

        //public string sendTo;
        //public string SendTo
        //{

        //    get
        //    {

        //        return sendTo;

        //    }
        //    set
        //    {

        //        sendTo = value;
        //    }
        //}
        //[DataMember]
        //public DeviceEnum actualState;
        //public DeviceEnum ActualState
        //{

        //    get
        //    {

        //        return actualState;

        //    }
        //    set
        //    {

        //        actualState = value;
        //    }
        //}

        



        public LocalDeviceClass1() { }




        private static Random ran = new Random();
        /* private static DeviceEnum GetType3()
         {
             return (DeviceEnum)(ran.Next(0, 3));

         }*/
        private static int GetType2()
        {

            return ran.Next(1, 10);
        }
        private static int GetType1()
        {

            return ran.Next(0, 2);  //Ako zelimo broj 0 ili 1 onda ide random(0,2)
        }
        //  public AnalogDeviceEnum AnalogDevice { get; set; }

      

        


       




    }
}

