using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerDTOs.ServerDTOs;
using RepositoryLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.Logic.TaskManager {
    public class TaskLogic : ITaskLogic {
        private IRepository<ReviewTaskDTO> _taskRepo;
        private IRepository<AnswerDTO> _answerRepo;
        private IRepository<UserDTO> _userRepo;
        private IRepository<StudyDTO> _studyRepo;
        private IRepository<TaskDelegationDTO> _taskDelegationRepository;
        private IRepository<PhaseDTO> _phaseRepository;
        private IRepository<FieldDTO> _fieldRepository;
        public TaskLogic(IRepository<ReviewTaskDTO> taskRepo, IRepository<FieldDTO> fieldRepository, IRepository<AnswerDTO> answerRepo, IRepository<UserDTO> userRepo, IRepository<StudyDTO> studyRepo, IRepository<TaskDelegationDTO> taskDelegationRepository, IRepository<PhaseDTO> phaseRepository) {
            _taskRepo = taskRepo;
            _answerRepo = answerRepo;
            _fieldRepository = fieldRepository;
            _userRepo = userRepo;
            _studyRepo = studyRepo;
            _taskDelegationRepository = taskDelegationRepository;
            _phaseRepository = phaseRepository;
        }

        /// <summary>
        /// Submits the given task. The task has to be created in the database beforehand.
        /// </summary>
        /// <param name="task">The task to be submitted</param>
        /// <returns>True if successful, false if the task could not be submitted</returns>
        public async Task<bool> SubmitTask(ReviewTaskDTO task) {
            // Make a list of all field ids in the answers of the task
            List<int> taskListId = new List<int>();
            foreach (AnswerDTO a in task.Answers) {
                taskListId.Add(a.Field.Id);
            }

            ReviewTaskDTO readTask;
            if ((readTask = await _taskRepo.ReadAsync(task.Id)) != null)
            {
                var readTaskDelegation = await _taskDelegationRepository.ReadAsync(readTask.TaskDelegationId);
                var readPhase = await _phaseRepository.ReadAsync(readTaskDelegation.PhaseId);

                var taskDelegation = await _taskDelegationRepository.ReadAsync(task.TaskDelegationId);
                var phase = await _phaseRepository.ReadAsync(taskDelegation.PhaseId);

                // Check if the phases match
                if (readPhase.Id != phase.Id)
                    return false;

                // Check if all the fields in the phase match the fields in the answers
                var fieldListId = from s in phase.InputFields
                                  where taskListId.Contains(s.Id)
                                  select s.Id;

                // Check if the answers and the phase have the same amount of fields
                if (fieldListId.Count() != taskListId.Count())
                    return false;

                // Use the above fieldList to get all tasks that match the 
                if (fieldListId == null)
                    return false;
                List<AnswerDTO> answerList = new List<AnswerDTO>();
                foreach (AnswerDTO a in task.Answers) {
                    if (fieldListId.Contains(a.Field.Id)) {
                        answerList.Add(a);
                    }
                }
                // Create all the answers
                if (answerList[0] == null)
                    return false;
                foreach (AnswerDTO a in answerList) {
                    await _answerRepo.CreateAsync(a);
                }

                // Submit the task
                task.IsSubmitted = true;
                // Update the task
                if (!await _taskRepo.UpdateAsync(task)) {
                    return false; 
                }

                var taskList = taskDelegation.Tasks.ToList();
                int j = 0;
                // Check if all tasks have been submitted
                for (int i = 0; i < taskList.Count(); i++) {
                    if (taskList[i].IsSubmitted)
                        j++;
                }

                // Check if all tasks have been submitted
                if (j == taskList.Count()) {
                    CommitTaskDelegation(taskDelegation);
                    var exclusions = (await _studyRepo.ReadAsync(phase.StudyId)).ExclusionCriteria;
                    foreach (var exclusionCriterionDto in exclusions) {
                        var publication = taskDelegation.Publication;

                        var dataField = publication.Data.Single(d =>
                        {
                            var f = _fieldRepository.ReadAsync(d.FieldId).Result;
                            return f.Id == exclusionCriterionDto.FieldId;
                        });
                        publication.Active = publication.Active && exclusionCriterionDto.Condition.Apply(dataField.Value);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Given a Study and a User, fetches all the users tasks (no matter the state) from the studys current active phase
        /// </summary>
        /// <param name="studyId">The id of the study</param>
        /// <param name="userId">The id of the user</param>
        /// <returns>All the users tasks from the studys active phase</returns>
        public async Task<IEnumerable<ReviewTaskDTO>> GetUserTasks(int studyId, int userId) {
            // Get all the tasks
            var study = await _studyRepo.ReadAsync(studyId);

            return study.ActivePhase.TaskDelegations
                .Select(t => t.Tasks)
                .SelectMany(t => t)
                .Where(t => t.User.Id == userId);
        }

        public async Task<IEnumerable<ReviewTaskDTO>> GetUserConflicts(int studyId, int phaseId, int userId) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewTaskDTO>> GetallTasks() {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewTaskDTO>> GetTaskById(int id) {
            throw new NotImplementedException();
        }

        private void CommitTaskDelegation(TaskDelegationDTO taskDelegation) {
            var answers = taskDelegation.Tasks.Select(t => t.Answers).SelectMany(t => t);

            var answerDict = CreateTaskDictionary(answers);

            var publication = taskDelegation.Publication;

            foreach (var fieldAnswerList in answerDict) {
                var fieldId = fieldAnswerList.Key;

                // TODO : Write framework for conflict handling

                var valueDict = CreateValueDict(fieldAnswerList.Value);

                // Find the value that was submitted the maximum number of times
                var mostPopular = valueDict.Aggregate((max, cur) => cur.Value > max.Value ? cur : max);

                publication.Data.Single(d =>
                {
                    var f = _fieldRepository.ReadAsync(d.FieldId).Result;
                    return f.Id == fieldId;
                }).Value = mostPopular.Key;
            }
        }

        private Dictionary<IValue, int> CreateValueDict(IEnumerable<AnswerDTO> answers) {
            var dict = new Dictionary<IValue, int>();
            foreach (var answerDto in answers) {
                var val = answerDto.Value;
                if (!dict.ContainsKey(val))
                    dict.Add(val, 1);
                else
                    dict[val] += 1;
            }
            return dict;
        }

        private Dictionary<int, List<AnswerDTO>> CreateTaskDictionary(IEnumerable<AnswerDTO> answers) {
            var dict = new Dictionary<int, List<AnswerDTO>>();
            foreach (var answerDto in answers) {
                var fid = answerDto.Field.Id;
                // Check if the dictionary already has an entry for the fieldId associated with the answer
                if (!dict.ContainsKey(fid))
                    dict.Add(fid, new List<AnswerDTO>());

                // Add the answer to the dictionary
                dict[fid].Add(answerDto);
            }
            return dict;
        }
    }
}