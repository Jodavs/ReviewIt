using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using ServerDTOs.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories {
    public class TaskDelegationRepository : IRepository<TaskDelegationDTO> {
        private readonly EFContext _context;

        public TaskDelegationRepository(EFContext context) {
            _context = context;
        }


        public async Task<int> CreateAsync(TaskDelegationDTO dto) {
            var entity = dto.ConvertToEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(int id) {
            var entity = _context.TaskDelegation.SingleOrDefault(u => u.Id == id);
            if (entity == null) return 0;
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }

        public async Task<IQueryable<TaskDelegationDTO>> ReadAsync() {
            return from task in _context.TaskDelegation
                          select task.ConvertToDTO();
        }

        public async Task<TaskDelegationDTO> ReadAsync(int id) {
            return await (from task in _context.TaskDelegation
                          where task.Id == id
                          select task.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(TaskDelegationDTO dto) {
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
