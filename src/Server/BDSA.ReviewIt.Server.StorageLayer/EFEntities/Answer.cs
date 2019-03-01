using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class Answer : IEntity {
        [Key]
        public int Id { get; set; }
        public int ReviewTaskId { get; set; }
        public ReviewTask ReviewTask { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
        public string Value { get; set; }
    }
}
