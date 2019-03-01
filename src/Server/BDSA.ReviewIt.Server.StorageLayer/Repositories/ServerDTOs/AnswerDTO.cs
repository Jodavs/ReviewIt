using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Xunit.Sdk;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using System.Collections.Generic;

namespace ServerDTOs.ServerDTOs {
    public class AnswerDTO : IDTO {
        public int Id { get; set; }
        public ReviewTaskDTO ReviewTask { get; set; }
        public FieldDTO Field { get; set; }
        public IValue Value { get; set; }
    }
}