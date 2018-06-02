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
using System.Runtime.Serialization;
using System.Threading;

namespace LocalControler
{
    [DataContract]
    public class LocalControlerClass : ILocalControler
    {
        [DataMember]
        public static List<LocalDeviceClass> list;
        [DataMember]
        public int LocalControlerCode { get; set; }
        [DataMember]
        public long TimeStamp { get; set; }
        private static List<string> aktivniKontroleri = new List<string>();
        public static string[] files;

        public static bool success = false;
        
        public LocalControlerClass() {

            list = new List<LocalDeviceClass>();

        }

       //public bool ReadXML()
       // {
       //     bool uspesno = false;
       //     string folder = @"..\..\..\Kontroleri";
       //     string[] files = Directory.GetFiles(folder, "*.xml");

       //     if (files == null)
       //     {
       //         uspesno = false;
       //     }
       //     else
       //     {
       //         uspesno = true;

       //         foreach (string putanja in files)
       //         {
       //             XmlDataDocument xmldoc = new XmlDataDocument();
       //             XmlNodeList xmlnode;
       //             int i = 0;
       //             string str = null;
       //             FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
       //             xmldoc.Load(fs);
       //             xmlnode = xmldoc.GetElementsByTagName("Device");
       //             for (i = 0; i <= xmlnode.Count - 1; i++)
       //             {
       //                 uspesno = true;
       //                 xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
       //                 str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim()+" "+
       //                  xmlnode[i].ChildNodes.Item(3).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(4).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
       //                 Console.WriteLine(str);
       //                 WriteAMSxml(str);
       //             }

       //             fs.Close();
                
       //         }
       //     }

       //     return uspesno;
       // }

       // public bool WriteAMSxml(string s)
       // {
       //     bool uspesno = false;

       //     string[] stringArray = s.Split(' ');
       //     string Type = stringArray[0];
       //     string Id = stringArray[2].ToString();
       //     string SendTo = stringArray[4];
       //     string ActualValue = stringArray[5];
       //     string ActualState = stringArray[7];
       //     string TimeStamp = stringArray[9] + stringArray[10];
       //     string Measurment = stringArray[12];
       //     success = true;

       //     if (!File.Exists(@"..\..\..\AMSBaza\AMS.xml"))
       //     {



       //         using (XmlWriter writer = XmlWriter.Create(@"..\..\..\AMSBaza\AMS.xml"))
       //         {
       //             writer.WriteStartDocument();
       //             writer.WriteStartElement("Devices");


       //             writer.WriteStartElement("Device");

       //             writer.WriteElementString("Type", Type);
       //             writer.WriteElementString("ID", Id);
       //             writer.WriteElementString("SendTo", SendTo);
       //             writer.WriteElementString("ActualValue", ActualValue);
       //             writer.WriteElementString("ActualState", ActualState);
       //             writer.WriteElementString("TimeStamp", TimeStamp);
       //             writer.WriteElementString("Measurment", Measurment);

       //             writer.WriteEndElement();

       //             writer.WriteEndElement();
       //             writer.WriteEndDocument();
       //         }

       //     }
       //     else
       //     {
       //         XDocument xDocument = XDocument.Load(@"..\..\..\AMSBaza\AMS.xml");
       //         XElement root = xDocument.Element("Devices");
       //         IEnumerable<XElement> rows = root.Descendants("Device");
       //         XElement firstRow = rows.First();

       //         firstRow.AddBeforeSelf(
       //             new XElement("Device",
       //             new XElement("Type", Type),
       //             new XElement("ID", Id),
       //             new XElement("SendTo", SendTo),
       //             new XElement("ActualValue", ActualValue),
       //             new XElement("ActualState", ActualState),
       //             new XElement("TimeStamp", TimeStamp),
       //             new XElement("Measurment", Measurment)));

       //         xDocument.Save(@"..\..\..\AMSBaza\AMS.xml");
       //     }
       //     return uspesno;
       // }

       // public void DeleteControllers() {

       //     string folder = @"..\..\..\Kontroleri";
       //     string[] files = Directory.GetFiles(folder, "*.xml");

       //     if (success)
       //     {

       //         foreach (string putanja in files)
       //         {

       //             File.Delete(putanja);



       //         }
       //     }
       // }

        public void Procitaj(Contracts.LocalDeviceClass P) {

            Console.WriteLine("Uredjaj"+P.DeviceType + "" +P.LocalDeviceCode);
        }



