﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonenOrt.Repository.Service.Context;

#nullable disable

namespace PersonenOrt.Repository.Service.Migrations
{
    [DbContext(typeof(PersonenOrtContext))]
    partial class PersonenOrtContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("PersonenOrt.Framework.Ort", b =>
                {
                    b.Property<string>("PLZ")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PLZ");

                    b.ToTable("Ort");
                });

            modelBuilder.Entity("PersonenOrt.Framework.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Geburtsdatum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OrtPLZ")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrtPLZ");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("PersonenOrt.Framework.Person", b =>
                {
                    b.HasOne("PersonenOrt.Framework.Ort", "Ort")
                        .WithMany()
                        .HasForeignKey("OrtPLZ")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ort");
                });
#pragma warning restore 612, 618
        }
    }
}