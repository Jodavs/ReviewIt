using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.Values {
    public interface IValue {
        string GetString();
    }
}