        // public bool CreateXML(string p1, Dictionary<int,List<LocalDeviceClass>> dic,int d)
        public bool CreateXML(string p1, Dictionary<int, List<LocalDeviceClass>> dic, LocalDeviceClass di)
        {
            int j = 0;
            bool u = false;

            if (!CheckXMLFile(p1))
            {
                using (XmlWriter writer = XmlWriter.Create(p1))
                {

                    foreach (KeyValuePair<int, List<LocalDeviceClass>> item in dic)
                    {
                        if (item.Key == di.IdControler)
                        {

                            u = true;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Devices");

                            while (j != 3)
                            {

                                foreach (LocalDeviceClass d in item.Value)
                                {
                               

                                    writer.WriteStartElement("Device");
                                    //Enum e = device.ActualValue;
                                    // device.Timestamp = DateTime.Now;

                                    //Enum.GetName(typeof(DeviceEnum), e);

                                    writer.WriteElementString("Type", d.DeviceType);
                                    writer.WriteElementString("ID", d.LocalDeviceCode.ToString());
                                    writer.WriteElementString("SendTo", d.SendTo);
                                    writer.WriteElementString("ActualValue", d.ActualValue.ToString());
                                    writer.WriteElementString("ActualState", d.ActualState.ToString());
                                    writer.WriteElementString("TimeStamp", d.TimeStamp.ToString());

                                    if (d.DeviceType == "A")
                                    {
                                        writer.WriteElementString("Measurment", d.AnalogActualValue.ToString());
                                    }
                                    else if (d.DeviceType == "D")
                                    {
                                        writer.WriteElementString("Measurment", d.AnalogActualValue.ToString());
                                    }
                                    writer.WriteEndElement();
                                    

                                }
                                j++;
                            }
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            //writer.Close();
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
            else
            {


                foreach (KeyValuePair<int, List<LocalDeviceClass>> item in dic)
                {
                    if (item.Key == di.IdControler)
                    {
                        foreach (LocalDeviceClass d in item.Value)
                        {

                            XDocument xDocument = null;

                            bool savesuccess = false;
                            while (!savesuccess)
                            {
                                try
                                {
                                    xDocument = XDocument.Load(p1);
                                    savesuccess = true;
                                }
                                catch (Exception e)
                                {
                                    //Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                                    Thread.Sleep(100);
                                }
                            }

                            XElement root = xDocument.Element("Devices");

                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                IEnumerable<XElement> rows = root.Descendants("Device");
                                XElement firstRow = rows.First();


                                if (d.DeviceType == "A")
                                {

                                    firstRow.AddBeforeSelf(
                                        new XElement("Device",
                                        new XElement("Type", d.DeviceType.ToString()),
                                        new XElement("ID", d.LocalDeviceCode.ToString()),
                                        new XElement("SendTo", d.SendTo.ToString()),
                                        new XElement("ActualValue", d.ActualValue.ToString()),
                                        new XElement("ActualState", d.ActualState.ToString()),
                                        new XElement("TimeStamp", d.TimeStamp.ToString()),
                                        new XElement("Measurment", d.AnalogActualValue.ToString())));
                                }

                                if (d.DeviceType == "D")
                                {

                                    firstRow.AddBeforeSelf(
                                         new XElement("Device",
                                         new XElement("Type", d.DeviceType.ToString()),
                                         new XElement("ID", d.LocalDeviceCode.ToString()),
                                         new XElement("SendTo", d.SendTo.ToString()),
                                         new XElement("ActualValue", d.ActualValue.ToString()),
                                         new XElement("ActualState", d.ActualState.ToString()),
                                         new XElement("TimeStamp", d.TimeStamp.ToString()),
                                         new XElement("Measurment", d.AnalogActualValue.ToString())));

                                }

                                /*bool*/ savesuccess = false;
                                while (!savesuccess)
                                {
                                    try
                                    {
                                        xDocument.Save(p1);
                                        savesuccess = true;
                                    } catch (Exception e)
                                    {
                                        //Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                                        Thread.Sleep(100);
                                    }
                                }
                            
                                Thread.Sleep(3000);
                            }));
                            t.IsBackground = true;
                            t.Start();
                            //IEnumerable<XElement> rows = root.Descendants("Device");
                            //XElement firstRow = rows.First();


                            //if (d.DeviceType == "A")
                            //{

                            //    firstRow.AddBeforeSelf(
                            //        new XElement("Device",
                            //        new XElement("Type", d.DeviceType.ToString()),
                            //        new XElement("ID", d.LocalDeviceCode.ToString()),
                            //        new XElement("SendTo", d.SendTo.ToString()),
                            //        new XElement("ActualValue", d.ActualValue.ToString()),
                            //        new XElement("ActualState", d.ActualState.ToString()),
                            //        new XElement("TimeStamp", d.TimeStamp.ToString()),
                            //        new XElement("Measurment", d.AnalogActualValue.ToString())));
                            //}

                            //if (d.DeviceType == "D")
                            //{

                            //    firstRow.AddBeforeSelf(
                            //         new XElement("Device",
                            //         new XElement("Type", d.DeviceType.ToString()),
                            //         new XElement("ID", d.LocalDeviceCode.ToString()),
                            //         new XElement("SendTo", d.SendTo.ToString()),
                            //         new XElement("ActualValue", d.ActualValue.ToString()),
                            //         new XElement("ActualState", d.ActualState.ToString()),
                            //         new XElement("TimeStamp", d.TimeStamp.ToString()),
                            //         new XElement("Measurment", d.AnalogActualValue.ToString())));

                            //}
                            //xDocument.Save(p1);
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
                else
                {

                    uspesno = false;
                }

            }

            return uspesno;
        }




    }
}
