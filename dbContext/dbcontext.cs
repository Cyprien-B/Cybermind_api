using CyberMind_API.Modeles;
using Microsoft.EntityFrameworkCore;

namespace CyberMind_API.dbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Challenge> Challenge { get; set; }
        public DbSet<ChallengeDone> ChallengeDones { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }
        public DbSet<ImageEnonce> ImageEnonces { get; set; }
        public DbSet<ImageReponse> ImageReponses { get; set; }
        public DbSet<Reponse> Reponses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.ImageEnonces)
                .WithOne(ie => ie.Challenge)
                .HasForeignKey<ImageEnonce>(ie => ie.ChallengeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Etablissement)
                .WithMany()
                .HasForeignKey(u => u.EtablissementId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
