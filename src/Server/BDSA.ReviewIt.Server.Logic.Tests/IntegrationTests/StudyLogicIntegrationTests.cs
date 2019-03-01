using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.StudyManager;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using Moq;
using ServerDTOs.ServerDTOs;
using Xunit;
using RepositoryLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer;

namespace BDSA.ReviewIt.Server.Logic.Tests {
    public class StudyLogicIntegrationTests {
        private readonly IRepository<StudyDTO> _repo;
        private readonly StudyDTO _studyDTO;

        public StudyLogicIntegrationTests() {
            var efContext = new EFContext();
            _repo = new StudyRepository(efContext);
            _studyDTO = new StudyDTO() { Id = 1 };
            efContext.PurgeData();
        }


        [Fact(DisplayName = "GetAll on empty repo returns a empty queryable object")]
        public async Task GetAll_no_results_return_empty_queryable() {
            var results = new List<StudyDTO>().AsQueryable();
            var logic = new StudyLogic(_repo);

            var response = await logic.GetAll();
            Assert.Equal(0, response.Count());
        }


        [Fact(DisplayName = "GetAll on non-empty repo returns expected queryable object")]
        public async Task GetAll_multiple_results_return_expected_queryable_Integration() {
            var results = new List<StudyDTO> { new StudyDTO(), new StudyDTO(), new StudyDTO() }.AsQueryable();
            var logic = new StudyLogic(_repo);

            var response = await logic.GetAll();
            Assert.Equal(3, response.Count());
            Assert.Equal(results.First(), response.First());
            Assert.Equal(results.Last(), response.Last());
        }


        [Fact(DisplayName = "GetById on non-empty repo returns expected StudyDTO")]
        public async Task GetById_with_result_return_expected_dto_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.GetById(1);
            Assert.Equal(_studyDTO, response);
        }

        [Fact(DisplayName = "GetById on empty repo returns null")]
        public async Task GetById_no_result_return_null_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.GetById(1);
            Assert.Equal(null, response);
        }


        [Fact(DisplayName = "Create with correct input returns created item's ID")]
        public async Task Create_correct_input_returns_created_id_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Create(_studyDTO);
            Assert.Equal(_studyDTO.Id, response);
        }

        [Fact(DisplayName = "Create with incorrect as input returns zero")]
        public async Task Create_incorrect_input_returns_0_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Create(null);
            Assert.Equal(0, response);
        }


        [Fact(DisplayName = "Update with correct input returns true")]
        public async Task Update_correct_input_returns_true_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Update(_studyDTO, _studyDTO.Id);
            Assert.True(response);
        }


        [Fact(DisplayName = "Update with wrong input returns false")]
        public async Task Update_incorrect_input_returns_false_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Update(_studyDTO, _studyDTO.Id);
            Assert.False(response);
        }

        [Fact(DisplayName = "Delete with correct input returns true")]
        public async Task Delete_correct_input_returns_true_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Delete(_studyDTO.Id);
            Assert.True(response);
        }

        [Fact(DisplayName = "Delete with wrong input returns false")]
        public async Task Delete_incorrect_input_returns_false_Integration() {
            var logic = new StudyLogic(_repo);

            var response = await logic.Delete(_studyDTO.Id);
            Assert.False(response);
        }
    }
}