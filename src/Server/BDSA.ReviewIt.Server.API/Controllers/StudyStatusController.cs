﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.StudyManager;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class StudyStatusController : Controller
    {
        private readonly IStudyLogic _studyLogic;

        public StudyStatusController(IStudyLogic studyLogic)
        {
            _studyLogic = studyLogic;
        }
        /// <summary>
        /// Get status of a study from study id
        /// </summary>
        /// <param name="studyId"></param>
        /// <returns> IActionResult </returns>
        [HttpGet("api/study/{studyId}/status")]
        public async Task<IActionResult> GetStudyStatus(int studyId)
        {
            var get = await _studyLogic.GetStudyStatus(studyId);

            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

    }
}
