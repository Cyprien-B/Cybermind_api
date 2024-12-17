using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class ChallengeDoneDTO
    {
        public uint Id { get; set; }
        public required User User { get; set; }
        public ImageReponse? ImageReponse { get; set; }
        public required Challenge Challenge { get; set; }
        public ICollection<Reponse>? Reponse { get; set; }
    }
}