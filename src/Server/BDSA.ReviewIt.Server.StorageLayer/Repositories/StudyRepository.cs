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
    public class StudyRepository : IRepository<StudyDTO> {
        private readonly EFContext _context;

        public StudyRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(StudyDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }   

        public async Task<int> DeleteAsync(int id) {
            var entity = await _context.Study.SingleAsync(s => s.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<StudyDTO>> ReadAsync() {
            return from study in _context.Study
                          select study.ConvertToDTO();
        }

        public async Task<StudyDTO> ReadAsync(int id) {
            return await (from study in _context.Study
                          where study.Id == id
                          select study.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(StudyDTO dto) {
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
