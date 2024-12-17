﻿// <auto-generated />
using System;
using CyberMind_API.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CyberMind_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241217135122_MigrationEval3")]
    partial class MigrationEval3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CyberMind_API.Modeles.Challenge", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enonce")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("EtablissementId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementId");

                    b.ToTable("Challenge");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ChallengeDone", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("ChallengeId")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("DateSubmit")
                        .HasColumnType("datetime(6)");

                    b.Property<uint?>("ImageReponseId")
                        .HasColumnType("int unsigned");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<uint>("UserId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("ImageReponseId");

                    b.HasIndex("UserId");

                    b.ToTable("ChallengeDones");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Etablissement", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("CodeInscription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Etablissements");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ImageEnonce", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<uint>("ChallengeId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("PathImage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId")
                        .IsUnique();

                    b.ToTable("ImageEnonces");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ImageReponse", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("PathImage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ImageReponses");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Reponse", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint?>("ChallengeDoneId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("ChallengeId")
                        .HasColumnType("int unsigned");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<uint>("UserId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeDoneId");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("UserId");

                    b.ToTable("Reponses");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Role", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<uint>("EtablissementId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<uint>("roleId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementId");

                    b.HasIndex("roleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Challenge", b =>
                {
                    b.HasOne("CyberMind_API.Modeles.Etablissement", "Etablissement")
                        .WithMany("Challenges")
                        .HasForeignKey("EtablissementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissement");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ChallengeDone", b =>
                {
                    b.HasOne("CyberMind_API.Modeles.Challenge", "Challenge")
                        .WithMany()
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CyberMind_API.Modeles.ImageReponse", "ImageReponse")
                        .WithMany()
                        .HasForeignKey("ImageReponseId");

                    b.HasOne("CyberMind_API.Modeles.User", "User")
                        .WithMany("ChallengeDones")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");

                    b.Navigation("ImageReponse");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ImageEnonce", b =>
                {
                    b.HasOne("CyberMind_API.Modeles.Challenge", "Challenge")
                        .WithOne("ImageEnonces")
                        .HasForeignKey("CyberMind_API.Modeles.ImageEnonce", "ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Reponse", b =>
                {
                    b.HasOne("CyberMind_API.Modeles.ChallengeDone", null)
                        .WithMany("Reponse")
                        .HasForeignKey("ChallengeDoneId");

                    b.HasOne("CyberMind_API.Modeles.Challenge", "Challenge")
                        .WithMany("Reponse")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CyberMind_API.Modeles.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.User", b =>
                {
                    b.HasOne("CyberMind_API.Modeles.Etablissement", "Etablissement")
                        .WithMany()
                        .HasForeignKey("EtablissementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CyberMind_API.Modeles.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissement");

                    b.Navigation("role");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Challenge", b =>
                {
                    b.Navigation("ImageEnonces");

                    b.Navigation("Reponse");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.ChallengeDone", b =>
                {
                    b.Navigation("Reponse");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.Etablissement", b =>
                {
                    b.Navigation("Challenges");
                });

            modelBuilder.Entity("CyberMind_API.Modeles.User", b =>
                {
                    b.Navigation("ChallengeDones");
                });
#pragma warning restore 612, 618
        }
    }
}