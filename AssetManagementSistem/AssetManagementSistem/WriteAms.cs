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

namespace AssetManagementSistem
{
    public class WriteAms : IWriteAms
    {
        public LocalDeviceClass lc = new LocalDeviceClass();
        public string kosam = "";

        bool success = false;
        //public void PrimiObjekat(LocalDeviceClass o, string ko)
        //{
        //   if(kosam=="LD")
        //    {
        //        bool ajde = false;
        //        ajde = WriteAMSxml2(o);
        //    }
        //    else
        //    {
        //        bool ajde = false;
        //        ajde = ReadXML();
        //    }
        //}


        private static int GetType2()
        {

            Random ran = new Random();
            return ran.Next(1, 1000);
        }

        public bool WriteAMSxml2(LocalDeviceClass device)
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
                        writer.WriteElementString("TimeStamp", device.TimeStamp.ToString());

                        if (device.DeviceType == "A")
                        {
                            writer.WriteElementString("Measurment", GetType2().ToString());
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
                    writer.Flush();



                }

            }
            else
            {
                XDocument xDocument = XDocument.Load(@"..\..\..\AMSBaza\AMS.xml");
                XElement root = xDocument.Element("Devices");
                IEnumerable<XElement> rows = root.Descendants("Device");
                XElement firstRow = rows.First();

                int meas_value = 0;
                if (device.DeviceType == "A")
                {
                    //writer.WriteElementString("Measurment", GetType2().ToString());
                    meas_value = GetType2();
                }
                else if (device.DeviceType == "D")
                {
                    //writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());\
                    meas_value = GetType2() % 2;
                }

                firstRow.AddBeforeSelf(
                    new XElement("Device",
                    new XElement("Type", device.DeviceType),
                    new XElement("ID", device.LocalDeviceCode.ToString()),
                    new XElement("SendTo", device.SendTo),
                    new XElement("ActualValue", device.ActualValue),
                    new XElement("ActualState", device.ActualState),
                    new XElement("TimeStamp", device.TimeStamp.ToString()),
                    new XElement("Measurment", meas_value)));


                xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
            }

            return u;
        }



        public bool ReadXML()
        {
            bool uspesno = false;
            string folder = @"..\..\..\Kontroleri";
            string[] files = Directory.GetFiles(folder, "*.xml");

            if (files == null)
            {
                uspesno = false;
            }
            else
            {
                uspesno = true;

                foreach (string putanja in files)
                {
                    XmlDataDocument xmldoc = new XmlDataDocument();
                    XmlNodeList xmlnode;
                    int i = 0;
                    string str = null;
                    FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
                    xmldoc.Load(fs);
                    xmlnode = xmldoc.GetElementsByTagName("Device");
                    for (i = 0; i <= xmlnode.Count - 1; i++)
                    {
                        uspesno = true;
                        xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                        str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim() + " " +
                         xmlnode[i].ChildNodes.Item(3).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(4).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
                        Console.WriteLine(str);
                        WriteAMSxml(str);
                    }

                    fs.Close();

                }
            }

            return uspesno;
        }

        public bool WriteAMSxml(string s)
        {
            bool uspesno = false;

            string[] stringArray = s.Split(' ');
            string Type = stringArray[0];
            string Id = stringArray[2].ToString();
            string SendTo = stringArray[4];
            string ActualValue = stringArray[5];
            string ActualState = stringArray[7];
            string TimeStamp = stringArray[9] + stringArray[10];
            string Measurment = stringArray[12];
            success = true;

            if (!File.Exists(@"..\..\..\AMSBaza\AMS.xml"))
            {



                using (XmlWriter writer = XmlWriter.Create(@"..\..\..\AMSBaza\AMS.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Devices");


                    writer.WriteStartElement("Device");

                    writer.WriteElementString("Type", Type);
                    writer.WriteElementString("ID", Id);
                    writer.WriteElementString("SendTo", SendTo);
                    writer.WriteElementString("ActualValue", ActualValue);
                    writer.WriteElementString("ActualState", ActualState);
                    writer.WriteElementString("TimeStamp", TimeStamp);
                    writer.WriteElementString("Measurment", Measurment);

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
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
                    new XElement("Type", Type),
                    new XElement("ID", Id),
                    new XElement("SendTo", SendTo),
                    new XElement("ActualValue", ActualValue),
                    new XElement("ActualState", ActualState),
                    new XElement("TimeStamp", TimeStamp),
                    new XElement("Measurment", Measurment)));

                xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
            }
            return uspesno;
        }

        public void DeleteControllers()
        {

            string folder = @"..\..\..\Kontroleri";
            string[] files = Directory.GetFiles(folder, "*.xml");

            if (success)
            {

                foreach (string putanja in files)
                {

                    File.Delete(putanja);



                }
            }
        }
    } 
}
