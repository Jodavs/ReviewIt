using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System.Linq;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;

namespace BDSA.ReviewIt.Server.Logic.StudyManager {
    public class PhaseLogic : IPhaseLogic {
        private readonly IRepository<PhaseDTO> _phaseRepo;
        private readonly IRepository<StudyDTO> _studyRepo;
        private readonly IRepository<TaskDelegationDTO> _taskDelRepo;
        private readonly IRepository<ReviewTaskDTO> _taskRepo;

        public PhaseLogic(IRepository<PhaseDTO> phaseRepo, IRepository<StudyDTO> studyRepo, IRepository<TaskDelegationDTO> taskDelRepo, IRepository<ReviewTaskDTO> taskRepo) {
            _phaseRepo = phaseRepo;
            _studyRepo = studyRepo;
            _taskDelRepo = taskDelRepo;
            _taskRepo = taskRepo;
        }

        public async Task<IEnumerable<PhaseDTO>> GetAllForStudy(int studyId) {
            var phasesForStudy = from phase in await _phaseRepo.ReadAsync()
                                 where phase.StudyId == studyId
                                 select phase;
            return phasesForStudy;
        }

        public async Task<PhaseDTO> GetById(int id) {
            return await _phaseRepo.ReadAsync(id);
        }

        public async Task<int> Create(PhaseDTO phase) {
            return await _phaseRepo.CreateAsync(phase);
        }

        public async Task<bool> Update(PhaseDTO phase) {
            return await _phaseRepo.UpdateAsync(phase);
        }

        public async Task<bool> Delete(int id) {
            return await _phaseRepo.DeleteAsync(id) != 0;
        }

        public async Task<bool> StartPhase(PhaseDTO phase)
        {
            var study = await _studyRepo.ReadAsync(phase.StudyId);
            study.ActivePhase = phase;

            await _studyRepo.UpdateAsync(study);

            var p = study.Users;
            p = p.OrderBy(a => Guid.NewGuid()).ToList();
            var participants = p.ToList();

            study.Publications = study.Publications.OrderBy(a => Guid.NewGuid()).ToList(); // Shuffle list

            /*
             * Gennemgå hver publication
             *  Lav en task delegation for hver publication
             *      Lav antal x tasks (hver til én user)
             *          Få id fra GetNextUser
             **/


            var publications = study.Publications.ToList();

            var numPubs = study.Publications.Count();
            var numUsers = participants.Count();

            var numOverlap = numPubs * 100.0/phase.OverlapPercentage;

            var numMinTasks = numOverlap / numUsers; // Number of minimum tasks for each researcher
            var numExtraTasks = numOverlap % numUsers; // Number of researchers that must get an extra task

            int curUserIdx = 0;

            var taskList = new List<ReviewTaskDTO>();

            foreach (PublicationDTO publication in publications) {
                var delegation = new TaskDelegationDTO {
                    PhaseId = phase.Id,
                    Publication = publication,
                    Tasks = taskList
                };

                await _taskDelRepo.CreateAsync(delegation);

                var numTasks = numMinTasks + (numExtraTasks-- >= 0 ? 1 : 0); // Min tasks and maybe plus an extra
                for (int n = 0; n < numTasks; n++) {
                    var task = new ReviewTaskDTO {
                        TaskDelegationId = delegation.Id,
                        User = participants[curUserIdx].User
                    };

                    await _taskRepo.CreateAsync(task);

                    taskList.Add(task);

                    curUserIdx = (curUserIdx+1) % participants.Count();
                }

                await _taskDelRepo.UpdateAsync(delegation);
                return await Update(phase);
            }

            return false;
        }
    }
}