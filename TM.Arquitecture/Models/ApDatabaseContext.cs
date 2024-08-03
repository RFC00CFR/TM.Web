using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TM.Arquitecture.Models;

public partial class ApDatabaseContext : DbContext
{
    public ApDatabaseContext()
    {
    }

    public ApDatabaseContext(DbContextOptions<ApDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketsEmpleado> TicketsEmpleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PCB1OR7;Database=AP_database;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC07DBC63208");

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
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC078DEC4BFF");

            entity.Property(e => e.AsignadoA).HasColumnName("asignado_a");
            entity.Property(e => e.AsignadoPor).HasColumnName("asignado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(5000)
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

            entity.HasOne(d => d.AsignadoANavigation).WithMany(p => p.TicketAsignadoANavigations)
                .HasForeignKey(d => d.AsignadoA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__asignad__276EDEB3");

            entity.HasOne(d => d.AsignadoPorNavigation).WithMany(p => p.TicketAsignadoPorNavigations)
                .HasForeignKey(d => d.AsignadoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__asignad__267ABA7A");
        });

        modelBuilder.Entity<TicketsEmpleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticketsE__3214EC076ABE0A8E");

            entity.ToTable("ticketsEmpleado");

            entity.HasOne(d => d.Empleado).WithMany(p => p.TicketsEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ticketsEm__Emple__2A4B4B5E");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketsEmpleados)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ticketsEm__Ticke__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
