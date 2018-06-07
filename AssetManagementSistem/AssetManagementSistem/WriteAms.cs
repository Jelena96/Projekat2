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
        public static bool succes = false;
        bool success = false;
       


        private static int GetType2()
        {

            Random ran = new Random();
            return ran.Next(1, 300);
        }

        public void WriteAMSxml2(LocalDeviceClass device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }


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
                        writer.WriteElementString("TimeStamp", DateTime.Now.ToString());

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
                   
                    meas_value = GetType2();
                }
                else if (device.DeviceType == "D")
                {
                    
                    meas_value = GetType2() % 2;
                }

                firstRow.AddBeforeSelf(
                    new XElement("Device",
                    new XElement("Type", device.DeviceType),
                    new XElement("ID", device.LocalDeviceCode.ToString()),
                    new XElement("SendTo", device.SendTo),
                    new XElement("ActualValue", device.ActualValue),
                    new XElement("ActualState", device.ActualState),
                    new XElement("TimeStamp", DateTime.Now.ToString()),
                    new XElement("Measurment", meas_value)));


                xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
            }

            
        }



        public void ReadXML(string putanja,int id)
        {

            if (putanja == null || id==null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }

            if(id<=0)
            {
                throw new ArgumentException("Argument can't be lower than null");
            }

            DateTime datum = DateTime.Now;
           
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Device");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim() + " " +
                 xmlnode[i].ChildNodes.Item(3).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(4).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
                Console.WriteLine(str);
                WriteAMSxml(str,id,datum);
                succes = true;
            }

            fs.Close();
          
            //}
            //}

            
        }

        public void WriteAMSxml(string s,int id,DateTime vreme)
        {


            if (s == null || id == null || vreme == null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }
            if (id <= 0)
            {
                throw new ArgumentException("Argument can't be lower than null");
            }

            string[] stringArray = s.Split(' ');
            string Type = stringArray[0];
            string Id = stringArray[2].ToString();
            string SendTo = stringArray[4];
            string ActualValue = stringArray[5];
            string ActualState = stringArray[7];
            string TimeStamp = stringArray[9] +" "+stringArray[10];
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
                    writer.WriteElementString("SendToLC", id.ToString()+"|"+vreme.ToString());
                    writer.WriteElementString("ActualValue", ActualValue);
                    writer.WriteElementString("ActualState", ActualState);
                    writer.WriteElementString("TimeStamp", DateTime.Now.ToString());
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
                    new XElement("SendToLC", id.ToString()+"|"+ vreme.ToString()),
                    new XElement("ActualValue", ActualValue),
                    new XElement("ActualState", ActualState),
                    new XElement("TimeStamp", DateTime.Now.ToString()),
                    new XElement("Measurment", Measurment)));

                xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
            }
           
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
