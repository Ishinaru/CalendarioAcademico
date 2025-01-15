﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241220143047_changeToStringNumPortaria")]
    partial class changeToStringNumPortaria
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Model.Calendario", b =>
                {
                    b.Property<int>("IdCalendario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCalendario"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NumeroResolucao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdCalendario");

                    b.ToTable("Calendarios");
                });

            modelBuilder.Entity("API.Model.Evento", b =>
                {
                    b.Property<int>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEvento"));

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCalendario")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<bool>("Importante")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("bit");

                    b.Property<int>("TipoFeriado")
                        .HasColumnType("int");

                    b.Property<bool>("UescFunciona")
                        .HasColumnType("bit");

                    b.HasKey("IdEvento");

                    b.HasIndex("IdCalendario");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("API.Model.Evento_Portaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataInicio")
                        .HasColumnType("date");

                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdPortaria")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdEvento");

                    b.HasIndex("IdPortaria");

                    b.ToTable("Evento_Portarias");
                });

            modelBuilder.Entity("API.Model.Historico", b =>
                {
                    b.Property<int>("IdHistorico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorico"));

                    b.Property<DateTime>("DataMudanca")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdCalendario")
                        .HasColumnType("int");

                    b.Property<int?>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int?>("IdPortaria")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdHistorico");

                    b.HasIndex("IdCalendario");

                    b.HasIndex("IdEvento");

                    b.HasIndex("IdPortaria");

                    b.ToTable("Historicos");
                });

            modelBuilder.Entity("API.Model.Portaria", b =>
                {
                    b.Property<int>("Id_Portaria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Portaria"));

                    b.Property<int>("AnoPortaria")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NumPortaria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Portaria");

                    b.ToTable("Portarias");
                });

            modelBuilder.Entity("API.Model.Evento", b =>
                {
                    b.HasOne("API.Model.Calendario", "Calendario")
                        .WithMany("Eventos")
                        .HasForeignKey("IdCalendario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendario");
                });

            modelBuilder.Entity("API.Model.Evento_Portaria", b =>
                {
                    b.HasOne("API.Model.Evento", "Evento")
                        .WithMany("Eventos_Portarias")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Model.Portaria", "Portaria")
                        .WithMany("Eventos_Portarias")
                        .HasForeignKey("IdPortaria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Portaria");
                });

            modelBuilder.Entity("API.Model.Historico", b =>
                {
                    b.HasOne("API.Model.Calendario", "Calendario")
                        .WithMany("Historicos")
                        .HasForeignKey("IdCalendario")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("API.Model.Evento", "Evento")
                        .WithMany("Historicos")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("API.Model.Portaria", "Portaria")
                        .WithMany("Historicos")
                        .HasForeignKey("IdPortaria")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Calendario");

                    b.Navigation("Evento");

                    b.Navigation("Portaria");
                });

            modelBuilder.Entity("API.Model.Calendario", b =>
                {
                    b.Navigation("Eventos");

                    b.Navigation("Historicos");
                });

            modelBuilder.Entity("API.Model.Evento", b =>
                {
                    b.Navigation("Eventos_Portarias");

                    b.Navigation("Historicos");
                });

            modelBuilder.Entity("API.Model.Portaria", b =>
                {
                    b.Navigation("Eventos_Portarias");

                    b.Navigation("Historicos");
                });
#pragma warning restore 612, 618
        }
    }
}
