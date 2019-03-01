using BDSA.ReviewIt.Server.Logic.TaskManager;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Moq;
using ServerDTOs.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDSA.ReviewIt.Server.Logic.Tests.Test {
    public class TaskLogicTest {
        private readonly Mock<IRepository<ReviewTaskDTO>> _mockTaskRepo;
        private readonly Mock<IRepository<UserDTO>> _mockUserRepo;
        private readonly Mock<IRepository<StudyDTO>> _mockStudyRepo;
        private readonly Mock<IRepository<AnswerDTO>> _mockAnswerRepo;
        private readonly Mock<IRepository<PhaseDTO>> _mockPhaseRepo;
        private readonly Mock<IRepository<TaskDelegationDTO>> _mockTaskDelegationRepo;
        private readonly Mock<IRepository<FieldDTO>> _mockFieldRepo;
        /*private static TaskDelegationDTO taskDel1 = new TaskDelegationDTO {
            Id = 72,
            Phase = phase1,
            Publication = pub1,
            Tasks = new List<ReviewTaskDTO> { task1 }
        };

        private static PhaseDTO phase1 = new PhaseDTO {
            Id = 21,
            Study = study1,
            ConflictManager = user1,
            DisplayFields = null,
            InputFields = null,
            IsAutomatic = true,
            OverlapPercentage = 50,
            Participants = null, // TODO What?
            Purpose = "Test Purpose",
            TaskDelegations = new List<TaskDelegationDTO> { taskDel1 } // TODO Dont have 2 refs
        };

        private static PublicationDTO pub1 = new PublicationDTO {
            Id = 46,
            Active = true,
            Data = null, // TODO What?
            Study = study1
        };

        private static ReviewTaskDTO task1 = new ReviewTaskDTO {
            Id = 25,
            Answers = new List<AnswerDTO> { answer1 },
            IsSubmitted = false,
            TaskDelegation = taskDel1,
            User = user1
        };

        private static AnswerDTO answer1 = new AnswerDTO {
            Id = 24,
            Field = field1,
            ReviewTask = task1,
            Value = new IntValue(42)
        };

        private static FieldDTO field1 = new FieldDTO {
            Id = 26,
            Name = "Test Field Name 1",
            Description = "Test Field Description 1",
            Type = FieldType.INT
        };

        private static UserDTO user1 = new UserDTO {
            Id = 72,
            Name = "Jerry"
        };

        private static ParticipantDTO participant1 = new ParticipantDTO {
            Id = 54,
            Role = ParticipantRole.RESEARCHER,
            Study = study1,
            User = user1
        };

        private static StudyDTO study1 = new StudyDTO {
            Id = 7,
            Name = "Study 1 Test Name",
            Description = "Study 1 Test Description",
            Users = new List<ParticipantDTO> { participant1 },
            ActivePhase = phase1,
            Phases = new List<PhaseDTO> { phase1 },

            ExclusionCriteria = null,
            ClassificationCriteria = null,
            Publications = null
        };*/


        private static FieldDTO field1 = new FieldDTO {
            Id = 2
        };

        private static PhaseDTO phase1 = new PhaseDTO {
            TaskDelegations = new List<TaskDelegationDTO> {
                new TaskDelegationDTO {
                    Tasks = new List<ReviewTaskDTO> {
                        new ReviewTaskDTO {
                            Id = 11,
                            User = new UserDTO {
                                Id = 16
                            }
                        },
                        new ReviewTaskDTO {
                            Id = 22,
                            User = new UserDTO {
                                Id = 48
                            }
                        }
                    }
                }
            },
            InputFields = new List<FieldDTO> { field1 }
        };

        private static StudyDTO study1 = new StudyDTO {
            Id = 4,
            ActivePhase = phase1,
            ExclusionCriteria = new List<ExclusionCriterionDTO>()
        };

        private static ReviewTaskDTO task1 = new ReviewTaskDTO {
            Id = 21,
            Answers = new List<AnswerDTO> {
                new AnswerDTO {
                    Id = 2,
                    Field = field1,
                    Value = new IntValue(84)
                }
            },
            TaskDelegationId = 78,
            /*TaskDelegation = new TaskDelegationDTO {
                Id = 78,
                Phase = phase1,
                Publication = new PublicationDTO {
                    Data = new List<DataDTO> {
                        new DataDTO {
                            Field = field1
                        }
                    }
                }
            }*/
        };

        public TaskLogicTest() {
            _mockTaskRepo = new Mock<IRepository<ReviewTaskDTO>>();
            _mockUserRepo = new Mock<IRepository<UserDTO>>();
            _mockStudyRepo = new Mock<IRepository<StudyDTO>>();
            _mockAnswerRepo = new Mock<IRepository<AnswerDTO>>();
            _mockFieldRepo = new Mock<IRepository<FieldDTO>>();
            _mockTaskDelegationRepo = new Mock<IRepository<TaskDelegationDTO>>();
            _mockPhaseRepo = new Mock<IRepository<PhaseDTO>>();

            //task1.TaskDelegation.Tasks = new List<ReviewTaskDTO> { task1 };
            phase1.StudyId = study1.Id;
        }

        [Fact(DisplayName = "[TaskLogic] GetUserTasks | Success")]
        public void Test_Get_User_Tasks_Success() {
            // Arrange
            _mockStudyRepo.Setup(r => r.ReadAsync(study1.Id)).ReturnsAsync(study1);

            var taskLogic = new TaskLogic(_mockTaskRepo.Object, _mockFieldRepo.Object, _mockAnswerRepo.Object, _mockUserRepo.Object, _mockStudyRepo.Object, _mockTaskDelegationRepo.Object, _mockPhaseRepo.Object);

            // Act
            var userTasks = taskLogic.GetUserTasks(study1.Id, 16).Result;

            // Assert
            Assert.Equal(1, userTasks.Count()); // There's two tasks, but only one for this user
            Assert.Equal(11, userTasks.First().Id);
        }

        [Fact(DisplayName = "[TaskLogic] GetUserTasks | No Tasks")]
        public void Test_Get_User_Tasks_No_Tasks() {
            // Arrange
            _mockStudyRepo.Setup(r => r.ReadAsync(study1.Id)).ReturnsAsync(study1);

            var taskLogic = new TaskLogic(_mockTaskRepo.Object, _mockFieldRepo.Object, _mockAnswerRepo.Object, _mockUserRepo.Object, _mockStudyRepo.Object, _mockTaskDelegationRepo.Object, _mockPhaseRepo.Object);

            // Act
            var userTasks = taskLogic.GetUserTasks(study1.Id, 9).Result; // Not a user with any tasks

            // Assert
            Assert.Equal(0, userTasks.Count());
        }

        [Fact(DisplayName = "[TaskLogic] SubmitTask | Success (true)")]
        public void Test_Submit_Task_Success() {
            // Arrange
            _mockTaskRepo.Setup(r => r.ReadAsync(task1.Id)).ReturnsAsync(task1);
            _mockTaskRepo.Setup(r => r.UpdateAsync(task1)).ReturnsAsync(true);
            _mockStudyRepo.Setup(r => r.ReadAsync(study1.Id)).ReturnsAsync(study1);

            var taskLogic = new TaskLogic(_mockTaskRepo.Object, _mockFieldRepo.Object, _mockAnswerRepo.Object, _mockUserRepo.Object, _mockStudyRepo.Object, _mockTaskDelegationRepo.Object, _mockPhaseRepo.Object);

            // Act
            var submitted = taskLogic.SubmitTask(task1).Result;

            // Assert
            Assert.True(submitted);
            Assert.True(task1.IsSubmitted);
        }

        [Fact(DisplayName = "[TaskLogic] SubmitTask | Failure (false)")]
        public void Test_Submit_Task_Failure() {
            // Arrange
            var taskLogic = new TaskLogic(_mockTaskRepo.Object, _mockFieldRepo.Object, _mockAnswerRepo.Object, _mockUserRepo.Object, _mockStudyRepo.Object, _mockTaskDelegationRepo.Object, _mockPhaseRepo.Object);

            // Act
            var submitted_null = taskLogic.SubmitTask(null).Result; // Null
            var submitted_empty_task = taskLogic.SubmitTask(new ReviewTaskDTO()).Result; // Empty task

            // Assert
            Assert.False(submitted_null);
            Assert.False(submitted_empty_task); // TODO We would expect it to return false, but the program crashes
        }
    }
}
