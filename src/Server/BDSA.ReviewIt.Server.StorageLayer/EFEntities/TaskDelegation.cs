using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities
{
    public class TaskDelegation : IEntity
    {
        public int Id { get; set; }
        public int PhaseId { get; set; }
        public Phase Phase { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
        public IList<ReviewTask> Tasks { get; set; }
    }
}
