using BDSA.ReviewIt.Server.Logic.TaskManager;
using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Moq;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDSA.ReviewIt.Server.Logic.Tests.Test {
    public class TaskLogicIntegrationTest {
        private IRepository<ReviewTaskDTO> _taskRepo;
        private IRepository<AnswerDTO> _answerRepo;
        private IRepository<UserDTO> _userRepo;
        private IRepository<StudyDTO> _studyRepo;
        private IRepository<TaskDelegationDTO> _taskDelegationRepository;
        private IRepository<PhaseDTO> _phaseRepository;
        private IRepository<FieldDTO> _fieldRepository;

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
            }
        };

        public TaskLogicIntegrationTest() {
            var efContext = new EFContext();
            _taskRepo = new TaskRepository(efContext);
            _userRepo = new UserRepository(efContext);
            _studyRepo = new StudyRepository(efContext);
            _answerRepo = new AnswerRepository(efContext);
            _phaseRepository = new PhaseRepository(efContext);
            _fieldRepository = new FieldRepository(efContext);
            _taskDelegationRepository = new TaskDelegationRepository(efContext);

            efContext.PurgeData();
        }

        [Fact(DisplayName = "[TaskLogic] GetUserTasks | Success")]
        public async Task Test_Get_User_Tasks_Success_Integration() {

            var taskLogic = new TaskLogic(_taskRepo, _fieldRepository, _answerRepo, _userRepo, _studyRepo, _taskDelegationRepository, _phaseRepository);

            // Act
            var userTasks = await taskLogic.GetUserTasks(study1.Id, 16);

            // Assert
            Assert.Equal(1, userTasks.Count()); // There's two tasks, but only one for this user
            Assert.Equal(11, userTasks.First().Id);
        }

        [Fact(DisplayName = "[TaskLogic] GetUserTasks | No Tasks")]
        public async Task Test_Get_User_Tasks_No_Tasks_Integration() {

            var taskLogic = new TaskLogic(_taskRepo, _fieldRepository, _answerRepo, _userRepo, _studyRepo, _taskDelegationRepository, _phaseRepository);

            // Act
            var userTasks = await taskLogic.GetUserTasks(study1.Id, 9); // Not a user with any tasks

            // Assert
            Assert.Equal(0, userTasks.Count());
        }

        [Fact(DisplayName = "[TaskLogic] SubmitTask | Success (true)")]
        public async Task Test_Submit_Task_Success_Integration() {
            var taskLogic = new TaskLogic(_taskRepo, _fieldRepository, _answerRepo, _userRepo, _studyRepo, _taskDelegationRepository, _phaseRepository);

            // Act
            var submitted = await taskLogic.SubmitTask(task1);

            // Assert
            Assert.True(submitted);
            Assert.True(task1.IsSubmitted);
        }

        [Fact(DisplayName = "[TaskLogic] SubmitTask | Failure (false)")]
        public async Task Test_Submit_Task_Failure_Integration() {
            // Arrange
            var taskLogic = new TaskLogic(_taskRepo, _fieldRepository, _answerRepo, _userRepo, _studyRepo, _taskDelegationRepository, _phaseRepository);

            // Act
            var submitted_null = await taskLogic.SubmitTask(null); // Null
            var submitted_empty_task = await taskLogic.SubmitTask(new ReviewTaskDTO()); // Empty task

            // Assert
            Assert.False(submitted_null);
            Assert.False(submitted_empty_task); // TODO We would expect it to return false, but the program crashes
        }
    }
}
