using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.Repositories.ServerDTOs
{
    public class UserPhaseParticipantDTO : IDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PhaseDTO Phase { get; set; }
    }
}