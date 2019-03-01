using System;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class UserRepoTests {

        private readonly UserDTO _userDto;
        private readonly IRepository<UserDTO> _repository;

        public UserRepoTests() {
            var context = new EFContext();
            _repository = new UserRepository(context);
            context.PurgeData();
            _userDto = new UserDTO() { Id = 100, Name = "Lucas" };
        }

        [Fact]
        public void Create_UserDTO_valid_input() {
            _repository.CreateAsync(_userDto);
            var result = _repository.ReadAsync(_userDto.Id).Result;
            Assert.Equal(_userDto.Id, result.Id);
            Assert.Equal(_userDto.Name, result.Name);
        }

        [Fact]
        public void Create_UserDTO_invalid_input() {
            Assert.ThrowsAsync<NullReferenceException>(() => _repository.CreateAsync(null));
        }

        [Fact]
        public void Delete_valid_UserDTO() {
            Assert.NotNull(_repository.ReadAsync(5).Result);
            _repository.DeleteAsync(5);
            var result = _repository.ReadAsync(5).Result;
            Assert.Null(result);
        }

        [Fact]
        public void Delete_invalid_UserDTO() {
            Assert.ThrowsAsync<AggregateException>(() => _repository.DeleteAsync(6)); //TODO: Might be bad solution
        }

        [Fact]
        public void Update_valid_UserDTO() {
            var updateDto = new UserDTO() { Id = 5, Name = "newName" };
            var restultUpdate = _repository.UpdateAsync(updateDto).Result;
            var resultRead = _repository.ReadAsync(updateDto.Id).Result;
            Assert.True(restultUpdate);
            Assert.Equal(updateDto.Name, resultRead.Name);

        }

        [Fact]
        public void Update_invalid_UserDTO() {
            Assert.ThrowsAsync<NullReferenceException>(() => _repository.UpdateAsync(null));
        }

        [Fact]
        public void Read_valid_UserDTO() {
            var result =  _repository.ReadAsync(5).Result;
            Assert.Equal(5, result.Id);
            Assert.Equal("Lucas", result.Name);
        }

        [Fact]
        public void Read_invalid_UserDTO() {
            var result = _repository.ReadAsync(int.MaxValue).Result;
            Assert.Null(result);
        }

        [Fact]
        public void Read_multiple_valid_UserDTO() {
            var result = _repository.ReadAsync().Result;
            Assert.Equal(5, result.Count());
        }
    }
}
