using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class TaskDelegationRepoTests {
        private IRepository<TaskDelegationDTO> _taskRepo;

        public TaskDelegationRepoTests() {
            EFContext efContext = new EFContext();
            _taskRepo = new TaskDelegationRepository(efContext);
            efContext.PurgeData();
        }

        private static TaskDelegationDTO taskDelegation = new TaskDelegationDTO {
            Tasks = new List<ReviewTaskDTO> {
                new ReviewTaskDTO {
                    User = new UserDTO {
                        Name = "user1"
                    }
                },
                new ReviewTaskDTO {
                    User = new UserDTO {
                        Name = "user2"
                    }
                }
            }
        };

        [Fact]
        public void Delete_valid_TaskDelegationDTO() {
            // Act
            var deleted = _taskRepo.DeleteAsync(1).Result;

            var readDeletedTaskDelegation = _taskRepo.ReadAsync(1).Result;

            // Assert
            Assert.Null(readDeletedTaskDelegation);
            Assert.True(deleted > 0);
        }

        [Fact]
        public void Delete_invalid_TaskDelegationDTO() {
            // Act
            var deleted = _taskRepo.DeleteAsync(700).Result;

            var readDeletedTaskDelegation = _taskRepo.ReadAsync(700).Result;

            // Assert
            Assert.Null(readDeletedTaskDelegation);
            Assert.Equal(0, deleted);
        }

        [Fact]
        public void Update_valid_TaskDelegationDTO() {
            // Act
            var dto = _taskRepo.ReadAsync(1).Result;
            var updated = _taskRepo.UpdateAsync(dto).Result;

            Assert.True(updated);
        }

        [Fact]
        public void Update_invalid_TaskDelegationDTO() {
            Assert.True(false);
        }

        [Fact]
        public void Read_valid_TaskDelegationDTO() {
            // Act
            var read = _taskRepo.ReadAsync(1).Result;

            // Assert
            Assert.Equal(1, read.Id);
        }

        [Fact]
        public void Read_invalid_TaskDelegationDTO() {
            Assert.True(false);
        }

        [Fact]
        public void Read_multiple_valid_TaskDelegationDTO() {
            // Act
            var taskDelegations = _taskRepo.ReadAsync().Result;

            // Assert
            Assert.True(taskDelegations.Count() > 0);
        }
    }
}
