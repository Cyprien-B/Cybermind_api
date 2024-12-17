using CyberMind_API.Modeles;

namespace CyberMind_API.DTOs
{
    public class ChallengeDTO
    {
        public uint Id { get; set; }
        public required string Titre { get; set; }
        public required string Enonce { get; set; }
        public uint EtablissementId { get; set; }
        public Etablissement? Etablissement { get; set; }
        public ImageEnonce? ImageEnonces { get; set; }
        public required string Categories { get; set; }
        public ICollection<Reponse>? Reponse { get; set; }
    }
}
