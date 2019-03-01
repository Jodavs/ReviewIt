using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System.Linq;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public class PublicationLogic : IPublicationLogic
    {

        private readonly IRepository<PublicationDTO> _publicatioRepository;

        public PublicationLogic(IRepository<PublicationDTO> publicationRepository)
        {
            _publicatioRepository = publicationRepository;
        }


        public async Task<IEnumerable<PublicationDTO>> GetAllForStudy(int studyId)
        {
            var phases = await _publicatioRepository.ReadAsync();
            var phasesForStudy = phases.Where(p => p.StudyId == studyId);
            return phasesForStudy;
        }

        public Task<PublicationDTO> GetById(int publicationId)
        {
            return _publicatioRepository.ReadAsync(publicationId);
        }
    }
}