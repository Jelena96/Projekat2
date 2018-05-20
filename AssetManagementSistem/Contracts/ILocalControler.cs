using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface ILocalControler
    {
      
        int LocalControlerCode { get; set; }
        
        long TimeStamp { get; set; }
        
      

        [OperationContract]
        void Ispis1();
    }
}
