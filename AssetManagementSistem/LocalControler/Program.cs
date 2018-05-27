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

namespace LocalControler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("u kontroleru sam");
            LocalControlerClass lc = new LocalControlerClass();
            bool uspesno = lc.ReadXML();
            if (uspesno)
            {
                Console.WriteLine("Procitao sam");
            }
           


            Console.ReadLine();
            
        }
    }
}
