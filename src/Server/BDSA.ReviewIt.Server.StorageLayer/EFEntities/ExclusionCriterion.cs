using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Conditions;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class ExclusionCriterion : IEntity {
        [Key]
        public int Id { get; set; }
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
        public ExclusionCondition Condition { get; set; }
        public string Operator { get; set; }
    }
}
