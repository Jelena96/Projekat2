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
       
        public static int promene;
        public LocalDeviceClass1() { }

        private static Random ran = new Random();
        
        private static int GetType2()
        {

            return ran.Next(1, 10);
        }
        private static int GetType1()
        {

            return ran.Next(0, 2);  //Ako zelimo broj 0 ili 1 onda ide random(0,2)
        }
        


        public void PromenaStanja(int id,int idk,string type, LocalDeviceClass l,string promena) {




        }
        


       




    }
}

