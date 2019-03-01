using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.Utilities;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class ExclusionCriterionRepoTests {
        private IRepository<ExclusionCriterionDTO> _repo;
        public ExclusionCriterionRepoTests() {
            var efContext = new EFContext();
            _repo = new ExclusionCriterionRepository(efContext);
            efContext.PurgeData();
        }
        // Create Exclusion
        private static ExclusionCriterion exclusionEntity = new ExclusionCriterion {
            FieldId = 2,
            StudyId = 1,
            Condition = ExclusionCondition.BOOL,
            Operator = "test"
        };

        private static ExclusionCriterionDTO exclusionDTO = exclusionEntity.ConvertToDTO();

        [Fact]
        public void Create_ExclusionCriterionDTO_valid_input() {
            // Act
            var created = _repo.CreateAsync(exclusionDTO).Result;

            // Assert
            Assert.True(created > 0);

        }

        [Fact]
        public void Create_ExclusionCriterionDTO_invalid_input() {
            var createdWithNull = _repo.CreateAsync(null).Result;

            Assert.Null(createdWithNull);
        }

        [Fact]
        public void Delete_valid_ExclusionCriterionDTO() {
            var deleted = _repo.DeleteAsync(1).Result;
            Assert.True(deleted > 0);
        }

        [Fact]
        public void Delete_invalid_ExclusionCriterionDTO() {
            var deleted = _repo.DeleteAsync(700).Result;
            Assert.True(deleted == 0);
        }

        [Fact]
        public void Update_valid_ExclusionCriterionDTO() {
            _repo.CreateAsync(exclusionDTO);
            var updated = _repo.UpdateAsync(exclusionDTO).Result;
            Assert.True(updated);
        }

        [Fact]
        public void Update_invalid_ExclusionCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_valid_ExclusionCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_invalid_ExclusionCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_multiple_valid_ExclusionCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_multiple_ExclusionCriterionDTO() {
            Assert.True(true);
        }
    }
}
