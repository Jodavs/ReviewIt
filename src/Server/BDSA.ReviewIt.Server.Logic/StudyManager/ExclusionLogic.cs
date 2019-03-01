using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public class ExclusionLogic : IExclusionLogic
    {
        private readonly PublicationRepository _publicationRepository;
        private readonly ExclusionCriterionRepository _exclusionCriterionRepository;
        private readonly StudyRepository _studyRepository;

        public ExclusionLogic(PublicationRepository publicationRepository,
            ExclusionCriterionRepository exclusionCriterionRepository, StudyRepository studyRepository)
        {
            _publicationRepository = publicationRepository;
            _exclusionCriterionRepository = exclusionCriterionRepository;
            _studyRepository = studyRepository;
        }
        public async Task<IEnumerable<ExclusionCriterionDTO>> GetAllForStudy(int studyId)
        {
            return (await _studyRepository.ReadAsync(studyId)).ExclusionCriteria;
        }

        public async Task<ExclusionCriterionDTO> GetById(int id)
        {
            return await _exclusionCriterionRepository.ReadAsync(id);
        }

        public async Task<int> Create(ExclusionCriterionDTO exclusion)
        {
            int res = await _exclusionCriterionRepository.CreateAsync(exclusion);
            ApplyExclusionCriterion(exclusion);
            return res;
        }

        public async Task<bool> Update(ExclusionCriterionDTO exclusion)
        {
            bool res = await _exclusionCriterionRepository.UpdateAsync(exclusion);
            ApplyExclusionCriterion(exclusion);
            return res;
        }

        public async Task<bool> Delete(int id)
        {
            // Get the exclusionCriterion
            var exclusion = await _exclusionCriterionRepository.ReadAsync(id);

            // Delete it
            int res = await _exclusionCriterionRepository.DeleteAsync(id);

            // Loop through each exclusionCriterion in the study and apply them anew to the publications
            var study = await _studyRepository.ReadAsync(exclusion.StudyId);
            foreach (var e in study.ExclusionCriteria)
            {
                ApplyExclusionCriterion(e);
            }

            return res != 0;
        }

        private async void ApplyExclusionCriterion(ExclusionCriterionDTO exclusion)
        {
            var studyPublications = await GetPublicationsForStudy(exclusion.StudyId);

            await studyPublications.ForEachAsync(p =>
            {
                var dataField = p.Data.First(d => d.FieldId == exclusion.FieldId);
                // Check if publication should be excluded or included (change to false if apply returns false)
                p.Active = p.Active && exclusion.Condition.Apply(dataField.Value);
            });
        }

        private async Task<IQueryable<PublicationDTO>> GetPublicationsForStudy(int studyId)
        {
            var publications = await _publicationRepository.ReadAsync();
            return publications.Where(p => p.StudyId == studyId);
        }
    }
}