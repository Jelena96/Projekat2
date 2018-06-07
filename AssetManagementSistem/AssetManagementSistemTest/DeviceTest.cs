using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagementSistem;
using NUnit.Framework;

namespace AssetManagementSistemTest
{
    [TestFixture]
    public class DeviceTest
    {
        [Test]
        [TestCase(1,15283, 15283,6)]
        [TestCase(50,15283, 152834, 4)]
        [TestCase(230, 15283, 1528, 4)]
        [TestCase(100, 15283, 15283, 5)]
        [TestCase(34, 1504, 15406, 4)]
        public void DeviceGoodParameter(int id, int t1, int t2, int p)
        {
            Device d = new Device(id, t1, t2, p);
        }


        [Test]
        [TestCase(-1,0,-1,0)]
        [TestCase(-10,0,0,0)]
        [TestCase(-100,0,-234,-1244)]
        [TestCase(-2345,-234,0,0)]
        [TestCase(-40,-345,-56,-788)]
        [TestCase(-409,0,0,-23)]
        [TestCase(-90,-12,-34,0)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeviceBadParameter(int id, int t1, int t2, int p)
        {
            Device d = new Device(id, t1, t2, p);
        }



        [Test]
        [TestCase(-1, null, -1, 0)]
        [TestCase(-10, 0, null, 0)]
        [TestCase(-100, null, -234, null)]
        [TestCase(-2345, null, 0, 0)]
        [TestCase(null, -345, null, -788)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeviceBadParameter2(int id, int t1, int t2, int p)
        {
            Device d = new Device(id, t1, t2, p);
        }

    }
}
