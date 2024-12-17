using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class ImageEnonceDTO
    {
        public uint Id { get; set; }
        public uint ChallengeId { get; set; } // Add this property
        public required Challenge Challenge { get; set; }
        public required string PathImage { get; set; }
    }
}
