using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.TaskManager
{
    public interface ITaskLogic
    {
        Task<bool> SubmitTask(ReviewTaskDTO task);
        
        Task<IEnumerable<ReviewTaskDTO>> GetUserTasks(int studyId, int userId);
        Task<IEnumerable<ReviewTaskDTO>> GetUserConflicts(int studyId, int phaseId, int userId);
        Task<IEnumerable<ReviewTaskDTO>> GetallTasks();
        Task<IEnumerable<ReviewTaskDTO>> GetTaskById(int id);
    }
}
