﻿// <auto-generated />
using CapaDatos.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapaDatos.Migrations
{
    [DbContext(typeof(GlobalContext))]
    [Migration("20201118121108_Colores")]
    partial class Colores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CapaDominio.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Red"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Blue"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "White"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Brown"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}