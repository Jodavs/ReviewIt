using System.ComponentModel.DataAnnotations;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities
{
    public class UserPhaseParticipant : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int PhaseId { get; set; }
        public Phase Phase { get; set; }

    }
}