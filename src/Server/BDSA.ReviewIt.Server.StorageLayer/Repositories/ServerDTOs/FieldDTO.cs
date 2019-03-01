using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class FieldDTO : IDTO {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FieldType Type { get; set; }
    }
}