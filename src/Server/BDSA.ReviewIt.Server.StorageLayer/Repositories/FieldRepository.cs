using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using ServerDTOs.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories {
    public class FieldRepository : IRepository<FieldDTO> {
        private readonly EFContext _context;

        public FieldRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(FieldDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.Field.SingleAsync(f => f.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<FieldDTO>> ReadAsync() {
            return from field in _context.Field
                          select field.ConvertToDTO();
        }

        public async Task<FieldDTO> ReadAsync(int id) {
            return await (from field in _context.Field
                          where field.Id == id
                          select field.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(FieldDTO dto) {
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
