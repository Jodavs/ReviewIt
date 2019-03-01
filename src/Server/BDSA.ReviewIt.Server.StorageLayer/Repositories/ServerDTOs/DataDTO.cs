using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using ServerDTOs.ServerDTOs;

namespace BDSA.ReviewIt.Server.StorageLayer.ServerDTOs {
    public class DataDTO : IDTO {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int FieldId { get; set; }
        public IValue Value { get; set; }
    }
}
