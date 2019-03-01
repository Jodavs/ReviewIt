using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.LogicDTOs;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IStudyLogic
    {
        Task<IEnumerable<StudyDTO>> GetAll();
        Task<StudyDTO> GetById(int id);
        Task<int> Create(StudyDTO study);
        Task<bool> Update(StudyDTO study, int id);
        Task<bool> Delete(int id);

        Task<StudyStatusDTO> GetStudyStatus(int studyId);
    }
}