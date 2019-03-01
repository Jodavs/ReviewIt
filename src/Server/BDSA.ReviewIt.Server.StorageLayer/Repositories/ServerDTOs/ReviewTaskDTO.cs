using System;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using System.Collections.Generic;

namespace ServerDTOs.ServerDTOs {
    public class ReviewTaskDTO : IDTO {
        public int Id { get; set; }
        public int TaskDelegationId { get; set; }
        public UserDTO User { get; set; }
        public bool IsSubmitted { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
    }
}