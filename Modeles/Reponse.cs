namespace CyberMind_API.Modeles
{
    public class Reponse
    {
        public uint Id { get; set; }
        public required int Score { get; set; }
        public required string Answer { get; set; }
        public required Challenge Challenge { get; set; }
        public required User User { get; set; }
    }
}
