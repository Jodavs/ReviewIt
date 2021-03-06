﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities {
    public class User : IEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<UserPhaseParticipant> PhaseParticipants { get; set; }
    }
}
