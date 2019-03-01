using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer {
    public class EFContext : DbContext {
        public DbSet<Answer> Answer { get; set; }
        public DbSet<ClassificationCriterion> ClassificationCriterion { get; set; }
        public DbSet<Data> Data { get; set; }
        public DbSet<ExclusionCriterion> ExclusionCriterion { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Phase> Phase { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<ReviewTask> ReviewTask { get; set; }
        public DbSet<TaskDelegation> TaskDelegation { get; set; }
        public DbSet<Study> Study { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserPhaseParticipant> UserPhaseParticipant { get; set; }

        public EFContext() {
            Database.EnsureCreated();
        }

        public void PurgeData()
        {
            /*RemoveRange(Answer);
            RemoveRange(ClassificationCriterion);
            RemoveRange(Data);
            RemoveRange(ExclusionCriterion);
            RemoveRange(Field);
            RemoveRange(Participant);
            RemoveRange(Phase);
            RemoveRange(Publication);
            RemoveRange(ReviewTask);
            RemoveRange(TaskDelegation);
            RemoveRange(Study);
            RemoveRange(User);
            RemoveRange(UserPhaseParticipant); 
            SaveChanges();*/
            Database.EnsureDeleted();
            Database.EnsureCreated();
            CreateEFTestData.CreateTestData(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Participant>().HasKey(
                t => new { t.StudyId, t.UserId }
            );
            modelBuilder.Entity<Phase>().HasOne(p => p.Study);
            modelBuilder.Entity<Study>().HasOne(p => p.ActivePhase);
            modelBuilder.Entity<Study>().HasMany(s => s.Phases);
            modelBuilder.Entity<Phase>().HasOne(p => p.ConflictManager);

            modelBuilder.Entity<Phase>().HasMany(p => p.UserParticipants);
            modelBuilder.Entity<User>().HasMany(u => u.PhaseParticipants);
            modelBuilder.Entity<UserPhaseParticipant>().HasOne(upp => upp.Phase);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (optionsBuilder.IsConfigured) return;
            if (_databaseName != null) {
                optionsBuilder.UseSqlite("Filename=./"+_databaseName);
            }
            else {*/

            //}

            optionsBuilder.UseSqlite("Filename=./database.db");
        }
    }
}
