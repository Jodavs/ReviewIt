using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class PhaseDTO : IDTO {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public ICollection<UserPhaseParticipantDTO> Participants { get; set; }
        public ICollection<TaskDelegationDTO> TaskDelegations { get; set; }
        public ICollection<FieldDTO> DisplayFields { get; set; }
        public ICollection<FieldDTO> InputFields { get; set; }
        public int OverlapPercentage { get; set; }
        public bool IsAutomatic { get; set; }
        public UserDTO ConflictManager { get; set; }
        public int StudyId { get; set; }
    }
}