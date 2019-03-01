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
    public class TaskRepository : IRepository<ReviewTaskDTO> {
        private readonly EFContext _context;

        public TaskRepository(EFContext context) {
            _context = context;
        }


        public async Task<int> CreateAsync(ReviewTaskDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = _context.User.Single(u => u.Id == id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<ReviewTaskDTO>> ReadAsync() {
            return from task in _context.ReviewTask
                          select task.ConvertToDTO();
        }

        public async Task<ReviewTaskDTO> ReadAsync(int id) {
            return await (from task in _context.ReviewTask
                          where task.Id == id
                          select task.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ReviewTaskDTO dto) {
            var orgEntity = _context.ReviewTask.Single(u => u.Id == dto.Id);

            List<Answer> answers = new List<Answer>();
            dto.Answers.ToList().ForEach(a => answers.Add(a.ConvertToEntity()));

            orgEntity.Id = dto.Id;
            orgEntity.Answers = answers;
            orgEntity.IsSubmitted = dto.IsSubmitted;
            orgEntity.TaskDelegationId = dto.TaskDelegationId;
            orgEntity.User = dto.User.ConvertToEntity();
            orgEntity.UserId = dto.User.Id;

            _context.Update(orgEntity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException) {
                return false;
            }
        }
    }
}
