using System;
using System.Collections.Generic;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class StudyDTO : IDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PhaseDTO ActivePhase { get; set; }
        public ICollection<ExclusionCriterionDTO> ExclusionCriteria { get; set; }
        public ICollection<ClassificationCriterionDTO> ClassificationCriteria { get; set; }
        public ICollection<ParticipantDTO> Users { get; set; }
        public ICollection<PublicationDTO> Publications { get; set; }
        public ICollection<PhaseDTO> Phases { get; set; }
        
    }
}