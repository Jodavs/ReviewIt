using ServerDTOs.ServerDTOs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public interface IPublicationLogic
    {
        Task<IEnumerable<PublicationDTO>> GetAllForStudy(int studyId);
        Task<PublicationDTO> GetById(int publicationId);
    }
}