using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace LocalDevice
{
    class Program
    {
        //public static IEnumerable<int> Range(int start,int count);
        private static readonly Random ran = new Random();
        private static List<string> aktivniKontroleri = new List<string>();
       
        private static DeviceEnum GetType3()
        {
             return (DeviceEnum)(ran.Next(0,1));

        }

        

        private static DeviceEnum GetType2()
        {
            
            return (DeviceEnum)(ran.Next(2, 3));
        }

        static void Main(string[] args)
        {
            string type="";
            List<LocalDeviceClass> ld = new List<LocalDeviceClass>();
           

                while (type != "E")
                {


                Console.WriteLine("Unesi tip zeljenog uredjaja");
                type = Console.ReadLine();
                if (type == "A" || type == "D")
                {
                    LocalDeviceClass l = new LocalDeviceClass();
                    Console.WriteLine("Unesi id uredjaja");
                    int id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Unesi id zeljenog kontrolera");
                    int idK = int.Parse(Console.ReadLine());


                    if (type == "A")
                    {

                        l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, Timestamp = DateTime.Now, ActualValue = GetType2() };

                    }

                    if (type == "D")
                    {
                        l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, Timestamp = DateTime.Now, ActualValue = GetType3() };


                    }

                    ld.Add(l);
                }
                else
                {

                    Console.WriteLine("Niste uneli dobar tip uredjaja");
                    //continue;
                }
                }
            
           
            

    
                using (XmlWriter writer = XmlWriter.Create("controler1.xml"))
                {
                writer.WriteStartDocument();
                writer.WriteStartElement("Devices");


                foreach (LocalDeviceClass employee in ld)
                {
                    writer.WriteStartElement("Device");
                    Enum e = employee.ActualValue;

                    //Enum.GetName(typeof(DeviceEnum), e);

                    writer.WriteElementString("ID", employee.LocalDeviceCode.ToString());
                    writer.WriteElementString("ActualValue", e.ToString());
                    writer.WriteElementString("Type", employee.DeviceType);
                    writer.WriteElementString("TimeStamp", employee.Timestamp.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }


            Console.ReadLine();
        }
    }
}
