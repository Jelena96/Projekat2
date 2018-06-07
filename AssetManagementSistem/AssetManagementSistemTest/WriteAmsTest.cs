using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagementSistem;
using Contracts;
using NUnit.Framework;

namespace AssetManagementSistemTest
{
    [TestFixture]
    public class WriteAmsTest
    {

        public static DateTime date = DateTime.Now;


        [Test]
        [TestCase(null)]
        [ExpectedException(typeof(ArgumentNullException))]

        public void WriteAMSxml2BadParameters(LocalDeviceClass d1)
        {
            WriteAms w = new WriteAms();

            w.WriteAMSxml2(d1);
        }


        [Test]
        [TestCase(null, 346, null)]
        [TestCase(null, 23, null)]
        [TestCase(null, null, null)]
        [ExpectedException(typeof(ArgumentNullException))]

        public void ReadXMLBadParameters(string putanja, int id, DateTime datum)
        {
            WriteAms w = new WriteAms();

            w.ReadXML(putanja, id, datum);
        }




        [Test]
        [TestCase(null, -346, null)]
        [TestCase(null, -23, null)]
        [TestCase(null, null, null)]
        [ExpectedException(typeof(ArgumentNullException))]

        public void ReadXMLBadParameters2(string putanja, int id, DateTime datum)
        {
            WriteAms w = new WriteAms();

            w.ReadXML(putanja, id, datum);
        }




        [Test]
        [TestCase(null, -2, null)]
        [TestCase(null, -3, null)]
        [TestCase(null, null, null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteAMSxmlBadParameters(string s, int id, DateTime vreme)
        {

            WriteAms w = new WriteAms();

            w.WriteAMSxml(s, id, vreme);

        }


        [Test]
        [TestCase(null, 20, null)]
        [TestCase(null, 30, null)]
        [TestCase(null, null, null)]
        [ExpectedException(typeof(ArgumentNullException))]

        public void WriteAMSxmlBadParameters2(string putanja, int id, DateTime datum)
        {
            WriteAms w = new WriteAms();

            w.WriteAMSxml(putanja, id, datum);
        }
    }
}
