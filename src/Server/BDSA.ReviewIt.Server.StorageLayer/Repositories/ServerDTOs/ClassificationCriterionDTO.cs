using System.Collections.Generic;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class ClassificationCriterionDTO : IDTO {
        public int Id { get; set; }
        public ClassificationCriterionDTO Parent { get; set; }
        public ICollection<FieldDTO> Classifications { get; set; }
        public int StudyId { get; set; }
    }
}