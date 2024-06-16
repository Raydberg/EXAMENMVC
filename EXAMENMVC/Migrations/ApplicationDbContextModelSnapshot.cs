﻿// <auto-generated />
using System;
using EXAMENMVC.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EXAMENMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EXAMENMVC.Models.Marca", b =>
                {
                    b.Property<int>("IDMARCA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDMARCA"), 1L, 1);

                    b.Property<string>("NOM_MARCA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDMARCA");

                    b.ToTable("Marcas");

                    b.HasData(
                        new
                        {
                            IDMARCA = 1,
                            NOM_MARCA = "Toyota"
                        },
                        new
                        {
                            IDMARCA = 2,
                            NOM_MARCA = "BMW"
                        },
                        new
                        {
                            IDMARCA = 3,
                            NOM_MARCA = "Ford"
                        });
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.Property<int>("IDMODELO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDMODELO"), 1L, 1);

                    b.Property<int>("MarcaIDMARCA")
                        .HasColumnType("int");

                    b.Property<string>("NOM_MODELO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDMODELO");

                    b.HasIndex("MarcaIDMARCA");

                    b.ToTable("Modelos");

                    b.HasData(
                        new
                        {
                            IDMODELO = 1,
                            MarcaIDMARCA = 1,
                            NOM_MODELO = "Corolla"
                        },
                        new
                        {
                            IDMODELO = 2,
                            MarcaIDMARCA = 1,
                            NOM_MODELO = "Camry"
                        },
                        new
                        {
                            IDMODELO = 3,
                            MarcaIDMARCA = 1,
                            NOM_MODELO = "RAV4"
                        },
                        new
                        {
                            IDMODELO = 4,
                            MarcaIDMARCA = 2,
                            NOM_MODELO = "Serie 3"
                        },
                        new
                        {
                            IDMODELO = 5,
                            MarcaIDMARCA = 2,
                            NOM_MODELO = "X5"
                        },
                        new
                        {
                            IDMODELO = 6,
                            MarcaIDMARCA = 2,
                            NOM_MODELO = "i8"
                        },
                        new
                        {
                            IDMODELO = 7,
                            MarcaIDMARCA = 3,
                            NOM_MODELO = "Mustang"
                        },
                        new
                        {
                            IDMODELO = 8,
                            MarcaIDMARCA = 3,
                            NOM_MODELO = "F-150"
                        },
                        new
                        {
                            IDMODELO = 9,
                            MarcaIDMARCA = 3,
                            NOM_MODELO = "Explorer"
                        });
                });

            modelBuilder.Entity("EXAMENMVC.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Vehiculo", b =>
                {
                    b.Property<int>("IDVEHICULO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDVEHICULO"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModeloIDMODELO")
                        .HasColumnType("int");

                    b.Property<string>("NRO_PLACA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("año")
                        .HasColumnType("datetime2");

                    b.HasKey("IDVEHICULO");

                    b.HasIndex("ModeloIDMODELO");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.HasOne("EXAMENMVC.Models.Marca", "Marca")
                        .WithMany("Modelos")
                        .HasForeignKey("MarcaIDMARCA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Vehiculo", b =>
                {
                    b.HasOne("EXAMENMVC.Models.Modelo", "Modelo")
                        .WithMany("Vehiculos")
                        .HasForeignKey("ModeloIDMODELO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Marca", b =>
                {
                    b.Navigation("Modelos");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.Navigation("Vehiculos");
                });
#pragma warning restore 612, 618
        }
    }
}
