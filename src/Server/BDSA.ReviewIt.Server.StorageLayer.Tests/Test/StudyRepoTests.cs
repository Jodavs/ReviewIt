using System;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class StudyRepoTests
    {

        private readonly IRepository<StudyDTO> _repository;

        public StudyRepoTests()
        {
            var context = new EFContext();
            _repository = new StudyRepository(context);
            context.PurgeData();
        }

        [Fact]
        public void Create_StudyDTO_valid_input()
        {
            var result = _repository.CreateAsync(new StudyDTO()).Result;
            Assert.IsType<int>(result);
        }


        [Fact]
        public void Delete_valid_StudyDTO() {
            Assert.True(_repository.DeleteAsync(1).Result > 1);
        }

        [Fact]
        public void Delete_invalid_StudyDTO() {
            Assert.ThrowsAsync<NullReferenceException>(() => _repository.DeleteAsync(int.MaxValue));
        }
   

        [Fact]
        public void Read_multiple_valid_StudyDTO() {
            Assert.Equal(1, _repository.ReadAsync().Result.Count());
        }
    }
}
