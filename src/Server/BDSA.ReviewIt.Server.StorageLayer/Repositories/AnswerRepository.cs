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
    public class AnswerRepository : IRepository<AnswerDTO> {
        private readonly EFContext _context;

        public AnswerRepository(EFContext context) {
            _context = context;
        }
        public async Task<int> CreateAsync(AnswerDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.Answer.SingleAsync(a => a.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<AnswerDTO>> ReadAsync() {
            var answers = from answer in _context.Answer
                          select answer.ConvertToDTO();
            return answers;
        }

        public async Task<AnswerDTO> ReadAsync(int id) {
           var answers = from answer in _context.Answer
                          where id == answer.Id
                          select answer.ConvertToDTO();
            return await answers.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(AnswerDTO dto) {
            var orgEntity = _context.Answer.Single(u => u.Id == dto.Id);

            orgEntity.Id = dto.Id;
            orgEntity.Field = dto.Field.ConvertToEntity();
            orgEntity.FieldId = dto.Field.Id;
            orgEntity.ReviewTask = dto.ReviewTask.ConvertToEntity();
            orgEntity.Value = null;

            _context.Update(orgEntity);
            try {
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException) {
                return false;
            }
        }
    }
}
