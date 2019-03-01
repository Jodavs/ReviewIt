using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.UserManager.Interfaces
{
    public interface IUserLogic
    {
        Task<UserDTO> GetById(int id);
        Task<int> Create(UserDTO user);
        Task<bool> Update(UserDTO user, int id);
        Task<bool> Delete(int id);

        Task<bool> AddUserToStudy(ParticipantDTO participant);
        Task<bool> UpdateUserRole(ParticipantDTO participant);
        Task<bool> RemoveUserFromStudy(int studyId, int userId);
        Task<ParticipantDTO> GetUserFromStudy(int studyId, int userId);
    }
}