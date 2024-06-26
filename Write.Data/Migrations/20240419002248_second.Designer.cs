﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Write.Data.EF;

#nullable disable

namespace Write.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240419002248_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IqUser", b =>
                {
                    b.Property<Guid>("IqAssignedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IqAssignedId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("IqUser");
                });

            modelBuilder.Entity("Write.Contacts.Entities.Iq", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("building_name");

                    b.HasKey("Id")
                        .HasName("iq_pkey");

                    b.HasIndex("BuildingName")
                        .IsUnique();

                    b.ToTable("iqs", (string)null);
                });

            modelBuilder.Entity("Write.Contacts.Entities.Lock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessLevel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("access_level");

                    b.Property<Guid>("IqId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("lock_pkey");

                    b.HasIndex("IqId");

                    b.ToTable("locks", (string)null);
                });

            modelBuilder.Entity("Write.Contacts.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessLevel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("access_level");

                    b.HasKey("Id")
                        .HasName("user_pkey");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("IqUser", b =>
                {
                    b.HasOne("Write.Contacts.Entities.Iq", null)
                        .WithMany()
                        .HasForeignKey("IqAssignedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Write.Contacts.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Write.Contacts.Entities.Lock", b =>
                {
                    b.HasOne("Write.Contacts.Entities.Iq", "Iq")
                        .WithMany("Locks")
                        .HasForeignKey("IqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Iq");
                });

            modelBuilder.Entity("Write.Contacts.Entities.Iq", b =>
                {
                    b.Navigation("Locks");
                });
#pragma warning restore 612, 618
        }
    }
}
