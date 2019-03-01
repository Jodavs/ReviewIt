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

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test
{
    public class PhaseLogicTests
    {
        private readonly IPhaseLogic _phaseLogic;
        private readonly Mock<IRepository<PhaseDTO>> _phaseDtoRepository;
        private readonly Mock<IRepository<StudyDTO>> _studyDtoRepository;
        private readonly Mock<IRepository<ReviewTaskDTO>> _taskDtoRepository;
        private readonly Mock<IRepository<TaskDelegationDTO>> _taskDelegationRepository;

        public PhaseLogicTests()
        {
            _phaseDtoRepository = new Mock<IRepository<PhaseDTO>>();
            _studyDtoRepository = new Mock<IRepository<StudyDTO>>();
            _taskDtoRepository = new Mock<IRepository<ReviewTaskDTO>>();
            _taskDelegationRepository = new Mock<IRepository<TaskDelegationDTO>>();

            _phaseLogic = new PhaseLogic(_phaseDtoRepository.Object, _studyDtoRepository.Object, _taskDelegationRepository.Object, _taskDtoRepository.Object);
        }

        [Fact(DisplayName = "Returns all phases for study valid input")]
        public void Gets_all_phases_for_study_valid_input()
        {
            var study = new StudyDTO
            {
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


            var listOfPhases = new List<PhaseDTO>
            {
                new PhaseDTO
                {
                    Id = 0, Purpose = "something", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null, StudyId = study.Id
                },
                new PhaseDTO
                {
                    Id = 1, Purpose = "something1", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null, StudyId = study.Id
                },
                new PhaseDTO
                {
                    Id = 5, Purpose = "something", Participants = null, TaskDelegations = null,
                    DisplayFields = null, InputFields = null, OverlapPercentage = 50,
                    IsAutomatic = true, ConflictManager = null, StudyId = study.Id
                }
            };

            study.Phases = listOfPhases.ToList();

            _phaseDtoRepository.Setup(a => a.ReadAsync()).Returns(Task.FromResult(listOfPhases.AsQueryable()));


            var result = _phaseLogic.GetAllForStudy(study.Id).Result;

            var phase1 = listOfPhases.ElementAt(0);
            var res1 = result.ElementAt(0);

            Assert.Equal(listOfPhases.Count(), result.Count());
            Assert.Equal(phase1.Id, res1.Id);
        }

        [Fact(DisplayName = "Returns all phases for study invalid input")]
        public void Gets_all_phases_for_study_invalid_input()
        {
            var result = _phaseLogic.GetAllForStudy(0).Result;

            Assert.Equal(0, result.Count());
        }

        [Fact(DisplayName = "Get Phase by id valid input")]
        public void Get_Phase_By_Id_Valid_Input()
        {
            var phase = new PhaseDTO
            {
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

            _phaseDtoRepository.Setup(a => a.ReadAsync(It.IsAny<int>())).ReturnsAsync(phase);

            var result = _phaseLogic.GetById(phase.Id).Result;

            Assert.Equal(phase, result);
            Assert.Equal(phase.Purpose, result.Purpose);
        }

        [Fact(DisplayName = "Get phase invalid input")]
        public void Get_Phase_Invalid_Input()
        {
            //_phaseDtoRepository.Setup(a => a.ReadAsync(0)).ReturnsAsync(null);
            var result = _phaseLogic.GetById(0).Result;

            Assert.Equal(null, result);
        }

        [Fact(DisplayName = "Create phase valid input")]
        public void Create_phase_valid_input()
        {
            var phase = new PhaseDTO();

            _phaseDtoRepository.Setup(a => a.CreateAsync(It.IsAny<PhaseDTO>())).ReturnsAsync(phase.Id);

            var results = _phaseLogic.Create(phase).Result;

            Assert.Equal(phase.Id, results);
        }

        [Fact(DisplayName = "Create phase invalid input")]
        public void Create_phase_invalid_input()
        {
            var results = _phaseLogic.Create(null).Result;

            Assert.Equal(0, results);
        }

        [Fact(DisplayName = "Update phase valid input")]
        public void Update_phase_valid_input()
        {
            var phase = new PhaseDTO();

            _phaseDtoRepository.Setup(a => a.UpdateAsync(It.IsAny<PhaseDTO>())).ReturnsAsync(true);

            var results = _phaseLogic.Update(phase).Result;

            Assert.Equal(true, results);
        }

        [Fact(DisplayName = "Update phase invalid input")]
        public void Update_Phase_Invalid_Input()
        {
            var results = _phaseLogic.Update(null).Result;

            Assert.Equal(false, results);
        }
    }
}