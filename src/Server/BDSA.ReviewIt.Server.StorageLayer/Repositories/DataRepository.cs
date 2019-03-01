using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;

namespace RepositoryLayer.Repositories {
    public class DataRepository : IRepository<DataDTO> {
        private readonly EFContext _context;

        public DataRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(DataDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.Data.SingleAsync(d => d.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<DataDTO>> ReadAsync() {
            return from data in _context.Data
                          select data.ConvertToDTO();
        }

        public async Task<DataDTO> ReadAsync(int id) {
            return await (from data in _context.Data
                          where data.Id == id
                          select data.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(DataDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Update(entity);
            try {
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException) {
                return false;
            }
        }
    }
}
