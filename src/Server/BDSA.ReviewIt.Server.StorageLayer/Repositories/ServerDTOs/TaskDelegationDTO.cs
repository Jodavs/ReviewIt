using System.Collections.Generic;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.ServerDTOs {
    public class TaskDelegationDTO : IDTO {
        public int Id { get; set; }
        public int PhaseId { get; set; }
        public PublicationDTO Publication { get; set; }
        public ICollection<ReviewTaskDTO> Tasks { get; set; }
    }
}