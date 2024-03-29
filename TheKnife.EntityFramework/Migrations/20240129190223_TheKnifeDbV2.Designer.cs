﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheKnife.EntityFramework;

#nullable disable

namespace TheKnife.EntityFramework.Migrations
{
    [DbContext(typeof(TheKnifeDbContext))]
    [Migration("20240129190223_TheKnifeDbV2")]
    partial class TheKnifeDbV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheKnife.Entities.Efos.CommentsEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateOnly>("CommentDate")
                        .HasColumnType("date");

                    b.Property<int>("Restaurant_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Review")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Restaurant_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.ContactsEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Contacts", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RegisterUsersEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("RegisterUsers", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.ReservationsEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberPeople")
                        .HasColumnType("int");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ReservationDate")
                        .HasColumnType("date");

                    b.Property<string>("ReservationTime")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("Restaurant_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Restaurant_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RestaurantRegistrationsEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AveragePrice")
                        .HasColumnType("decimal(5, 1)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ClosingHours")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FlName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("NumberOfTables")
                        .HasColumnType("int");

                    b.Property<string>("OpeningDays")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("RName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RPhone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RestaurantRegistrations", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RestaurantResponsiblesEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FlName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("RImage")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("RestaurantRegistration_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantRegistration_Id")
                        .IsUnique();

                    b.ToTable("RestaurantResponsibles", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RestaurantsEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AveragePrice")
                        .HasColumnType("decimal(5, 1)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ClosingHours")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("NumberOfTables")
                        .HasColumnType("int");

                    b.Property<string>("OpeningDays")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("RName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RPhone")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantRegistration_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Rresponsible_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantRegistration_Id")
                        .IsUnique();

                    b.HasIndex("Rresponsible_Id")
                        .IsUnique();

                    b.ToTable("Restaurants", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.UsersEfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RegisterUser_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RegisterUser_Id")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.CommentsEfo", b =>
                {
                    b.HasOne("TheKnife.Entities.Efos.RestaurantsEfo", "Restaurants")
                        .WithMany()
                        .HasForeignKey("Restaurant_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheKnife.Entities.Efos.UsersEfo", "Users")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurants");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.ReservationsEfo", b =>
                {
                    b.HasOne("TheKnife.Entities.Efos.RestaurantsEfo", "Restaurants")
                        .WithMany()
                        .HasForeignKey("Restaurant_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheKnife.Entities.Efos.UsersEfo", "Users")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurants");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RestaurantResponsiblesEfo", b =>
                {
                    b.HasOne("TheKnife.Entities.Efos.RestaurantRegistrationsEfo", "RestaurantRegistrations")
                        .WithOne()
                        .HasForeignKey("TheKnife.Entities.Efos.RestaurantResponsiblesEfo", "RestaurantRegistration_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RestaurantRegistrations");
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.RestaurantsEfo", b =>
                {
                    b.HasOne("TheKnife.Entities.Efos.RestaurantRegistrationsEfo", "RestaurantRegistrations")
                        .WithOne()
                        .HasForeignKey("TheKnife.Entities.Efos.RestaurantsEfo", "RestaurantRegistration_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TheKnife.Entities.Efos.RestaurantResponsiblesEfo", "RestaurantResponsibles")
                        .WithOne()
                        .HasForeignKey("TheKnife.Entities.Efos.RestaurantsEfo", "Rresponsible_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RestaurantRegistrations");

                    b.Navigation("RestaurantResponsibles");
                });

            modelBuilder.Entity("TheKnife.Entities.Efos.UsersEfo", b =>
                {
                    b.HasOne("TheKnife.Entities.Efos.RegisterUsersEfo", "RegisterUsers")
                        .WithOne()
                        .HasForeignKey("TheKnife.Entities.Efos.UsersEfo", "RegisterUser_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RegisterUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
