using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System.Linq;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public class ClassificationLogic : IClassificationLogic
    {
        private readonly IRepository<ClassificationCriterionDTO> _classificationRepo;
        private readonly IRepository<StudyDTO> _studyRepo;

        public ClassificationLogic(IRepository<ClassificationCriterionDTO> classificationRepo, IRepository<StudyDTO> studyRepo)
        {
            _classificationRepo = classificationRepo;
            _studyRepo = studyRepo;
        }

        public async Task<IEnumerable<ClassificationCriterionDTO>> GetAllForStudy(int studyId)
        {
            var getStudy = await _studyRepo.ReadAsync(studyId);

            var getAll = await _classificationRepo.ReadAsync();

            var allForStudy = getAll.Where(d => d.StudyId == getStudy.Id);

            return allForStudy;
        }

        public async Task<ClassificationCriterionDTO> GetById(int id)
        {
            var get = await _classificationRepo.ReadAsync(id);

            return get;
        }

        public async Task<int> Create(ClassificationCriterionDTO classification)
        {
            var create = await _classificationRepo.CreateAsync(classification);

            return create;
        }

        public async Task<bool> Update(ClassificationCriterionDTO classification, int id)
        {
            var update = await _classificationRepo.UpdateAsync(classification);

            return update;
        }

        public async Task<bool> Delete(int id)
        {
            int delete = await _classificationRepo.DeleteAsync(id);

            return delete != 0;
        }
    }
}