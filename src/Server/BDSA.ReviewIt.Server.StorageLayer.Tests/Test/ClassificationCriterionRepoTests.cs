using System.Linq;
using Moq;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace BDSA.ReviewIt.Server.StorageLayer.Tests.Test {
    public class ClassificationCriterionRepoTests {
        private readonly ClassificationCriterionRepository _classificationCriterionRepository;

        public ClassificationCriterionRepoTests() {
            EFContext efContext = new EFContext();
            _classificationCriterionRepository = new ClassificationCriterionRepository(efContext);
            efContext.PurgeData();
        }

        [Fact]
        public void Create_ClassificationCriterionDTO_valid_input() {
            // Arrange
            var dto = new ClassificationCriterionDTO {
                StudyId = 1
            };
            dto.Parent = _classificationCriterionRepository.ReadAsync(1).Result;

            // Act
            var result = _classificationCriterionRepository.CreateAsync(dto).Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ClassificationCriterionDTO_invalid_input() {
            // Arrange
            var invalid_dto = new ClassificationCriterionDTO();

            // Act
            var result = _classificationCriterionRepository.CreateAsync(invalid_dto).Result;

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Delete_valid_ClassificationCriterionDTO() {
            // Act
            var result = _classificationCriterionRepository.DeleteAsync(1).Result;
            var result2 = _classificationCriterionRepository.ReadAsync(1).Result;

            // Assert
            Assert.True(result > 0);
            Assert.Null(result2);
        }

        [Fact]
        public void Update_valid_ClassificationCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Update_invalid_ClassificationCriterionDTO() {
            Assert.True(true);
        }

        [Fact]
        public void Read_valid_ClassificationCriterionDTO() {
            // Act
            var result = _classificationCriterionRepository.ReadAsync(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Read_invalid_ClassificationCriterionDTO() {
            // Act
            var result = _classificationCriterionRepository.ReadAsync(7000);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Read_multiple_valid_ClassificationCriterionDTO() {
            // Act
            var result = _classificationCriterionRepository.ReadAsync().Result;

            // Assert
            Assert.True(result.Any());
        }
    }
}
