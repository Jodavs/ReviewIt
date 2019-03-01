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
    public class PhaseRepository : IRepository<PhaseDTO> {

        private readonly EFContext _context;

        public PhaseRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(PhaseDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.Phase.SingleAsync(p => p.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<PhaseDTO>> ReadAsync() {
            return from phase in _context.Phase
                          select phase.ConvertToDTO();
        }

        public async Task<PhaseDTO> ReadAsync(int id) {
            return await (from phase in _context.Phase
                          where phase.Id == id
                          select phase.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(PhaseDTO dto) {
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
