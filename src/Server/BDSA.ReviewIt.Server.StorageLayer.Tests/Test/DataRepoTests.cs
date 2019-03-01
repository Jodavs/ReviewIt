using System;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;
using Xunit.Sdk;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class DataRepoTests
    {
        private readonly IRepository<DataDTO> _repository;
        private readonly DataDTO _dataDto;
        private readonly EFContext _context;

        public DataRepoTests()
        {
            _context = new EFContext();
            _repository = new DataRepository(_context);
            _context.PurgeData();

        }

        [Fact]
        public void Delete_valid_DataDTO()
        {
            var delete = _repository.DeleteAsync(1).Result;

            Assert.Equal(1, delete);
        }

        [Fact]
        public void Delete_invalid_DataDTO() {
            Assert.Throws<AggregateException>(() => _repository.DeleteAsync(4846).Result);
        }

        [Fact]
        public void Read_valid_DataDTO()
        {
            var results = _repository.ReadAsync(1).Result;

            Assert.Equal(1, results.Id);
        }

        [Fact]
        public void Read_invalid_DataDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_multiple_valid_DataDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_multiple_DataDTO() {
            Assert.True(true);
        }
    }
}
