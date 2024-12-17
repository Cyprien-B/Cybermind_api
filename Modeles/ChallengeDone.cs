namespace CyberMind_API.Modeles
{
    public class ChallengeDone
    {
        public uint Id { get; set; }
        public required User User { get; set; }
        public ImageReponse? ImageReponse { get; set; }
        public required Challenge Challenge { get; set; }
        public ICollection<Reponse>? Reponse { get; set; }
        public required string Answer { get; set; }
        public int Score { get; set; }
        public DateTime DateSubmit { get; set; }
    }
}
