using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.ServerDTOs {
    public class ParticipantDTO : IDTO {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public int StudyId { get; set; }
        public ParticipantRole Role { get; set; }
    }
}
