using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LocalDevice;
using System.IO;
using System.Xml;
using System.Windows.Controls;
using System.Threading;

namespace LocalControler
{
    class Program
    {
        private static IWriteAms proksi2;
        private static int idk;
        public static string path;
        public static int caseSwitch;
        public static string putanja = @"..\..\..\Vreme\vreme.xml";
        public static List<string> aktivniKontroleri = new List<string>();

        public static void Meni() {
            LocalControlerClass lc = new LocalControlerClass();
            Console.WriteLine(lc.ReadXMLTime(putanja).ToString());
            Console.WriteLine("*******MENI********");
            Console.WriteLine("1.Da li zelite da kreirate novi kontroler");
            Console.WriteLine("2.Da li zelite da upalite  kontroler");
            Console.WriteLine("Za kraj unosa, unesite 0");

            caseSwitch = int.Parse(Console.ReadLine());

        }

        static void Main(string[] args)
        {



            LocalControlerClass lc = new LocalControlerClass();

            OpenServer();
            Meni();

            do
            {
               
                switch (caseSwitch)
                {
                   
                    case 1:


                        Console.WriteLine("Koji kontroler zelite da kreirate:");
                        idk = int.Parse(Console.ReadLine());
                        path = @"..\..\..\Kontroleri\controler" + idk + ".xml";
                        lc.CreateXMLK(path);
                        string[] p2 = path.Split('\\');
                        string ime2 = p2[4].Substring(0, p2[4].Length - 4);
                       
                        Meni();
                        break;



                    case 2:

                        
                        Console.WriteLine("Koji kontroler zelite da upalite:");
                        IzlistajKontrolere();
                        idk = int.Parse(Console.ReadLine());
                        path = @"..\..\..\Kontroleri\controler" + idk + ".xml";
                        
                        string[] p = path.Split('\\');
                        string ime = p[4].Substring(0, p[4].Length - 4);
                        aktivniKontroleri.Remove(ime);
                        lc.LocalControlerCode = idk;
                        lc.TimeStamp = DateTime.Now;
                        bool success;
                        
                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            while (true)
                            {
                                ConnectwithAMS();
                                bool uspesno = proksi2.ReadXML(path, lc.LocalControlerCode, lc.TimeStamp);
                                
                                Thread.Sleep(lc.ReadXMLTime(putanja));
                            }
                            
                        }));
                        t.IsBackground = true;
                        t.Start();


                        

                        Meni();
                        break;
                }
            } while (caseSwitch != 0);
            File.Delete(path);
            Console.ReadLine();
            
        }

        static void OpenServer() {

            ServiceHost svc = new ServiceHost(typeof(LocalControlerClass));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(ILocalControler), binding, new Uri(String.Format("net.tcp://localhost:10101/LocalControlerClass")));
            svc.Open();

        }

         static void ConnectwithAMS()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;
            ChannelFactory<IWriteAms> factory = new ChannelFactory<IWriteAms>(binding, new EndpointAddress(String.Format("net.tcp://localhost:10100/WriteAms")));

            proksi2 = factory.CreateChannel();


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
    }
}
