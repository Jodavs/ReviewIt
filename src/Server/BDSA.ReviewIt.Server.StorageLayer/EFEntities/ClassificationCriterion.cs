using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class ClassificationCriterion : IEntity {
        [Key]
        public int Id { get; set; }
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public int ParentCriterionId { get; set; }
        public ClassificationCriterion ParentCriterion { get; set; }
        public ICollection<Field> Classifications { get; set; }
    }
}
