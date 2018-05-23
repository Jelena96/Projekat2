using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public DeviceEnum ActualValue { get; set; }

        public string DeviceType { get; set; }

     
        
        public string SendTo { get; set; }

        public LocalDeviceClass()
        {

        }
        //  public AnalogDeviceEnum AnalogDevice { get; set; }
      
        public bool CreateXML(string p1)
        {
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
                        foreach (LocalDeviceClass device in item.Value)
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
