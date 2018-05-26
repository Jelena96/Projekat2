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
        private static List<string> aktivniKontroleri = new List<string>();
        public static List<LocalDeviceClass> ld = new List<LocalDeviceClass>();
        public static Dictionary<int, List<LocalDeviceClass>> DeviceDic = new Dictionary<int, List<LocalDeviceClass>>();
        public static LocalDeviceClass l;
        
            
            
            
        private static DeviceEnum GetType3()
        {
            return (DeviceEnum)(ran.Next(0, 3));

        }



        private static int GetType2()
        {

            return ran.Next(0, 10);
        }


        static void Main(string[] args)
        {
           

                        /*{ 1,new List<LocalDeviceClass>()},
                        { 2,new List<LocalDeviceClass>()},
                        { 3,new List<LocalDeviceClass>()},
                        { 4,new List<LocalDeviceClass>()},
                        { 5,new List<LocalDeviceClass>()},

                    };*/

            string path = "";
            string type = "";
            
            int idk = 0;
           
                while (type != "E")
            {
                if (ld.Count != 0)
                {
                    ld.Clear();
                }
              
                Console.WriteLine("Unesi tip zeljenog uredjaja");
                type = Console.ReadLine();
                if (type == "A" || type == "D")
                {
                     l = new LocalDeviceClass();
                    Console.WriteLine("Unesi id uredjaja");
                    int id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Unesi id zeljenog kontrolera:");
                    idk = int.Parse(Console.ReadLine());

                    Console.WriteLine("Unesi kome zelis da saljes podatke");
                    string salji = Console.ReadLine();


                    if (type == "A")
                    {

                        l = new LocalDeviceClass() { LocalDeviceCode = id, IdControler = idk, DeviceType = type, Timestamp = DateTime.Now, AnalogActualValue=GetType2() ,ActualValue = DeviceEnum.none, SendTo = salji };
                        
                    }

                    if (type == "D")
                    {
                        l = new LocalDeviceClass() { LocalDeviceCode = id, DeviceType = type, IdControler = idk, Timestamp = DateTime.Now, ActualValue = GetType3(), AnalogActualValue=0, SendTo = salji };


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
                                //foreach (LocalDeviceClass str in item.Value)
                                //{
                                item.Value.Add(l);
                            }
                            //}
                        }
                        path = @"..\..\..\Kontroleri\controler" + idk + ".xml";
                        bool uspjesno = false;
                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            uspjesno=l.CreateXML(path);
                            //FileStream fs = new FileStream(path, FileMode.Append);
                            Thread.Sleep(Timeout.Infinite);
                        }));
                        t.IsBackground = true;
                        t.Start();
                       
                    }

                 else if(salji=="AMS")
                    {

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
