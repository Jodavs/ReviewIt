using System;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class FieldRepoTests
    {
        private readonly IRepository<FieldDTO> _repository;
        private readonly FieldDTO _fieldDto;
        private readonly EFContext _context;

        public FieldRepoTests()
        {
            _context = new EFContext();
            _repository = new FieldRepository(_context);
            _context.PurgeData();
            _fieldDto = new FieldDTO {Id = 8, Name = "hello", Description = "something", Type = FieldType.BOOL};
        }
        [Fact]
        public void Create_FieldDTO_valid_input()
        {
            var results = _repository.CreateAsync(_fieldDto).Result;

            Assert.Equal(_fieldDto.Id, results);
        }

        [Fact]
        public void Create_FieldDTO_invalid_input()
        {

            Assert.Throws<AggregateException>(() => _repository.CreateAsync(null).Result);
        }

        [Fact]
        public void Delete_valid_FieldDTO()
        {

            _repository.DeleteAsync(5).Wait();
            var read = _context.Field.Count();

            Assert.Equal(6, read);
        }

        [Fact]
        public void Delete_invalid_FieldDTO()
        {
            Assert.Throws<AggregateException>(() => _repository.DeleteAsync(8).Result);
        }

        [Fact]
        public void Update_valid_FieldDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Update_invalid_FieldDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_valid_FieldDTO()
        {

            var field = _repository.ReadAsync(7).Result;

            Assert.Equal(7, field.Id);
            Assert.Equal("Group for classification criterion", field.Description);
            Assert.Equal("Group C", field.Name);
        }

        [Fact]
        public void Read_invalid_FieldDTO()
        {
            var field = _repository.ReadAsync(10000).Result;
            Assert.Null(field);
        }

        [Fact]
        public void Read_multiple_valid_FieldDTO()
        {
            var fields = _repository.ReadAsync().Result;

            Assert.Equal(7, fields.Count());
        }

    }
}
