using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.StudyManager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using Server.Controllers;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.API.Tests
{
    public class StudyControllerTests
    {
        private readonly Mock<IStudyLogic> _studyLogicMock;
        private readonly StudyDTO _studyDTO;

        public StudyControllerTests()
        {
            _studyLogicMock = new Mock<IStudyLogic>();
            _studyDTO = new StudyDTO() { Id = 1, Name = "Test", Description = "Test"};
        }

        [Fact(DisplayName = "GetStudyById with correct input returns OK code")]
        public void GetStudyById_correct_input_returns_OK()
        {
            _studyLogicMock.Setup(m => m.GetById(It.IsAny<int>())).ReturnsAsync(_studyDTO);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.GetStudyById(1).Result;

            var jsonResult = Assert.IsType<OkObjectResult>(result);
            var dtoResult = Assert.IsType<StudyDTO>(jsonResult.Value);
            Assert.Equal(_studyDTO, dtoResult);
        }

        [Fact(DisplayName = "GetStudyById with invalid input returns NotFound code")]
        public void GetStudyById_invalid_input_returns_NotFound()
        {
            _studyLogicMock.Setup(m => m.GetById(It.IsAny<int>())).ReturnsAsync(null);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.GetStudyById(1).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Create with model state error returns BadRequest code")]
        public void Create_model_error_returns_bad_request()
        {
            _studyLogicMock.Setup(m => m.Create(It.IsAny<StudyDTO>())).ReturnsAsync(_studyDTO.Id);
            var controller = new StudyController(_studyLogicMock.Object);
            controller.ModelState.AddModelError("", "");

            var result = controller.CreateStudy(_studyDTO).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Create with correct input returns OK code")]
        public void Create_correct_input_returns_OK()
        {
            _studyLogicMock.Setup(m => m.Create(It.IsAny<StudyDTO>())).ReturnsAsync(_studyDTO.Id);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.CreateStudy(_studyDTO).Result;

            var jsonResult = Assert.IsType<OkObjectResult>(result);
            var dtoResult = Assert.IsType<int>(jsonResult.Value);
            Assert.Equal(_studyDTO.Id, dtoResult);
        }

        [Fact(DisplayName = "Update with correct input returns NoContent code")]
        public void Update_correct_input_returns_NoContent()
        {
            _studyLogicMock.Setup(m => m.Update(It.IsAny<StudyDTO>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.UpdateStudy(_studyDTO, _studyDTO.Id).Result;
            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "Update with invalid input returns NotFound code")]
        public void Update_invalid_input_returns_not_found()
        {
            _studyLogicMock.Setup(m => m.Update(It.IsAny<StudyDTO>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.UpdateStudy(_studyDTO, _studyDTO.Id).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Update with model state error returns BadRequest code")]
        public void Update_model_error_returns_bad_request()
        {
            _studyLogicMock.Setup(m => m.Update(It.IsAny<StudyDTO>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new StudyController(_studyLogicMock.Object);
            controller.ModelState.AddModelError("", "");
            var result = controller.UpdateStudy(_studyDTO, _studyDTO.Id).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Delete with correct input returns NoContent code")]
        public void Delete_correct_input_returns_NoContent()
        {
            _studyLogicMock.Setup(m => m.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.DeleteStudy(_studyDTO.Id).Result;
            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "Delete with invalid input returns NotFound code")]
        public void Delete_invalid_input_returns_not_found()
        {
            _studyLogicMock.Setup(m => m.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.DeleteStudy(_studyDTO.Id).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "GetAllStudies with results returns OK code")]
        public void GetAllStudies_with_result_returns_OK()
        {
            var list = new List<StudyDTO>() {_studyDTO};
            _studyLogicMock.Setup(m => m.GetAll()).ReturnsAsync(list);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.GetAllStudies().Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "GetAllStudies with no results returns NotFound code")]
        public void GetAllStudies_no_result_returns_NotFound()
        {
            _studyLogicMock.Setup(m => m.GetAll()).ReturnsAsync(null);
            var controller = new StudyController(_studyLogicMock.Object);
            var result = controller.GetAllStudies().Result;
            Assert.IsType<NotFoundResult>(result);
        }
    }
}