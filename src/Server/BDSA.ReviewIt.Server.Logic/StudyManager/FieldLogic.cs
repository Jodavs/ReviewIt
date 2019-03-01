using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using ServerDTOs.ServerDTOs;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace BDSA.ReviewIt.Server.Logic.StudyManager
{
    public class FieldLogic : IFieldLogic
    {
        private readonly IRepository<FieldDTO> _fieldRepo;
        private readonly IRepository<StudyDTO> _studyRepo;

        public FieldLogic(IRepository<FieldDTO> fieldRepo, IRepository<StudyDTO> studyRepo, IRepository<PhaseDTO> phaseRepo)
        {
            _fieldRepo = fieldRepo;
            _studyRepo = studyRepo;
        }

        public async Task<IEnumerable<FieldDTO>> GetAllForStudy(int studyId)
        {
            var getstudy = await _studyRepo.ReadAsync(studyId);

            var fieldList = getstudy.Phases.Select(d => d.DisplayFields).SelectMany(i => i);

            return fieldList;

        }

        public async Task<FieldDTO> GetById(int id)
        {
            var get = await _fieldRepo.ReadAsync(id);

            return get;
        }

        public async Task<int> Create(FieldDTO field)
        {
            var create = await _fieldRepo.CreateAsync(field);

            return create;
        }

        public async Task<bool> Update(FieldDTO field, int id)
        {
            var  update = await _fieldRepo.UpdateAsync(field);

            return update;
        }

        public async Task<bool> Delete(int id)
        {
            int delete = await _fieldRepo.DeleteAsync(id);

            return delete != 0;
        }
    }
}