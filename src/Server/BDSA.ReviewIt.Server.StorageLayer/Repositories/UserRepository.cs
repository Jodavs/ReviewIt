using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.Repositories {
    public class UserRepository : IRepository<UserDTO> {
        private readonly EFContext _context;

        public UserRepository(EFContext context) {
            _context = context;
        }

        public async Task<int> CreateAsync(UserDTO dto) {
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

        public async Task<IQueryable<UserDTO>> ReadAsync() {
            return from user in _context.User
                   select user.ConvertToDTO();
        }

        public async Task<UserDTO> ReadAsync(int id) {
            return await (from user in _context.User
                          where user.Id == id
                          select user.ConvertToDTO()).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(UserDTO dto) {
            var orgEntity = await _context.User.SingleAsync(u => u.Id == dto.Id);
            //orgEntity.Id = dto.Id;
            //orgEntity.Name = dto.Name;
            //orgEntity.Password = dto.Password;
            //_context.Update(orgEntity);
            orgEntity.UpdateProperties(dto.ConvertToEntity());
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
