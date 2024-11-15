﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webapitest.Data;

#nullable disable

namespace webapitest.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DistortionEvent", b =>
                {
                    b.Property<Guid>("DistortionsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventsId")
                        .HasColumnType("uuid");

                    b.HasKey("DistortionsId", "EventsId");

                    b.HasIndex("EventsId");

                    b.ToTable("DistortionEvent");
                });

            modelBuilder.Entity("DistortionThought", b =>
                {
                    b.Property<Guid>("DistortionsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ThoughtsId")
                        .HasColumnType("uuid");

                    b.HasKey("DistortionsId", "ThoughtsId");

                    b.HasIndex("ThoughtsId");

                    b.ToTable("DistortionThought");
                });

            modelBuilder.Entity("webapitest.Repository.Models.Distortions.Distortion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Distortion");
                });

            modelBuilder.Entity("webapitest.Repository.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Challenge")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ChallengePrompt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Emotions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Outcome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ResultingBehaviour")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Situation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Thoughts")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("webapitest.Repository.Models.Thought", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Thoughts");
                });

            modelBuilder.Entity("webapitest.Repository.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DistortionEvent", b =>
                {
                    b.HasOne("webapitest.Repository.Models.Distortions.Distortion", null)
                        .WithMany()
                        .HasForeignKey("DistortionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapitest.Repository.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DistortionThought", b =>
                {
                    b.HasOne("webapitest.Repository.Models.Distortions.Distortion", null)
                        .WithMany()
                        .HasForeignKey("DistortionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapitest.Repository.Models.Thought", null)
                        .WithMany()
                        .HasForeignKey("ThoughtsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapitest.Repository.Models.Event", b =>
                {
                    b.HasOne("webapitest.Repository.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapitest.Repository.Models.Thought", b =>
                {
                    b.HasOne("webapitest.Repository.Models.User", "User")
                        .WithMany("Thoughts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapitest.Repository.Models.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Thoughts");
                });
#pragma warning restore 612, 618
        }
    }
}
