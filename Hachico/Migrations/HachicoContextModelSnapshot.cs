﻿// <auto-generated />
using Hachico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Hachico.Migrations
{
    [DbContext(typeof(HachicoContext))]
    partial class HachicoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hachico.Models.Device", b =>
                {
                    b.Property<string>("SSID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("Status");

                    b.HasKey("SSID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Hachico.Models.DeviceDetail", b =>
                {
                    b.Property<string>("SSID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Battery");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("EditDate");

                    b.Property<bool>("Status");

                    b.Property<int>("TypeException");

                    b.HasKey("SSID");

                    b.ToTable("DeviceDetails");
                });

            modelBuilder.Entity("Hachico.Models.ImagePet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.Property<Guid>("PetId");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("ImagePets");
                });

            modelBuilder.Entity("Hachico.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("SSID");

                    b.Property<int>("Status");

                    b.Property<Guid>("UserId");

                    b.Property<double>("lat");

                    b.Property<double>("lng");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Hachico.Models.Login", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("AccessToken")
                        .IsRequired();

                    b.Property<string>("FacebookId")
                        .IsRequired();

                    b.Property<string>("OneSignalID");

                    b.Property<string>("Provider")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("UserId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Hachico.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DayOfBirth");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Gender");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NumberInformation")
                        .IsRequired();

                    b.Property<string>("SSID")
                        .IsRequired();

                    b.Property<int>("TypeAnimalId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("SSID");

                    b.HasIndex("TypeAnimalId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Hachico.Models.PetPermission", b =>
                {
                    b.Property<Guid>("PetId");

                    b.Property<Guid>("UserId");

                    b.Property<int>("Type");

                    b.HasKey("PetId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PetPermissions");
                });

            modelBuilder.Entity("Hachico.Models.PetStatus", b =>
                {
                    b.Property<Guid>("PetId");

                    b.Property<int>("Type");

                    b.HasKey("PetId");

                    b.ToTable("PetStatuses");
                });

            modelBuilder.Entity("Hachico.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Hachico.Models.Student", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Hachico.Models.TypeAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("TypeAnimals");
                });

            modelBuilder.Entity("Hachico.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email")
                        .HasMaxLength(500);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("PhoneId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hachico.Models.Device", b =>
                {
                    b.HasOne("Hachico.Models.DeviceDetail", "DeviceDetail")
                        .WithMany()
                        .HasForeignKey("SSID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.ImagePet", b =>
                {
                    b.HasOne("Hachico.Models.Pet", "Pet")
                        .WithMany("Images")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.Login", b =>
                {
                    b.HasOne("Hachico.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.Pet", b =>
                {
                    b.HasOne("Hachico.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("SSID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hachico.Models.TypeAnimal", "Type")
                        .WithMany("Pets")
                        .HasForeignKey("TypeAnimalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.PetPermission", b =>
                {
                    b.HasOne("Hachico.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hachico.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.PetStatus", b =>
                {
                    b.HasOne("Hachico.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hachico.Models.User", b =>
                {
                    b.HasOne("Hachico.Models.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
