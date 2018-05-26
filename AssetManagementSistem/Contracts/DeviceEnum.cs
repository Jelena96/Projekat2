using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum DeviceEnum { on,off,close,open,none};
    public class Klasa
    {
       public DeviceEnum Device { get; set; }
    }
}
