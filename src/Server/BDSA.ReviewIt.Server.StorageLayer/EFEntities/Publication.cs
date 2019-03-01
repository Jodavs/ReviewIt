using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class Publication : IEntity {
        [Key]
        public int Id { get; set; }
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public bool Active { get; set; }
        public ICollection<Data> Data { get; set; }
    }
}
