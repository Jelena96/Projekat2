﻿using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
        public static List<int> ids = new List<int>();
        public static List<LocalDeviceClass> ld = new List<LocalDeviceClass>();
        public static LocalDeviceClass l;
        public static bool success = false;
        public static int hash = 45;
        private static int id;
        private static ILocalControler proksi;
        private static IWriteAms proksi2;
        public static Dictionary<int, List<LocalDeviceClass>> DeviceDic = new Dictionary<int, List<LocalDeviceClass>>();

        

        private static void ConnectLC() {
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;
            ChannelFactory<ILocalControler> factory = new ChannelFactory<ILocalControler>(binding, new EndpointAddress(String.Format("net.tcp://localhost:10101/LocalControlerClass")));

            proksi = factory.CreateChannel();
            

        }

        private static void ConnectAMS()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;
            ChannelFactory<IWriteAms> factory = new ChannelFactory<IWriteAms>(binding, new EndpointAddress(String.Format("net.tcp://localhost:10102/WriteAms")));

            proksi2 = factory.CreateChannel();


        }

        public class SomeType
        {
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

         private static int GetType1()
            {

            return ran.Next(0, 1);

            }
        private static int GetType2()
        {

            return ran.Next(1, 300);
        }

        public static int GetHashCode2()
        {
            return Int32.MaxValue.GetHashCode();
        }

        static void Main(string[] args)
        {
      



        string path = "";

          

            string type = "";
            int idk = 0;
           
                while (type != "E")
                {
                if (ld.Count != 0)
                {
                    ld.Clear();
                }

                Console.WriteLine("Unos novog uredjaja");
                Console.WriteLine("Unesi tip zeljenog uredjaja:");
                type = Console.ReadLine();
                if (type == "A" || type == "D")
                {
                 

                    Console.WriteLine("Unesi id uredjaja");
                    int id = int.Parse(Console.ReadLine());
           
                    SomeType s = new SomeType();
                    string text = ReadIdXml();
                    
                   
                    if (text.Contains(id.ToString()))
                    {
                        Console.WriteLine("Vec postoji, dodeljujem mu HASH vrednost");
                        id = s.GetHashCode();

                    }
                    else
                    {

                        WriteIdXml(id);
                        ids.Add(id);

                    }

                    Console.WriteLine("Da li zelite da promenite stanje uredjaja? (ukoliko zelite odgovorite sa: da)");

                    string promena = Console.ReadLine();
                    if (promena == "da")
                    {
                       // promene++;
                        Console.WriteLine("Promenili ste stanje");
                        if (type == "A")
                        {

                            l = new LocalDeviceClass() { LocalDeviceCode = id, IdControler = idk, DeviceType = type, TimeStamp = DateTime.Now, AnalogActualValue = GetType2(), ActualValue = DeviceEnum.off, ActualState = DeviceEnum.close };
                        }

                        if (type == "D")
                        {
                            l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, IdControler = idk, TimeStamp = DateTime.Now, ActualValue = DeviceEnum.off, ActualState = DeviceEnum.close, AnalogActualValue = GetType1() };

                        }

                    }
                    else
                    {

                        Console.WriteLine("Unesi kome zelis da saljes podatke");
                        string salji = Console.ReadLine();

                    if (salji == "LC")
                        {

                            IzlistajKontrolere();
                            Console.WriteLine("Unesi id zeljenog kontrolera:");
                            idk = int.Parse(Console.ReadLine());

                            if (type == "A")
                            {

                                l = new LocalDeviceClass() { LocalDeviceCode = id, IdControler = idk, DeviceType = type, TimeStamp = DateTime.Now, AnalogActualValue = GetType2(), ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, SendTo = salji };
                            }

                            if (type == "D")
                            {
                                l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, IdControler = idk, TimeStamp = DateTime.Now, ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, AnalogActualValue = GetType1(), SendTo = salji };

                            }

                            if (!DeviceDic.ContainsKey(idk))
                            {
                                DeviceDic.Add(idk, new List<LocalDeviceClass>());
                            }

                            foreach (KeyValuePair<int, List<LocalDeviceClass>> item in DeviceDic)
                            {
                                if (item.Key == idk)
                                {
                                    item.Value.Add(l);
                                }
                            }
                            path = @"..\..\..\Kontroleri\controler" + idk + ".xml";
                            bool uspjesno = false;
                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                while (true)
                                {
                                    ConnectLC();
                                    proksi.CreateXML(path, DeviceDic, l);
                                    Thread.Sleep(2000);
                                }
                            }));
                            t.IsBackground = true;
                            t.Start();

                        }

                        else if (salji == "AMS")
                        {

                            if (type == "A")
                            {

                                l = new LocalDeviceClass() { LocalDeviceCode = id, IdControler = idk, DeviceType = type, TimeStamp = DateTime.Now, AnalogActualValue = GetType2(), ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, SendTo = salji };
                            }

                            if (type == "D")
                            {
                                l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, IdControler = idk, TimeStamp = DateTime.Now, ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, AnalogActualValue = GetType1(), SendTo = salji };

                            }

                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                while (true)
                                {
                                    ConnectAMS();
                                //LocalDeviceClass.WriteAMSxml(l);
                                //success = true;
                                proksi2.WriteAMSxml2(l);

                                    Thread.Sleep(1000);
                                }


                            }));

                            t.IsBackground = true;
                            t.Start();

                        }
                    }
                }
                else
                {

                    Console.WriteLine("Niste uneli dobar tip uredjaja");
                    //continue;
                }
                    
                    
                }

                    

            Console.ReadLine();


        }

        public static void IzlistajKontrolere()
        {

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

        public static void WriteIdXml(int id)
        {
            if (id <= 0 || id==null)
            {
                throw new ArgumentException("Argument can't be lower than null or null");
            }

            if (!File.Exists(@"..\..\..\BazaId\Id.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(@"..\..\..\BazaId\Id.xml"))
                {


                    writer.WriteStartDocument();
                    writer.WriteStartElement("Devices");
                    writer.WriteStartElement("Device");

                    writer.WriteElementString("iD", id.ToString());

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();


                }

            }
            else
            {
                XDocument xDocument = XDocument.Load(@"..\..\..\BazaId\Id.xml");
                XElement root = xDocument.Element("Devices");
                IEnumerable<XElement> rows = root.Descendants("Device");
                XElement firstRow = rows.First();



                firstRow.AddBeforeSelf(
                    new XElement("Device",
                    new XElement("iD",id.ToString())));


                xDocument.Save(@"..\..\..\BazaId\Id.xml");
            }


        }



        public static string ReadIdXml()
        {
            string text = "";
            
            string folder = @"..\..\..\BazaId";
            string[] files = Directory.GetFiles(folder, "*.xml");

            if (files == null)
            {
                
            }
            else
            {
                

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
                        str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                       // Console.WriteLine(str);
                        text += str;

                    }

                    fs.Close();

                }
            }

            return text;
        }
    }
}
