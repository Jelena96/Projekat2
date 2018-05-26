using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LocalDevice
{
    public class LocalDeviceClass : ILocalDevice
    {
        public int LocalDeviceCode { get; set; }
        public int IdControler { get; set; }
        public DateTime Timestamp { get; set; }
        public int AnalogActualValue { get; set; }
        public DeviceEnum ActualValue { get; set; }

        public string DeviceType { get; set; }



        public string SendTo { get; set; }

        public LocalDeviceClass()
        {

        }

        private static Random ran = new Random();
        private static DeviceEnum GetType3()
        {
            return (DeviceEnum)(ran.Next(0, 3));

        }



        private static int GetType2()
        {

            return ran.Next(0, 10);
        }

        //  public AnalogDeviceEnum AnalogDevice { get; set; }

        public bool CreateXML(string p1)
        {
            int j = 0;
            bool u = false;
            using (XmlWriter writer = XmlWriter.Create(p1))
            {

              

                    foreach (KeyValuePair<int, List<LocalDeviceClass>> item in Program.DeviceDic)
                    {
                        if (item.Key == Program.l.IdControler)
                        {
                       
                            u = true;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Devices");

                        while (j != 3) {

                            foreach (LocalDeviceClass device in item.Value)
                            {


                                writer.WriteStartElement("Device");
                                Enum e = device.ActualValue;

                                //Enum.GetName(typeof(DeviceEnum), e);

                                writer.WriteElementString("ID", device.LocalDeviceCode.ToString());
                                if (device.DeviceType == "D")
                                {
                                    writer.WriteElementString("ActualValue", GetType3().ToString());
                                }
                                writer.WriteElementString("Type", device.DeviceType);
                                writer.WriteElementString("TimeStamp", DateTime.Now.ToString());
                                writer.WriteElementString("SendTo", device.SendTo);
                                if (device.DeviceType == "A")
                                {
                                    writer.WriteElementString("AnalogMeasurment", GetType2().ToString());
                                }
                                writer.WriteEndElement();

                               
                            }
                            j++;
                        }
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }


                    }

                    
               

                u = false;
                string[] p;
                string ime = null;
                string folder = @"..\..\..\Kontroleri";
                string[] files = Directory.GetFiles(folder, "*.xml");

                if (files.Length == 0)
                {
                    Console.WriteLine("Ne postoji xml file");

                }
                else
                {
                  

                    p = files[0].Split('\\');
                    ime = p[4].Substring(0, p[4].Length - 4);
                    Console.WriteLine("Postoje fajlovi{0} ", ime);

                }
                return u;
            }


        }
    }
}
