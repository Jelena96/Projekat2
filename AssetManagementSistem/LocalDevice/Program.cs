using Contracts;
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
        
        public static List<int> ids = new List<int>();
        public static List<LocalDeviceClass> ld = new List<LocalDeviceClass>();
          public static LocalDeviceClass l;
        public static bool success = false;
        
        private static ILocalControler proksi;
        private static IWriteAms proksi2;
        public static Dictionary<int, List<LocalDeviceClass>> DeviceDic = new Dictionary<int, List<LocalDeviceClass>>();

        /*private static DeviceEnum GetType3()
        {
            return (DeviceEnum)(ran.Next(0, 3));

        }*/

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
        private static int GetType1()
        {

            return ran.Next(0, 1);
        }
        private static int GetType2()
        {

            return ran.Next(1, 300);
        }

        static void Main(string[] args)
        {
            LocalDeviceClass1 lo = new LocalDeviceClass1();
        string path = "";
            string type = "";
            int idk = 0;
           
                while (type != "E")
                {
                if (ld.Count != 0)
                {
                    ld.Clear();
                }
              
                Console.WriteLine("Unesi tip zeljenog uredjaja:");
                type = Console.ReadLine();
                if (type == "A" || type == "D")
                {
                   // l = new LocalDeviceClass();

                    Console.WriteLine("Unesi id uredjaja");
                    int id = int.Parse(Console.ReadLine());
                    while(ids.Contains(id))
                    {
                        Console.WriteLine("Ponovo unesite ID uredjaja,uneseni vec postoji");
                        id = int.Parse(Console.ReadLine());
                    }
                    ids.Add(id);
                   

                    Console.WriteLine("Unesi id zeljenog kontrolera:");
                    idk = int.Parse(Console.ReadLine());
                    
                    Console.WriteLine("Unesi kome zelis da saljes podatke");
                    string salji = Console.ReadLine();


                    if (type == "A")
                    {
                        //Thread t = new Thread(new ThreadStart(() =>
                        //{
                            //while (true)
                            //{
                                l = new LocalDeviceClass() { LocalDeviceCode = id, IdControler = idk, DeviceType = type, TimeStamp = DateTime.Now, AnalogActualValue = GetType2(), ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, SendTo = salji };
                        //    }

                        //}));
                        //t.IsBackground = true;
                        //t.Start();

            }

                    if (type == "D")
                    {

                        //Thread t = new Thread(new ThreadStart(() =>
                        //{
                        //    while (true)
                        //    {
                                l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, IdControler = idk, TimeStamp = DateTime.Now, ActualValue = DeviceEnum.on, ActualState = DeviceEnum.close, AnalogActualValue = GetType1(), SendTo = salji };
                        //    }

                        //}));
                        //t.IsBackground = true;
                        //t.Start();
                    }

                      //zavrsili sa deviceom
                
                    
                    if (salji == "LC")
                    {
                        if (!DeviceDic.ContainsKey(idk))
                        {
                            DeviceDic.Add(idk,new List<LocalDeviceClass>());
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
                                uspjesno = proksi.CreateXML(path, DeviceDic, l);
                                Thread.Sleep(2000);
                            }
                        }));
                        t.IsBackground = true;
                        t.Start();
                       
                    }

                 else if(salji=="AMS")
                    {
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
                else
                {

                    Console.WriteLine("Niste uneli dobar tip uredjaja");
                    //continue;
                }
                    
                    
                }

                    

            Console.ReadLine();


        }
    }
}
