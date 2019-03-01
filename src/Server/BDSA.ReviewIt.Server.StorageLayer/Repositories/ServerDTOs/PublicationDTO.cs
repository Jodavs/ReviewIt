using System.Collections.Generic;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class PublicationDTO : IDTO {
        public int Id { get; set; }
        public bool Active { get; set; }
        public ICollection<DataDTO> Data { get; set; }
        public int StudyId { get; set; }
    }
}
