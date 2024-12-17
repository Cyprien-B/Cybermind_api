namespace CyberMind_API.Modeles
{
    public class Role
    {

        public uint Id { get; set; }
        public required string Name { get; set; }
        public Role() { }
        public Role(uint unid, string v2)
        {
            Id = unid;
            Name = v2;
        }
  }
}
