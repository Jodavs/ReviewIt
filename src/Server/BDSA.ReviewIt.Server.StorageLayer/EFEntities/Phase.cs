using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class Phase : IEntity {
        [Key]
        public int Id { get; set; }
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public string Purpose { get; set; }
        public ICollection<UserPhaseParticipant> UserParticipants { get; set; }
        public ICollection<TaskDelegation> TaskDelegations { get; set; }
        public ICollection<Field> DisplayFields { get; set; }
        public ICollection<Field> InputFields { get; set; }
        public int OverlapPercentage { get; set; } // Delegation specification
        public bool Automatic { get; set; }
        public int ConflictManagerId { get; set; }
        public User ConflictManager { get; set; }

        // Todo : TaskDelegationSpecification
    }
}
