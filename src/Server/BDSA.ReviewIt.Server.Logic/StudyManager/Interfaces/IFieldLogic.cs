using System.Collections.Generic;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IFieldLogic
    {
        Task<IEnumerable<FieldDTO>> GetAllForStudy(int studyId);
        Task<FieldDTO> GetById(int id);
        Task<int> Create(FieldDTO field);
        Task<bool> Update(FieldDTO field, int id);
        Task<bool> Delete(int id);
    }
}