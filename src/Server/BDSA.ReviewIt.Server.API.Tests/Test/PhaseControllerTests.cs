using BDSA.ReviewIt.Server.Logic.StudyManager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.API.Tests.Test
{
    public class PhaseControllerTests
    {
        private readonly Mock<IPhaseLogic> _phaseLogic;
        private readonly PhaseController _phaseController;

        public PhaseControllerTests()
        {
            _phaseLogic = new Mock<IPhaseLogic>();
            _phaseController = new PhaseController(_phaseLogic.Object);
        }

        [Fact(DisplayName = "Get phase by id valid input")]
        public void Get_phase_by_id_valid_input()
        {
            var phase = new PhaseDTO {Id = 1};

            _phaseLogic.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(phase);

            var results = _phaseController.GetPhaseById(phase.Id).Result;

            var jResults = Assert.IsType<OkObjectResult>(results);
            var aResults = Assert.IsType<PhaseDTO>(jResults.Value);

            Assert.Equal(phase, aResults);
        }

        [Fact(DisplayName = "Get phase by id invalid input")]
        public void Get_phase_by_id_invalid_input()
        {
            _phaseLogic.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(null);

            var result = _phaseController.GetPhaseById(0).Result;

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Create phase invalid input")]
        public void Create_phase_invalid_input()
        {
            var phase = new PhaseDTO();
            _phaseController.ModelState.AddModelError("","");

            var results = _phaseController.CreatePhase(phase).Result;

            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact(DisplayName = "Create phase valid input")]
        public void Create_phase_valid_input()
        {
            var phase = new PhaseDTO();

            _phaseLogic.Setup(a => a.Create(phase)).ReturnsAsync(2);

            var results = _phaseController.CreatePhase(phase).Result;
            //var created = results.Value as PhaseDTO;

            _phaseLogic.Verify(r => r.Create(phase), Times.Once);

            Assert.IsAssignableFrom<OkResult>(results);
            //Assert.Equal(10, created.Id);
        }

        [Fact(DisplayName = "Update phase valid input")]
        public void Update_phase_valid_input()
        {
            var phase = new PhaseDTO {Id = 5};

            _phaseLogic.Setup(a => a.Update(phase)).ReturnsAsync(true);

            var results = _phaseController.UpdatePhase(phase).Result;

            Assert.IsType<NoContentResult>(results);
        }

        [Fact(DisplayName = "Update phase invalid input")]
        public void Update_phase_invalid_input()
        {
            var phase = new PhaseDTO {Id = 2};

            var results = _phaseController.UpdatePhase(phase).Result;

            Assert.IsType<NotFoundResult>(results);
        }

        [Fact(DisplayName = "Delete phase valid id")]
        public void Delete_phase_valid_id()
        {
            _phaseLogic.Setup(a => a.Delete(10)).ReturnsAsync(true);

            var results = _phaseController.DeletePhaseById(10).Result;

            Assert.IsType<NoContentResult>(results);
        }

        [Fact(DisplayName = "Delete phase invalid id")]
        public void Delete_phase_invalid_id()
        {
            var results = _phaseController.DeletePhaseById(10).Result;

            Assert.IsType<NotFoundResult>(results);
        }

        [Fact(DisplayName = "Start phase valid input")]
        public void Start_phase_valid_input()
        {
            var phase = new PhaseDTO {Id = 10};

            _phaseLogic.Setup(a => a.StartPhase(phase)).ReturnsAsync(true);

            var results = _phaseController.StartPhase(phase.Id).Result;

            Assert.IsType<NoContentResult>(results);
        }

        [Fact(DisplayName = "Start phase invalid input")]
        public void Start_phase_invalid_input()
        {
            var phase = new PhaseDTO {Id = 10};

            var results = _phaseController.StartPhase(phase.Id).Result;

            Assert.IsType<NotFoundResult>(results);
        }
    }
}