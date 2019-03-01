using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.StudyManager;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using Moq;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.Logic.Tests
{
    public class StudyLogicTests
    {
        private readonly Mock<IRepository<StudyDTO>> _mock;
        private readonly StudyDTO _studyDTO;

        public StudyLogicTests()
        {
            _mock = new Mock<IRepository<StudyDTO>>();
            _studyDTO =  new StudyDTO() { Id = 1 };
        }


        [Fact(DisplayName = "GetAll on empty repo returns a empty queryable object")]
        public async Task GetAll_no_results_return_empty_queryable()
        {
            var results = new List<StudyDTO>().AsQueryable();
            _mock.Setup(m => m.ReadAsync()).ReturnsAsync(results);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.GetAll();
            Assert.Equal(0, response.Count());
        }


        [Fact(DisplayName = "GetAll on non-empty repo returns expected queryable object")]
        public async Task GetAll_multiple_results_return_expected_queryable()
        {
            var results = new List<StudyDTO> {new StudyDTO(), new StudyDTO(), new StudyDTO()}.AsQueryable();
            _mock.Setup(m => m.ReadAsync()).ReturnsAsync(results);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.GetAll();
            Assert.Equal(3, response.Count());
            Assert.Equal(results.First(), response.First());
            Assert.Equal(results.Last(), response.Last());
        }


        [Fact(DisplayName = "GetById on non-empty repo returns expected StudyDTO")]
        public async Task GetById_with_result_return_expected_dto()
        {
            _mock.Setup(m => m.ReadAsync(It.IsAny<int>())).ReturnsAsync(_studyDTO);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.GetById(1);
            Assert.Equal(_studyDTO, response);
        }

        [Fact(DisplayName = "GetById on empty repo returns null")]
        public async Task GetById_no_result_return_null()
        {
            _mock.Setup(m => m.ReadAsync(It.IsAny<int>())).ReturnsAsync(null);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.GetById(1);
            Assert.Equal(null, response);
        }


        [Fact(DisplayName = "Create with correct input returns created item's ID")]
        public async Task Create_correct_input_returns_created_id()
        {
            _mock.Setup(m => m.CreateAsync(It.IsAny<StudyDTO>())).ReturnsAsync(_studyDTO.Id);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.Create(_studyDTO);
            Assert.Equal(_studyDTO.Id, response);
        }

        [Fact(DisplayName = "Create with incorrect as input returns zero")]
        public async Task Create_incorrect_input_returns_0()
        {
            var mock = new Mock<IRepository<StudyDTO>>();
            mock.Setup(m => m.CreateAsync(null)).ReturnsAsync(0);
            var logic = new StudyLogic(mock.Object);

            var response = await logic.Create(null);
            Assert.Equal(0, response);
        }


        [Fact(DisplayName = "Update with correct input returns true")]
        public async Task Update_correct_input_returns_true()
        {
            _mock.Setup(m => m.UpdateAsync(It.IsAny<StudyDTO>())).ReturnsAsync(true);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.Update(_studyDTO, _studyDTO.Id);
            Assert.True(response);
        }


        [Fact(DisplayName = "Update with wrong input returns false")]
        public async Task Update_incorrect_input_returns_false()
        {
            _mock.Setup(m => m.UpdateAsync(It.IsAny<StudyDTO>())).ReturnsAsync(false);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.Update(_studyDTO, _studyDTO.Id);
            Assert.False(response);
        }

        [Fact(DisplayName = "Delete with correct input returns true")]
        public async Task Delete_correct_input_returns_true()
        {
            _mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.Delete(_studyDTO.Id);
            Assert.True(response);
        }

        [Fact(DisplayName = "Delete with wrong input returns false")]
        public async Task Delete_incorrect_input_returns_false()
        {
            _mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(0);
            var logic = new StudyLogic(_mock.Object);

            var response = await logic.Delete(_studyDTO.Id);
            Assert.False(response);
        }
    }
}