namespace CyberMind_API.Modeles
{
    public class Etablissement
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        //public ICollection<User>? Users { get; set; }
        public ICollection<Challenge>? Challenges { get; set; }
        public required string CodeInscription { get; set; }
        //migration à faire
    }
}


