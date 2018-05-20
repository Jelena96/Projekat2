using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDevice;

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

       public void Ispis1()
        {
            Console.WriteLine("Ja sam local controler i ziv sam.");
        }

    }
}
