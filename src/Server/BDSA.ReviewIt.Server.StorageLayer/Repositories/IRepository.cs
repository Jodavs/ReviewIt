using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.Repositories {
    /// <summary>
    /// Basic Async CRUD
    /// </summary>
    /// <typeparam name="E">An object with an ID</typeparam>
    public interface IRepository<D> : IDisposable where D : IDTO {
        Task<int> CreateAsync(D dto);
        Task<IQueryable<D>> ReadAsync();
        Task<D> ReadAsync(int id);
        Task<bool> UpdateAsync(D dto);
        Task<int> DeleteAsync(int id);
    }
}
