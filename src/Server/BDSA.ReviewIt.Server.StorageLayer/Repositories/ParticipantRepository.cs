using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BDSA.ReviewIt.Server.StorageLayer.Repositories
{
    public class ParticipantRepository : IRepository<ParticipantDTO>
    {
        private readonly EFContext _context;

        public ParticipantRepository(EFContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CreateAsync(ParticipantDTO dto)
        {
            _context.Participant.Add(dto.ConvertToEntity());
            return await _context.SaveChangesAsync(); 
        }

        public async Task<IQueryable<ParticipantDTO>> ReadAsync()
        {
            return _context.Participant.Select(p => p.ConvertToDTO());
        }

        public async Task<ParticipantDTO> ReadAsync(int id)
        {
            return _context.Participant.First(p => p.Id == id).ConvertToDTO();
        }

        public async Task<bool> UpdateAsync(ParticipantDTO dto)
        {
            _context.Participant.Update(dto.ConvertToEntity());
            return (await _context.SaveChangesAsync()) != 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.Participant.SingleAsync(p => p.Id == id);
            _context.Participant.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}