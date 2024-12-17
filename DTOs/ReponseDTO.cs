using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class ReponseDTO
    {
        public uint Id { get; set; }
        public required int Score { get; set; }
        public required string Answer { get; set; }
        public required Challenge Challenge { get; set; }
        public required User User { get; set; }
    }
}

