using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class EtablissementDTO
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Challenge>? Challenges { get; set; }
    }
}
