using BDSA.ReviewIt.Server.StorageLayer.Conditions;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace ServerDTOs.ServerDTOs {
    public class ExclusionCriterionDTO : IDTO {
        public int Id { get; set; }
        public int StudyId { get; set; }
        public int FieldId { get; set; }
        public ExclusionCondition ExclusionCondition { get; set; }
        public IExclusionCondition<IValue> Condition { get; set; }
        public IValue Operator { get; set; }
    }
}