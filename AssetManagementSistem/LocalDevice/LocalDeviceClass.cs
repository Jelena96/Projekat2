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
        public DeviceEnum ActualState { get; set; }
        public static string[] files;
        public LocalDeviceClass()
        {

        }



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

        private static List<string> aktivniKontroleri = new List<string>();

        public bool CreateXML(string p1)
        {
            int j = 0;
            bool u = false;

            if (!CheckXMLFile(p1))
            {
                using (XmlWriter writer = XmlWriter.Create(p1))
                {

                    foreach (KeyValuePair<int, List<LocalDeviceClass>> item in Program.DeviceDic)
                    {
                        if (item.Key == Program.l.IdControler)
                        {

                            u = true;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Devices");

                            while (j != 3)
                            {

                                foreach (LocalDeviceClass device in item.Value)
                                {


                                    writer.WriteStartElement("Device");
                                    Enum e = device.ActualValue;
                                    // device.Timestamp = DateTime.Now;

                                    //Enum.GetName(typeof(DeviceEnum), e);

                                    writer.WriteElementString("Type", device.DeviceType);
                                    writer.WriteElementString("ID", device.LocalDeviceCode.ToString());
                                    writer.WriteElementString("SendTo", device.SendTo);
                                    writer.WriteElementString("ActualValue", device.ActualValue.ToString());
                                    writer.WriteElementString("ActualState", device.ActualState.ToString());
                                    writer.WriteElementString("TimeStamp", device.Timestamp.ToString());

                                    if (device.DeviceType == "A")
                                    {
                                        writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());
                                    }
                                    else if (device.DeviceType == "D")
                                    {
                                        writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());
                                    }
                                    writer.WriteEndElement();


                                }
                                j++;
                            }
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }


                    }


                }


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
                    Console.WriteLine("***Postoje kontroleri:***");
                    foreach (var file in files)
                    {
                        p = file.Split('\\');
                        ime = p[4].Substring(0, p[4].Length - 4);
                        aktivniKontroleri.Add(ime);

                        foreach (var item in aktivniKontroleri)
                        {
                            Console.WriteLine("{0}", item);
                        }
                        aktivniKontroleri.Remove(ime);
                    }


                }
            }
            else {


                foreach (KeyValuePair<int, List<LocalDeviceClass>> item in Program.DeviceDic)
                {
                    if (item.Key == Program.l.IdControler)
                    {
                        foreach (LocalDeviceClass device in item.Value)
                        {

                            XDocument xDocument = XDocument.Load(p1);
                            XElement root = xDocument.Element("Devices");
                            IEnumerable<XElement> rows = root.Descendants("Device");
                            XElement firstRow = rows.First();


                            if (device.DeviceType == "A")
                            {

                                firstRow.AddBeforeSelf(
                                    new XElement("Device",
                                    new XElement("Type", device.DeviceType.ToString()),
                                    new XElement("ID", device.LocalDeviceCode.ToString()),
                                    new XElement("SendTo", device.SendTo.ToString()),
                                    new XElement("ActualValue", device.ActualValue.ToString()),
                                    new XElement("ActualState", device.ActualState.ToString()),
                                    new XElement("TimeStamp", device.Timestamp.ToString()),
                                    new XElement("Measurment", device.AnalogActualValue.ToString())));
                            }

                            if (device.DeviceType == "D")
                            {

                                firstRow.AddBeforeSelf(
                                     new XElement("Device",
                                     new XElement("Type", device.DeviceType.ToString()),
                                     new XElement("ID", device.LocalDeviceCode.ToString()),
                                     new XElement("SendTo", device.SendTo.ToString()),
                                     new XElement("ActualValue", device.ActualValue.ToString()),
                                     new XElement("ActualState", device.ActualState.ToString()),
                                     new XElement("TimeStamp", device.Timestamp.ToString()),
                                     new XElement("Measurment", device.AnalogActualValue.ToString())));

                            }
                            xDocument.Save(p1);
                        }
                    }
                }

            }

            return u;
        }


        public static bool CheckXMLFile(string p)
        {

            bool uspesno = false;
            string folder = @"..\..\..\Kontroleri";
            files = Directory.GetFiles(folder, "*.xml");

            foreach (string putanja in files)
            {
                if (putanja == p)
                {
                    uspesno = true;
                    break;
                }
                else {

                    uspesno = false;
                }

            }

            return uspesno;
        }

        public static bool WriteAMSxml(LocalDeviceClass device)
        {
            int j = 0;
            bool u = false;
            if (!File.Exists(@"..\..\..\AMSBaza\AMS.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(@"..\..\..\AMSBaza\AMS.xml"))
                {

                    u = true;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Devices");

                    while (j != 3)
                    {


                        writer.WriteStartElement("Device");
                        Enum e = device.ActualValue;


                        writer.WriteElementString("Type", device.DeviceType);
                        writer.WriteElementString("ID", device.LocalDeviceCode.ToString());
                        writer.WriteElementString("SendTo", device.SendTo);
                        writer.WriteElementString("ActualValue", device.ActualValue.ToString());
                        writer.WriteElementString("ActualState", device.ActualState.ToString());
                        writer.WriteElementString("TimeStamp", device.Timestamp.ToString());

                        if (device.DeviceType == "A")
                        {
                            writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());
                        }
                        else if (device.DeviceType == "D")
                        {
                            writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());
                        }
                        writer.WriteEndElement();
                        j++;
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();

                    

                }

          


            }
            else
            {
                XDocument xDocument = XDocument.Load(@"..\..\..\AMSBaza\AMS.xml");
                XElement root = xDocument.Element("Devices");
                IEnumerable<XElement> rows = root.Descendants("Device");
                XElement firstRow = rows.First();

                firstRow.AddBeforeSelf(
                    new XElement("Device",
                    new XElement("Type", device.DeviceType),
                    new XElement("ID", device.LocalDeviceCode.ToString()),
                    new XElement("SendTo", device.SendTo),
                    new XElement("ActualValue", device.ActualValue),
                    new XElement("ActualState", device.ActualState),
                    new XElement("TimeStamp", device.Timestamp.ToString()),
                    new XElement("Measurment", device.AnalogActualValue.ToString())));

                xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
         
                

            }

            return u;
        }

       
                
    }
}

