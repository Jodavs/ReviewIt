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
    public class ClassificationCriterionRepository : IRepository<ClassificationCriterionDTO> {
        private readonly EFContext _context;

        public ClassificationCriterionRepository(EFContext context) {
            _context = context;
        }
        public async Task<int> CreateAsync(ClassificationCriterionDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.ClassificationCriterion.SingleAsync(c => c.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<ClassificationCriterionDTO>> ReadAsync() {
            var classifications = from cc in _context.ClassificationCriterion
                                  select cc.ConvertToDTO();
            return classifications;
        }

        public async Task<ClassificationCriterionDTO> ReadAsync(int id) {
            var classifications = from cc in _context.ClassificationCriterion
                                  where id == cc.Id
                                  select cc.ConvertToDTO();
            return await classifications.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ClassificationCriterionDTO dto) {
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
