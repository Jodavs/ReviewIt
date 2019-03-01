using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.LogicDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager {
    public class StudyLogic : IStudyLogic {
        private readonly IRepository<StudyDTO> _studyRepo;

        public StudyLogic(IRepository<StudyDTO> studyRepo) {
            _studyRepo = studyRepo;
        }

        public async Task<IEnumerable<StudyDTO>> GetAll() {
            var getAll = await _studyRepo.ReadAsync();

            return getAll;
        }

        public async Task<StudyDTO> GetById(int id) {
            var get = await _studyRepo.ReadAsync(id);

            return get;
        }

        public Task<int> Create(StudyDTO study) {
            var create = _studyRepo.CreateAsync(study);

            return create;
        }

        public async Task<bool> Update(StudyDTO study, int id) {
            var update = await _studyRepo.UpdateAsync(study);

            return update;
        }

        public async Task<bool> Delete(int id) {
            var delete = await _studyRepo.DeleteAsync(id);

            return delete != 0;
        }

        public async Task<StudyStatusDTO> GetStudyStatus(int studyId) {
            throw new NotImplementedException();
        }
    }
}