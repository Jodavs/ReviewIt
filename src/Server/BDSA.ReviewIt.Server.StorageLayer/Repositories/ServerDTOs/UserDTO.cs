using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;

namespace ServerDTOs.ServerDTOs {
    public class UserDTO : IDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}