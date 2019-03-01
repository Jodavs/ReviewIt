using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.StudyManager;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using ServerDTOs.ServerDTOs;
using Xunit;
using RepositoryLayer.Repositories;
using System.Collections.ObjectModel;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class PhaseLogicIntegrationTests {
        private readonly IPhaseLogic _phaseLogic;
        private readonly IRepository<PhaseDTO> _phaseDtoRepository;
        private readonly IRepository<StudyDTO> _studyDtoRepository;
        private readonly IRepository<ReviewTaskDTO> _taskDtoRepository;
        private readonly IRepository<TaskDelegationDTO> _taskDelegationRepository;

        public PhaseLogicIntegrationTests() {
            var efContext = new EFContext();
            _phaseDtoRepository = new PhaseRepository(efContext);
            _studyDtoRepository = new StudyRepository(efContext);
            _taskDtoRepository = new TaskRepository(efContext);
            _taskDelegationRepository = new TaskDelegationRepository(efContext);

            _phaseLogic = new PhaseLogic(_phaseDtoRepository, _studyDtoRepository, _taskDelegationRepository, _taskDtoRepository);
            efContext.PurgeData();
        }

        [Fact(DisplayName = "Returns all phases for study valid input")]
        public async void Gets_all_phases_for_study_valid_input_integration() {
            var study = new StudyDTO {
                Id = 0,
                Name = "test",
                Description = "for testing",
                ActivePhase = null,
                ExclusionCriteria = null,
                ClassificationCriteria = null,
                Users = null,
                Publications = null,
                Phases = null
            };


            var listOfPhases = new Collection<PhaseDTO>
            {
                new PhaseDTO
                {
                    Id = 0, Purpose = "something", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null
                },
                new PhaseDTO
                {
                    Id = 1, Purpose = "something1", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null
                },
                new PhaseDTO
                {
                    Id = 5, Purpose = "something", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null
                }
            };

            study.Phases = listOfPhases;

            var result = await _phaseLogic.GetAllForStudy(study.Id);

            var phase1 = listOfPhases.ElementAt(0);
            var res1 = result.ElementAt(0);

            Assert.Equal(listOfPhases.Count(), result.Count());
            Assert.Equal(phase1.Id, res1.Id);
        }

        [Fact(DisplayName = "Returns all phases for study invalid input")]
        public async void Gets_all_phases_for_study_invalid_input_Integration() {
            var result = await _phaseLogic.GetAllForStudy(0);

            Assert.Equal(0, result.Count());
        }

        [Fact(DisplayName = "Get Phase by id valid input")]
        public async void Get_Phase_By_Id_Valid_Input_Integration() {
            var phase = new PhaseDTO {
                Id = 0,
                Purpose = "something",
                DisplayFields = null,
                Participants = null,
                InputFields = null,
                TaskDelegations = null,
                IsAutomatic = false,
                ConflictManager = null,
                OverlapPercentage = 10
            };

            var result = await _phaseLogic.GetById(phase.Id);

            Assert.Equal(phase, result);
            Assert.Equal(phase.Purpose, result.Purpose);
        }

        [Fact(DisplayName = "Get phase invalid input")]
        public async void Get_Phase_Invalid_Input_Integration() {
            //_phaseDtoRepository.Setup(a => a.ReadAsync(0)).ReturnsAsync(null);
            var result = await _phaseLogic.GetById(0);

            Assert.Equal(null, result);
        }

        [Fact(DisplayName = "Create phase valid input")]
        public async void Create_phase_valid_input_Integration() {
            var phase = new PhaseDTO();

            var results = await _phaseLogic.Create(phase);

            Assert.Equal(phase.Id, results);
        }

        [Fact(DisplayName = "Create phase invalid input")]
        public async void Create_phase_invalid_input_Integration() {
            var results = await _phaseLogic.Create(null);

            Assert.Equal(0, results);
        }

        [Fact(DisplayName = "Update phase valid input")]
        public async void Update_phase_valid_input_Integration() {
            var phase = new PhaseDTO();

            var results = await _phaseLogic.Update(phase);

            Assert.Equal(true, results);
        }

        [Fact(DisplayName = "Update phase invalid input")]
        public async void Update_Phase_Invalid_Input_Integration() {
            var results = await _phaseLogic.Update(null);

            Assert.Equal(false, results);
        }
    }
}