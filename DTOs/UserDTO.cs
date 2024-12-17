using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Etablissement? Etablissement { get; set; }
        public ICollection<ChallengeDone>? ChallengeDones { get; set; }
        public string ?Role { get; set; }

        public int Point {  get; set; }
    }
}