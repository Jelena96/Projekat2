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
        public DateTime TimeStamp { get; set; }
        private static List<string> aktivniKontroleri = new List<string>();
        public static string[] files;
    
        

        public static bool success = false;
        
        public LocalControlerClass() {

            list = new List<LocalDeviceClass>();

        }


        public void Procitaj(Contracts.LocalDeviceClass P) {

            Console.WriteLine("Uredjaj"+P.DeviceType + "" +P.LocalDeviceCode);
        }

        private static int GetType2()
        {

            Random ran = new Random();
            return ran.Next(1, 300);
        }

        // public bool CreateXML(string p1, Dictionary<int,List<LocalDeviceClass>> dic,int d)
        public void CreateXML(string p1, Dictionary<int, List<LocalDeviceClass>> dic, LocalDeviceClass di)
        {

            if (dic == null || di==null || p1==null)
            {
                throw new NullReferenceException("Argument can't be null!");
            }
            if (dic.Count == 0)
            {
                throw new ArgumentException("Dictionary must have values!");
            }


            int j = 0;
         

            if (!CheckXMLFile(p1))
            {
                using (XmlWriter writer = XmlWriter.Create(p1))
                {

                    foreach (KeyValuePair<int, List<LocalDeviceClass>> item in dic)
                    {
                        if (item.Key == di.IdControler)
                        {

                            
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Devices");


                            while (j != 5)
                            {

                                foreach (LocalDeviceClass d in item.Value)
                                {


                                    writer.WriteStartElement("Device");
                                   

                                    int meas_value = 0;
                                    if (d.DeviceType == "A")
                                    {
                                     
                                        meas_value = GetType2();
                                    }
                                    else if (d.DeviceType == "D")
                                    {
                                       
                                        meas_value = GetType2() % 2;
                                    }



                                    writer.WriteElementString("Type", d.DeviceType);
                                    writer.WriteElementString("ID", d.LocalDeviceCode.ToString());
                                    writer.WriteElementString("SendTo", d.SendTo);
                                    writer.WriteElementString("ActualValue", d.ActualValue.ToString());
                                    writer.WriteElementString("ActualState", d.ActualState.ToString());
                                    writer.WriteElementString("TimeStamp", DateTime.Now.ToString());
                                    writer.WriteElementString("Measurment", meas_value.ToString());
                                    writer.WriteEndElement();


                                }
                                j++;
                            }
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            //writer.Close();
                        }
                    }

                    IzlistajKontrolere();
                   
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

                                int meas_value = 0;
                                if (d.DeviceType == "A")
                                {
                                    //writer.WriteElementString("Measurment", GetType2().ToString());
                                    meas_value = GetType2();
                                }
                                else if (d.DeviceType == "D")
                                {
                                    //writer.WriteElementString("Measurment", device.AnalogActualValue.ToString());\
                                    meas_value = GetType2() % 2;
                                }

                              

                                    firstRow.AddBeforeSelf(
                                        new XElement("Device",
                                        new XElement("Type", d.DeviceType.ToString()),
                                        new XElement("ID", d.LocalDeviceCode.ToString()),
                                        new XElement("SendTo", d.SendTo.ToString()),
                                        new XElement("ActualValue", d.ActualValue.ToString()),
                                        new XElement("ActualState", d.ActualState.ToString()),
                                        new XElement("TimeStamp", DateTime.Now.ToString()),
                                        new XElement("Measurment", meas_value.ToString())));
                                

                                /*bool*/
                                savesuccess = false;
                                while (!savesuccess)
                                {
                                    try
                                    {
                                        xDocument.Save(p1);
                                        savesuccess = true;
                                    }
                                    catch (Exception e)
                                    {
                                        //Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                                        Thread.Sleep(100);
                                    }
                                }

                                Thread.Sleep(3000);
                            }));
                            t.IsBackground = true;
                            t.Start();

                        }
                    }
                }

            }

            
        }

        public int ReadXMLTime(string putanja)
        {
            bool uspesno = false;
            
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            int text =0;
            string str = null;
            FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("note");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                uspesno = true;
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                text = int.Parse(str); 
               
            }

            fs.Close();

            return text;
        }

        public bool CreateXMLK(string p1)
        {
            int j = 0;
            bool u = false;

            if (!CheckXMLFile(p1))
            {
                using (XmlWriter writer = XmlWriter.Create(p1))
                {


                            u = true;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Devices");
                            writer.WriteStartElement("Device");

                            //writer.WriteEndElement();
                            writer.WriteEndDocument();
                            //writer.Close();
                        }
                    }
            return u;
        }

        public static void IzlistajKontrolere() {

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
