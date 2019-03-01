using System.Collections.Generic;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IExclusionLogic
    {
        Task<IEnumerable<ExclusionCriterionDTO>> GetAllForStudy(int studyId);
        Task<ExclusionCriterionDTO> GetById(int id);
        Task<int> Create(ExclusionCriterionDTO exclusion);
        Task<bool> Update(ExclusionCriterionDTO exclusion);
        Task<bool> Delete(int id);
    }
}