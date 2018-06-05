using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Contracts;

namespace AssetManagementSistem
{
    public class Device
    {
        public int LocalDeviceCode { get; set; }
    
       
        public string TimeStamp { get; set; }

        public int AnalogActualValue { get; set; }
      
        public string ActualValue { get; set; }
     
        public string DeviceType { get; set; }
       
        public string SendTo { get; set; }
        
        public string ActualState { get; set; }

        public  List<int> Measurments { get; set; }
        public   int BrMjerenja { get; set; }

        public Device(int id)
        {
            BrMjerenja = 0;
            Measurments = new List<int>();
            bool uspesno = false;
            string folder = @"..\..\..\AMSBaza";
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
                        uspesno = true;
                        xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                        str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim() + " " +
                         xmlnode[i].ChildNodes.Item(3).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(4).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
                        Console.WriteLine(str);
                        string[] stringArray = str.Split(' ');
                        if (Int32.Parse(stringArray[2]) == id)
                        {

                            DeviceType = stringArray[0];
                            LocalDeviceCode = Int32.Parse(stringArray[2]);
                            SendTo = stringArray[4];
                            ActualValue = stringArray[5];
                            ActualState = stringArray[7];
                            TimeStamp = stringArray[9] + stringArray[10];
                            AnalogActualValue = Int32.Parse(stringArray[12]);
                            Measurments.Add(AnalogActualValue);
                            BrMjerenja++;
                        }

                    }

                    fs.Close();

                }
            }
        }


        public override string ToString()
        {
            return LocalDeviceCode + " " + DeviceType + " " + " " + ReadValues() + "\n";
        }

        public string ReadValues()
        {
            string pom = "";

            for (int i = 0; i < Measurments.Count(); i++)
            {
                pom += "\n\t\t\t\tdate: " + TimeStamp + " value: " + Measurments[i] + "\n";
            }

            return pom;
        }

    }
}
