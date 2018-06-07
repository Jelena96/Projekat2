using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using LocalControler;
using NUnit.Framework;

namespace LocalControlerTest
{
    [TestFixture]
    public class LocalControlerClassTest
    {
        public static Dictionary<int, List<LocalDeviceClass>> d = new Dictionary<int, List<LocalDeviceClass>>();

        [Test]
        [TestCase(null,null, null)]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateXML(string p1, Dictionary<int, List<LocalDeviceClass>> dic, LocalDeviceClass di)
        {
            LocalControlerClass kont = new LocalControlerClass();

            kont.CreateXML(p1, dic,di);
        }
    }
}
