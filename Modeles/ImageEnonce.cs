namespace CyberMind_API.Modeles;
using CyberMind_API.Modeles;

public class ImageEnonce
{
    public uint Id { get; set; }
    public uint ChallengeId { get; set; } // Add this property
    public required Challenge Challenge { get; set; }
    public required string PathImage { get; set; }

}