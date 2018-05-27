using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDevice;
using System.IO;
using System.Xml;
using System.Windows;
using System.Xml.Linq;

namespace LocalControler
{
    public class LocalControlerClass : ILocalControler
    {
        public static List<LocalDeviceClass> list;
        public int LocalControlerCode { get; set; }
        public long TimeStamp { get; set; }

        
        public LocalControlerClass() {

            list = new List<LocalDeviceClass>();

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
                        xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                        str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim()+" "+
                         xmlnode[i].ChildNodes.Item(3).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(4).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
                        Console.WriteLine(str);
                        WriteAMSxml(str);
                    }


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
            string ActualState= stringArray[7];
            string TimeStamp= stringArray[9] + stringArray[10]+stringArray[11];
            string Measurment= stringArray[13];

            if (!File.Exists(@"..\..\..\AMSBaza\AMS.xml")){

               

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

          
        

    }
}
