using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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

                    Console.WriteLine("Unesi kome zelis da saljes podatke");
                    string salji = Console.ReadLine();


                    if (type == "A")
                    {

                        l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, Timestamp = DateTime.Now, ActualValue = GetType2() ,SendTo=salji};

                    }

                    if (type == "D")
                    {
                        l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, Timestamp = DateTime.Now, ActualValue = GetType3() ,SendTo=salji};


                    }

                    ld.Add(l);
                }
                else
                {

                    Console.WriteLine("Niste uneli dobar tip uredjaja");
                    //continue;
                }
                }

            
          

            foreach (LocalDeviceClass item in ld)
            {
                /*if (item.SendTo == "LC" && )
                {

                }*/
            }
            using (XmlWriter writer = XmlWriter.Create("controler1.xml"))
            {

                writer.WriteStartDocument();
                writer.WriteStartElement("Devices");


                foreach (LocalDeviceClass device in ld)
                {
                    writer.WriteStartElement("Device");
                    Enum e = device.ActualValue;

                    //Enum.GetName(typeof(DeviceEnum), e);

                    writer.WriteElementString("ID", device.LocalDeviceCode.ToString());
                    writer.WriteElementString("ActualValue", e.ToString());
                    writer.WriteElementString("Type", device.DeviceType);
                    writer.WriteElementString("TimeStamp", device.Timestamp.ToString());
                    writer.WriteElementString("TimeStamp", device.SendTo);



                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string[] p;
            string ime = null;
            string folder = @"..\..\..\Kontroleri";
            string[] files = Directory.GetFiles(folder,"*.xml");

            if (files.Length == 0)
            {
                Console.WriteLine("Ne postoji xml file");

            }
            else {

                p = files[0].Split('\\');
                ime = p[9].Substring(0, p[9].Length - 4);
                Console.WriteLine("Postoje fajlovi",ime);

            }


            Console.ReadLine();
        }
    }
}
