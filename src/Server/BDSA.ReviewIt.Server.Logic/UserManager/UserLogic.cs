using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.UserManager.Interfaces;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.UserManager
{
    public class UserLogic : IUserLogic
    {
        private readonly IRepository<ParticipantDTO> _participantRepo;
        private readonly IRepository<UserDTO> _userRepo;
        private readonly IRepository<StudyDTO> _studyRepo;

        public UserLogic(IRepository<ParticipantDTO> participantRepo, IRepository<UserDTO> userRepo, IRepository<StudyDTO> studyRepo)
        {
            _participantRepo = participantRepo;
            _userRepo = userRepo;
            _studyRepo = studyRepo;
        }
        public async Task<UserDTO> GetById(int id)
        {
            var get = await _userRepo.ReadAsync(id);

            return get;
        }

        public async Task<int> Create(UserDTO user)
        {
            var create = await _userRepo.CreateAsync(user);

            return create;
        }

        public async Task<bool> Update(UserDTO user, int id)
        {
            var update = await _userRepo.UpdateAsync(user);

            return update;
        }

        public async Task<bool> Delete(int id)
        {
            var delete = await _userRepo.DeleteAsync(id);

            return delete != 0;
        }

        public async Task<bool> AddUserToStudy(ParticipantDTO participant)
        {
            var add = await _participantRepo.CreateAsync(participant);

            return add != 0;
        }

        public async Task<bool> UpdateUserRole(ParticipantDTO participant)
        {
            var update = await _participantRepo.UpdateAsync(participant);

            return update;
        }

        public async Task<bool> RemoveUserFromStudy(int studyId, int id)
        {
            var all = await _participantRepo.ReadAsync();
            var participant = all.Single(p => 
                            p.StudyId == studyId && 
                            p.User.Id == id);

            int delete = await _participantRepo.DeleteAsync(participant.Id);

            return delete != 0;
        }

        public async Task<ParticipantDTO> GetUserFromStudy(int studyId, int userId)
        {
            var getStudy = await _studyRepo.ReadAsync(studyId);

            var getStudyUsers = getStudy.Users.Single(d => d.Id == userId);

            return getStudyUsers;
        }
    }
}