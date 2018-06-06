using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using LocalDevice;
using NUnit.Framework;

namespace LocalDeviceTest
{
    [TestFixture]
    public class LocalDeviceClass1Test
    {
        string t = "A";
        string t1 = "D";
        string t3 = "R";
        LocalDeviceClass l = new LocalDeviceClass();

        [Test]
        [TestCase(1,56)]
        [TestCase(20,90)]
        [TestCase(90,80)]
        [TestCase(70,2)]
        [TestCase(51,7)]
        public void PromenaStanjaGoodParameter(int id, int idk)
        {
            LocalDeviceClass1 ld = new LocalDeviceClass1();
            ld.PromenaStanja(id, idk, t, l, "da");
        }
    }
}
