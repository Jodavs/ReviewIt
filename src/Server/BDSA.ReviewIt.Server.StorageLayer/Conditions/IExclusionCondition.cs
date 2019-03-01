using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.StorageLayer.Conditions {
    public delegate bool ValueComparator<in T>(T a, T b);

    public interface IExclusionCondition<in T> where T : IValue
    {
        bool Apply(T a);
    }
}
