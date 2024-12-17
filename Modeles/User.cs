using CyberMind_API.Modeles;

namespace CyberMind_API.Modeles
{
    public class User
    {
        public int Id { get; set; }

        public uint EtablissementId { get; set; }
        public Etablissement? Etablissement { get; set; }
        public required string Name { get; set; }
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public ICollection<ChallengeDone>? ChallengeDones { get; set; }
        public required Role role { get; set; }
        public int Point { get; set; }

    }
}
