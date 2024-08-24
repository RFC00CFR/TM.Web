using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TM.Arquitecture.Models;

public partial class TmDatabaseContext : DbContext
{
    public TmDatabaseContext()
    {
    }

    public TmDatabaseContext(DbContextOptions<TmDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Server=DESKTOP-PCB1OR7;Database=TM_database;Trusted_Connection=True;TrustServerCertificate=True;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC07359CB27A");

            entity.ToTable("Empleado");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Departamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("puesto");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC078FE44E6D");

            entity.Property(e => e.AsignadoA).HasColumnName("asignado_a");
            entity.Property(e => e.AsignadoPor).HasColumnName("asignado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaDeAsignado).HasColumnName("fecha_de_asignado");
            entity.Property(e => e.FechaDeEntrega).HasColumnName("fecha_de_entrega");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("prioridad");

            entity.HasOne(d => d.AsignadoANavigation)
                .WithMany(p => p.TicketAsignadoANavigations)
                .HasForeignKey(d => d.AsignadoA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__asignad__276EDEB3");

            entity.HasOne(d => d.AsignadoPorNavigation)
                .WithMany(p => p.TicketAsignadoPorNavigations)
                .HasForeignKey(d => d.AsignadoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__asignad__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
