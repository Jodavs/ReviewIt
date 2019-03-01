using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.TaskManager;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Server.Controllers;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.API.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskLogic> _logicMock;
        private readonly TaskController _taskController;

        public TaskControllerTests()
        {
            _logicMock = new Mock<ITaskLogic>();
            _taskController = new TaskController(_logicMock.Object);
        }

        [Fact(DisplayName = "SubmitTask (succeed)")]
        public void Submit_task_returns_Ok_when_submission_succeeds()
        {
            // Arrange
            _logicMock.Setup(m => m.SubmitTask(It.IsAny<ReviewTaskDTO>())).Returns(Task.FromResult(true));

            // Act
            var result = _taskController.SubmitTask(new ReviewTaskDTO()).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Submit_task_returns_Bad_Request_when_submission_fails()
        {
            // Set up mock
            _logicMock.Setup(m => m.SubmitTask(It.IsAny<ReviewTaskDTO>())).Returns(Task.FromResult(false));

            // Test
            var result = _taskController.SubmitTask(new ReviewTaskDTO()).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAllTasks_returns_Ok_when_given_valid_studyId()
        {
            // Arrange
            _logicMock.Setup(m => m.GetallTasks()).Returns(
                Task.FromResult(new List<ReviewTaskDTO>() as IEnumerable<ReviewTaskDTO>));

            // Act
            var result = _taskController.GetAllTasks(0).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllTasks_returns_Not_Found_when_given_invalid_studyId()
        {
            // Arrange
            _logicMock.Setup(m => m.GetallTasks()).Returns(
                Task.FromResult<IEnumerable<ReviewTaskDTO>>(null));

            // Act
            var result = _taskController.GetAllTasks(0).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTaskById_returns_Ok_when_given_valid_taskId()
        {
            // Arrange
            _logicMock.Setup(m => m.GetTaskById(It.IsAny<int>())).Returns(
                Task.FromResult(new List<ReviewTaskDTO>() as IEnumerable<ReviewTaskDTO>));

            // Act
            var result = _taskController.GetTaskById(0).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetTaskById_returns_Not_Found_when_given_invalid_taskId()
        {
            // Arrange
            _logicMock.Setup(m => m.GetTaskById(It.IsAny<int>())).Returns(
                Task.FromResult<IEnumerable<ReviewTaskDTO>>(null));

            // Act
            var result = _taskController.GetTaskById(0).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetStudyUserTasks_returns_Ok_when_given_valid_input()
        {
            // Arrange
            _logicMock.Setup(m => m.GetUserTasks(It.IsAny<int>(), It.IsAny<int>())).Returns(
                Task.FromResult(new List<ReviewTaskDTO>() as IEnumerable<ReviewTaskDTO>));

            // Act
            var result = _taskController.GetStudyUserTasks(0, 0).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetStudyUserTasks_returns_Not_Found_when_given_valid_input()
        {
            // Arrange
            _logicMock.Setup(m => m.GetUserTasks(It.IsAny<int>(), It.IsAny<int>())).Returns(
                Task.FromResult<IEnumerable<ReviewTaskDTO>>(null));

            // Act
            var result = _taskController.GetStudyUserTasks(0, 0).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
