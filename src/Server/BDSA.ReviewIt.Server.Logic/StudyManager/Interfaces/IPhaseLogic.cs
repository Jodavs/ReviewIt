using System.Collections.Generic;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IPhaseLogic
    {
        Task<IEnumerable<PhaseDTO>> GetAllForStudy(int studyId);
        Task<PhaseDTO> GetById(int id);
        Task<int> Create(PhaseDTO phase);
        Task<bool> Update(PhaseDTO phase);
        Task<bool> Delete(int id);

        Task<bool> StartPhase(PhaseDTO phase);
    }
}