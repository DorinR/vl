﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webapitest.Data;

#nullable disable

namespace webapitest.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    [Migration("20240724014829_distortions-table-added")]
    partial class distortionstableadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

            modelBuilder.Entity("webapitest.Repository.Models.Thought", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("text");

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
                    b.Navigation("Thoughts");
                });
#pragma warning restore 612, 618
        }
    }
}