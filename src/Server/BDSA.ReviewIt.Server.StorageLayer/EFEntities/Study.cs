using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class Study : IEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Phase ActivePhase { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Publication> Publications { get; set; }
        public ICollection<ExclusionCriterion> ExclusionCriteria { get; set; }
        public ICollection<ClassificationCriterion> ClassificationCriteria { get; set; }
        public ICollection<Phase> Phases { get; set; }
    }
}
