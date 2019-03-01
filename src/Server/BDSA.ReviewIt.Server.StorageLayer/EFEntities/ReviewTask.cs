using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class ReviewTask : IEntity {
        [Key]
        public int Id { get; set; }
        public int TaskDelegationId { get; set; }
        public TaskDelegation TaskDelegation { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsSubmitted { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
