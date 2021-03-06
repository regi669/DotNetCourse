// <auto-generated />
using System;
using DotNetCourseNew.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetCourseNew.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20220627092335_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DotNetCourseNew.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AdressId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool?>("HasDelivery")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Address", b =>
                {
                    b.HasOne("DotNetCourseNew.Entities.Restaurant", "Restaurant")
                        .WithOne("Address")
                        .HasForeignKey("DotNetCourseNew.Entities.Address", "RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Dish", b =>
                {
                    b.HasOne("DotNetCourseNew.Entities.Restaurant", "Restaurant")
                        .WithMany("Dishes")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Restaurant", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
