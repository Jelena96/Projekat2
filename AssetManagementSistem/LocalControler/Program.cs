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
        public static List<string> aktivniKontroleri = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("u kontroleru sam");

            LocalControlerClass lc = new LocalControlerClass();

            OpenServer();

            IzlistajKontrolere();
            int unos = 0;
            Console.WriteLine();
         
            Console.WriteLine("koji kontroler zelite da upalite:");
            idk = int.Parse(Console.ReadLine());

           
                string path = @"..\..\..\Kontroleri\controler" + idk + ".xml";
                //string s = path.Split('\');

                lc.LocalControlerCode = idk;
                lc.TimeStamp = 2635;


                Thread t = new Thread(new ThreadStart(() =>
                {
                    ConnectwithAMS();
                    bool uspesno = proksi2.ReadXML(path);
                    
                    Thread.Sleep(10000);
                }));
                t.IsBackground = true;
                t.Start();
            
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
