using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;

namespace BDSA.ReviewIt.Server.StorageLayer {
    public static class CreateEFTestData {
        public static void CreateTestData(EFContext context) {
            var study = new Study {
                Id = 0,
                Description = "Study created for testing",
                Name = "TestStudy"
            };

            var publications = new List<Publication>
            {
                new Publication
                {
                    Active = true,
                    StudyId = 1,
                    Study = study,
                },
                new Publication
                {
                    Active = true,
                    StudyId = 1,
                    Study = study,
                },
                new Publication
                {
                    Active = true,
                    StudyId = 1,
                    Study = study,
                },
                new Publication
                {
                    Active = true,
                    StudyId = 1,
                    Study = study,
                }
            };

            var fields = new[]
            {
                new Field
                {
                    Name = "Title",
                    Description = "The title of the publication",
                    Type = FieldType.STRING
                },
                new Field
                {
                    Name = "Year",
                    Description = "Publication year",
                    Type = FieldType.INT
                },
                new Field
                {
                    Name = "Author",
                    Description = "",
                    Type = FieldType.STRING
                },
                new Field
                {
                    Name = "IsTheoretical",
                    Description = "Indicates whether the the publication is a theoretical text",
                    Type = FieldType.BOOL
                },
                new Field
                {
                    Name = "Group A",
                    Description = "Group for classification criterion",
                    Type = FieldType.BOOL
                },
                new Field
                {
                    Name = "Group B",
                    Description = "Group for classification criterion",
                    Type = FieldType.BOOL
                },
                new Field
                {
                    Name = "Group C",
                    Description = "Group for classification criterion",
                    Type = FieldType.BOOL
                }
            };

            var data = new Data[]
            {
                // Title (pub 0)
                new Data
                {
                    FieldId = 1,
                    Field = fields[0],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "Stable theories"
                },
                // Title (pub 1)
                new Data
                {
                    FieldId = 1,
                    Field = fields[0],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "Note on a min-max problem of Leo Moser"
                },
                // Title (pub 2)
                new Data
                {
                    FieldId = 1,
                    Field = fields[0],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "Finite diagrams stable in power"
                },
                // Title (pub 3)
                new Data
                {
                    FieldId = 1,
                    Field = fields[0],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "On theories T categorical in |T|"
                },
                // Year (pub 0)
                new Data
                {
                    FieldId = 2,
                    Field = fields[1],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "1969"
                },
                // Year (pub 1)
                new Data
                {
                    FieldId = 2,
                    Field = fields[1],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "1969"
                },
                // Year (pub 2)
                new Data
                {
                    FieldId = 2,
                    Field = fields[1],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "1970"
                },
                // Year (pub 3)
                new Data
                {
                    FieldId = 2,
                    Field = fields[1],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "1970"
                },
                // Author (pub 0)
                new Data
                {
                    FieldId = 3,
                    Field = fields[2],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "Shelah, Saharon"
                },
                // Author (pub 1)
                new Data
                {
                    FieldId = 3,
                    Field = fields[2],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "Shelah, Saharon"
                },
                // Author (pub 2)
                new Data
                {
                    FieldId = 3,
                    Field = fields[2],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "Shelah, Saharon"
                },
                // Author (pub 3)
                new Data
                {
                    FieldId = 3,
                    Field = fields[2],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "Shelah, Saharon"
                },
                // IsTheoretical (pub 0)
                new Data
                {
                    FieldId = 4,
                    Field = fields[3],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "true"
                },
                // IsTheoretical (pub 1)
                new Data
                {
                    FieldId = 4,
                    Field = fields[3],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "true"
                },
                // IsTheoretical (pub 2)
                new Data
                {
                    FieldId = 4,
                    Field = fields[3],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "false"
                },
                // IsTheoretical (pub 3)
                new Data
                {
                    FieldId = 4,
                    Field = fields[3],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "false"
                },
                // Group A (pub 0)
                new Data
                {
                    FieldId = 5,
                    Field = fields[4],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "true"
                },
                // Group A (pub 1)
                new Data
                {
                    FieldId = 5,
                    Field = fields[4],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "false"
                },
                // Group A (pub 2)
                new Data
                {
                    FieldId = 5,
                    Field = fields[4],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "false"
                },
                // Group A (pub 3)
                new Data
                {
                    FieldId = 5,
                    Field = fields[5],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "true"
                },
                // Group B (pub 0)
                new Data
                {
                    FieldId = 6,
                    Field = fields[5],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "false"
                },
                // Group B (pub 1)
                new Data
                {
                    FieldId = 6,
                    Field = fields[5],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "true"
                },
                // Group B (pub 2)
                new Data
                {
                    FieldId = 6,
                    Field = fields[5],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "false"
                },
                // Group B (pub 3)
                new Data
                {
                    FieldId = 6,
                    Field = fields[5],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "true"
                },
                // Group C (pub 0)
                new Data
                {
                    FieldId = 7,
                    Field = fields[6],
                    PublicationId = 1,
                    Publication = publications[0],
                    Value = "false"
                },
                // Group C (pub 1)
                new Data
                {
                    FieldId = 7,
                    Field = fields[6],
                    PublicationId = 2,
                    Publication = publications[1],
                    Value = "false"
                },
                // Group C (pub 2)
                new Data
                {
                    FieldId = 7,
                    Field = fields[6],
                    PublicationId = 3,
                    Publication = publications[2],
                    Value = "false"
                },
                // Group C (pub 3)
                new Data
                {
                    FieldId = 7,
                    Field = fields[6],
                    PublicationId = 4,
                    Publication = publications[3],
                    Value = "true"
                },
            };

            var exclusionCriteria = new ExclusionCriterion[]
            {
                new ExclusionCriterion
                {
                    FieldId = 4,
                    Field = fields[3],
                    Condition = ExclusionCondition.BOOL,
                    Operator = "true",
                    StudyId = 1,
                    Study = study
                }
            };

            var users = new User[]
            {
                new User
                {
                    Name = "Antoniusman",
                    Password = "1234"
                },
                new User
                {
                    Name = "Jacob",
                    Password = "1234"
                },
                new User
                {
                    Name = "Jonleif",
                    Password = "1234"
                },
                new User
                {
                    Name = "Jannik",
                    Password = "1234"
                },
                new User
                {
                    Name = "Lucas",
                    Password = "1234"
                },
            };

            var participants = new Participant[]
            {
                new Participant
                {
                    UserId = 1,
                    User = users[0],
                    StudyId = 1,
                    Study = study,
                    Role = ParticipantRole.TEAM_MANAGER
                },
                new Participant
                {
                    UserId = 2,
                    User = users[1],
                    StudyId = 1,
                    Study = study,
                    Role = ParticipantRole.RESEARCHER
                },
                new Participant
                {
                    UserId = 3,
                    User = users[2],
                    StudyId = 1,
                    Study = study,
                    Role = ParticipantRole.RESEARCHER
                },
                new Participant
                {
                    UserId = 4,
                    User = users[3],
                    StudyId = 1,
                    Study = study,
                    Role = ParticipantRole.RESEARCHER
                },
                new Participant
                {
                    UserId = 5,
                    User = users[4],
                    StudyId = 1,
                    Study = study,
                    Role = ParticipantRole.RESEARCHER
                },
            };

            var phaseParticipants = new UserPhaseParticipant[]
            {
                new UserPhaseParticipant
                {
                    PhaseId = 1,
                    UserId = 1,
                },
                new UserPhaseParticipant
                {
                    PhaseId = 1,
                    UserId = 2,
                },
                new UserPhaseParticipant
                {
                    PhaseId = 1,
                    UserId = 3,
                },
                new UserPhaseParticipant
                {
                    PhaseId = 1,
                    UserId = 4,
                },
                new UserPhaseParticipant
                {
                    PhaseId = 1,
                    UserId = 5,
                },
            };

            var phase = new Phase {
                Automatic = true,
                ConflictManagerId = 1,
                ConflictManager = users[0],
                DisplayFields = new List<Field>
                {
                    fields[0],
                    fields[1],
                    fields[2]
                },
                InputFields = new List<Field>
                {
                    fields[3]
                },
                OverlapPercentage = 200,
                Purpose = "Decide which publications are theoretical",
                StudyId = 1,
                Study = study,
                UserParticipants = new List<UserPhaseParticipant>
                {
                    phaseParticipants[0],
                    phaseParticipants[1],
                    phaseParticipants[2],
                    phaseParticipants[3],
                    phaseParticipants[4]
                }
            };

            var taskDelegations = new TaskDelegation[]
            {
                new TaskDelegation
                {
                    PhaseId = 1,
                    Phase = phase,
                    PublicationId = 1,
                    Publication = publications[0]
                },
                new TaskDelegation
                {
                    PhaseId = 1,
                    Phase = phase,
                    PublicationId = 2,
                    Publication = publications[1]
                },
                new TaskDelegation
                {
                    PhaseId = 1,
                    Phase = phase,
                    PublicationId = 3,
                    Publication = publications[2]
                },
                new TaskDelegation
                {
                    PhaseId = 1,
                    Phase = phase,
                    PublicationId = 4,
                    Publication = publications[3]
                },
            };

            var tasks = new ReviewTask[][]
            {
                new ReviewTask[] {
                new ReviewTask
                {
                    TaskDelegationId = 1,
                    TaskDelegation = taskDelegations[0],
                    UserId = 1,
                    User = users[0],
                    IsSubmitted = false
                },
                new ReviewTask
                {
                    TaskDelegationId = 1,
                    TaskDelegation = taskDelegations[0],
                    UserId = 2,
                    User = users[1],
                    IsSubmitted = false
                },
},
                new ReviewTask[] {
                new ReviewTask
                {
                    TaskDelegationId = 2,
                    TaskDelegation = taskDelegations[1],
                    UserId = 1,
                    User = users[0],
                    IsSubmitted = false
                },
                new ReviewTask
                {
                    TaskDelegationId = 2,
                    TaskDelegation = taskDelegations[1],
                    UserId = 2,
                    User = users[1],
                    IsSubmitted = false
                },},
                new ReviewTask[] {
                new ReviewTask
                {
                    TaskDelegationId = 3,
                    TaskDelegation = taskDelegations[2],
                    UserId = 1,
                    User = users[0],
                    IsSubmitted = false
                },
                new ReviewTask
                {
                    TaskDelegationId = 3,
                    TaskDelegation = taskDelegations[2],
                    UserId = 2,
                    User = users[1],
                    IsSubmitted = false
                },},
                new ReviewTask[] {
                new ReviewTask
                {
                    TaskDelegationId = 4,
                    TaskDelegation = taskDelegations[3],
                    UserId = 1,
                    User = users[0],
                    IsSubmitted = false
                },
                new ReviewTask
                {
                    TaskDelegationId = 4,
                    TaskDelegation = taskDelegations[3],
                    UserId = 2,
                    User = users[1],
                    IsSubmitted = false
                },}
            };

            for (int i = 0; i<taskDelegations.Length; i++) {
                taskDelegations[i].Tasks = tasks[i];
            }

            var answers = new[] {
                new Answer
                {
                    FieldId = 4,
                    Field = fields[3],
                    ReviewTaskId = 1,
                    ReviewTask = tasks[0][1],
                    Value = "true"
                },
                new Answer
                {
                    FieldId = 4,
                    Field = fields[3],
                    ReviewTaskId = 1,
                    ReviewTask = tasks[0][1],
                    Value = "false"
                }
            };

            ClassificationCriterion classificationCriterion = new ClassificationCriterion {
                Study = study,
                StudyId = 1,
                ParentCriterionId = 1
            };

            context.Study.Add(study);
            context.SaveChanges();

            context.Publication.AddRange(publications.AsEnumerable());
            context.SaveChanges();

            context.Field.AddRange(fields);
            context.SaveChanges();

            context.Data.AddRange(data);
            context.SaveChanges();

            context.ExclusionCriterion.AddRange(exclusionCriteria);
            context.SaveChanges();

            context.User.AddRange(users);
            context.SaveChanges();

            context.Participant.AddRange(participants);
            context.SaveChanges();

            context.Phase.Add(phase);
            context.UserPhaseParticipant.AddRange(phaseParticipants);
            context.SaveChanges();

            context.TaskDelegation.AddRange(taskDelegations);
            foreach (ReviewTask[] t in tasks) {
                context.ReviewTask.AddRange(t);
            }
            context.SaveChanges();

            context.AddRange(answers);
            context.SaveChanges();

            context.ClassificationCriterion.Add(classificationCriterion);
            context.SaveChanges();
        }
    }
}
