using System;
using System.Collections.Generic;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Xunit.Sdk;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class AnswerRepoTests {

        //private readonly EFContext _context;
        private readonly IRepository<AnswerDTO> _answerRepo;
        private readonly AnswerDTO _answerDTO;
        private readonly EFContext _context;

        public AnswerRepoTests()
        {
            _context = new EFContext();
            _answerRepo = new AnswerRepository(_context);
            _context.PurgeData();

        }

        [Fact]
        public void Delete_valid_AnswerDTO() {
            _answerRepo.DeleteAsync(1).Wait();
            var read = _context.Answer.Count();

            Assert.Equal(1, read);
        }

        [Fact]
        public void Delete_invalid_AnswerDTO() {
            Assert.Throws<AggregateException>(() => _answerRepo.DeleteAsync(50).Result); ;
        }

        [Fact]
        public void Read_valid_AnswerDTO()
        {
            var answer = _answerRepo.ReadAsync(2).Result;

            Assert.Equal(2, answer.Id);
            Assert.Equal("Indicates whether the the publication is a theoretical text", answer.Field.Description);
        }

        [Fact]
        public void Read_invalid_AnswerDTO() {
            var field = _answerRepo.ReadAsync(10000).Result;
            Assert.Null(field);
        }

        [Fact]
        public void Read_multiple_valid_AnswerDTOs()
        {
            var answers = _answerRepo.ReadAsync().Result;

            Assert.Equal(2, answers.Count());
        }

    }
}