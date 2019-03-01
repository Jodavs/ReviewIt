using System;
using System.Collections.Generic;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class TaskRepoTests {

        private readonly IRepository<ReviewTaskDTO> _repository;

        public TaskRepoTests()
        {
            var context = new EFContext();
            _repository = new TaskRepository(context);
            context.PurgeData();
        }


        [Fact]
        public void Delete_valid_ReviewTaskDTO()
        {
            var result = _repository.DeleteAsync(1).Result;
            Assert.True(result > 0);
        }

        [Fact]
        public void Delete_invalid_ReviewTaskDTO()
        {
            Assert.ThrowsAsync<NullReferenceException>(() => _repository.DeleteAsync(int.MaxValue));
        }

        [Fact]
        public void Read_multiple_valid_ReviewTaskDTO() {
            Assert.Equal(8, _repository.ReadAsync().Result.Count());
        }

    }
}
