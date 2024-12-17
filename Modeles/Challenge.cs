namespace CyberMind_API.Modeles
{
    public class Challenge
    {
        public uint Id { get; set; }
        public required string Titre { get; set; }
        public required string Enonce { get; set; }
        public uint EtablissementId { get; set; }
        public Etablissement? Etablissement { get; set; }
        public ICollection<Reponse>? Reponse { get; set; }
        public ImageEnonce? ImageEnonces { get; set; }
        public required string Categories { get; set; }
    }
}
