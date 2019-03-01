﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.TaskManager;
using Microsoft.AspNetCore.Mvc;
using ServerDTOs.ServerDTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskLogic _taskLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskLogic"></param>
        public TaskController(ITaskLogic taskLogic)
        {
            _taskLogic = taskLogic;
        }

        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns> IActionResult </returns>
        [HttpPost("/tasks")]
        public async Task<IActionResult> SubmitTask([FromBody]ReviewTaskDTO taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskLogic.SubmitTask(taskDto);

            if (!result)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);

        }

        /// <summary>
        /// Get all tasks from study id
        /// </summary>
        /// <param name="studyId"></param>
        /// <returns> IActionResult </returns>
        [HttpGet("study/{studyId}")]
        public async Task<IActionResult> GetAllTasks(int studyId)
        {
            var allTasks = await _taskLogic.GetallTasks();

            if (allTasks == null)
            {
                return NotFound();
            }
            return Ok(allTasks);
        }

        /// <summary>
        /// Get study user tasks
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="userId"></param>
        /// <returns> IActionResult </returns>
        [HttpGet("user/{userId}/study/{studyId}")]
        public async Task<IActionResult> GetStudyUserTasks(int studyId, int userId)
        {
            var getAllUserTasks = await _taskLogic.GetUserTasks(studyId, userId);

            if (getAllUserTasks == null)
            {
                return NotFound();
            }

            return Ok(getAllUserTasks);
        }



        /// <summary>
        /// Get task from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IActionResult </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var getTaskById = await _taskLogic.GetTaskById(id);

            if (getTaskById == null)
            {
                return NotFound();
            }

            return Ok(getTaskById);
        }

    }
}


