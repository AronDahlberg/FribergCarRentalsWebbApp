﻿// <auto-generated />
using System;
using FribergCarRentalsWebbApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FribergCarRentalsWebbApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250203110039_Inital")]
    partial class Inital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingCreation")
                        .HasColumnType("datetime2");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerAccountId")
                        .HasColumnType("int");

                    b.Property<bool?>("DamagedAtReturn")
                        .HasColumnType("bit");

                    b.Property<int?>("DeltaMileageAtReturn")
                        .HasColumnType("int");

                    b.Property<DateTime>("DropoffDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("InvalidationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PickupDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("UncleanedAtReturn")
                        .HasColumnType("bit");

                    b.Property<bool?>("UnfilledTankAtReturn")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ListingCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Unlisted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ConfirmationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOfPayment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdditionalMileagePriceInterval")
                        .HasColumnType("int");

                    b.Property<int>("AdditionalMileagePricePerInterval")
                        .HasColumnType("int");

                    b.Property<int>("AllowedDailyMileage")
                        .HasColumnType("int");

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("DailyPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("PriceCreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Booking", b =>
                {
                    b.HasOne("FribergCarRentalsWebbApp.Models.Car", "Car")
                        .WithMany("Bookings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FribergCarRentalsWebbApp.Models.Customer", "CustomerAccount")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("CustomerAccount");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Image", b =>
                {
                    b.HasOne("FribergCarRentalsWebbApp.Models.Car", null)
                        .WithMany("Images")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Payment", b =>
                {
                    b.HasOne("FribergCarRentalsWebbApp.Models.Booking", "Booking")
                        .WithMany("Payments")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Price", b =>
                {
                    b.HasOne("FribergCarRentalsWebbApp.Models.Car", null)
                        .WithMany("Prices")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Booking", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Car", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Images");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("FribergCarRentalsWebbApp.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
