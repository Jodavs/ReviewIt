using System.Collections.Generic;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IClassificationLogic
    {
        Task<IEnumerable<ServerDTOs.ServerDTOs.ClassificationCriterionDTO>> GetAllForStudy(int studyId);
        Task<ClassificationCriterionDTO> GetById(int id);
        Task<int> Create(ClassificationCriterionDTO classification);
        Task<bool> Update(ClassificationCriterionDTO classification, int id);
        Task<bool> Delete(int id);
    }
}