using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class Participant : IEntity {
        public int Id { get; set; } // Not the key
        public int UserId { get; set; }
        public User User { get; set; }
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public ParticipantRole Role { get; set; }
    }
}
