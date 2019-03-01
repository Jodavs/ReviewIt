using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using ServerDTOs.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;

namespace RepositoryLayer.Repositories {
    public class ExclusionCriterionRepository : IRepository<ExclusionCriterionDTO> {
        private readonly EFContext _context;

        public ExclusionCriterionRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(ExclusionCriterionDTO dto) {
            var entity = dto.ConvertToEntity();
            if (entity == null) return 0;
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.ExclusionCriterion.SingleOrDefaultAsync(e => e.Id == id);
            if (entity == null) return 0;
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<ExclusionCriterionDTO>> ReadAsync() {
            return from exclusion in _context.ExclusionCriterion
                          select exclusion.ConvertToDTO();
        }

        public async Task<ExclusionCriterionDTO> ReadAsync(int id) {
            return await (from exclusion in _context.ExclusionCriterion
                          where exclusion.Id == id
                          select exclusion.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ExclusionCriterionDTO dto) {
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
