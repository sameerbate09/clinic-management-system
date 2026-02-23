using System;
using System.Collections.Generic;
using Clinic.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Persistence;

public partial class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enquiry> Enquiries { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }

    public virtual DbSet<PrescriptionTherapy> PrescriptionTherapies { get; set; }

    public virtual DbSet<Therapy> Therapies { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.EnquiryId).HasName("PK__Enquirie__0A019B7D3D733AED");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F2128906B0CCED0");

            entity.HasIndex(e => e.MedicineGuid, "UQ_Medicines_MedicineGuid").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MedicineGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientGuid).HasName("PK_Patients_PatientGuid");

            entity.HasIndex(e => e.Mobile, "IX_Patients_Mobile");

            entity.HasIndex(e => e.PatientGuid, "UQ_Patients_PatientGuid").IsUnique();

            entity.HasIndex(e => e.Mobile, "UQ__Patients__6FAE078238AE7ED7").IsUnique();

            entity.Property(e => e.PatientGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Concern).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130832222A1EF2");

            entity.HasIndex(e => e.PrescriptionGuid, "UQ_Prescriptions_PrescriptionGuid").IsUnique();

            entity.HasIndex(e => e.VisitId, "UQ_Prescriptions_Visit").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FinalizedAt).HasColumnType("datetime");
            entity.Property(e => e.NextFollowUpDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PrescriptionGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Visit).WithMany(p => p.PrescriptionVisits)
                .HasPrincipalKey(p => p.VisitGuid)
                .HasForeignKey(d => d.VisitGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Visits_Guid");

            entity.HasOne(d => d.VisitNavigation).WithOne(p => p.PrescriptionVisitNavigation)
                .HasForeignKey<Prescription>(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Visits");
        });

        modelBuilder.Entity<PrescriptionMedicine>(entity =>
        {
            entity.HasKey(e => e.PrescriptionMedicineId).HasName("PK__Prescrip__2C5AC236380CDDBC");

            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Instructions).HasMaxLength(250);
            entity.Property(e => e.MedicineName).HasMaxLength(150);

            entity.HasOne(d => d.Medicine).WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionMedicines_Medicines");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionMedicinePrescriptions)
                .HasPrincipalKey(p => p.PrescriptionGuid)
                .HasForeignKey(d => d.PrescriptionGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionMedicines_Prescriptions_Guid");

            entity.HasOne(d => d.PrescriptionNavigation).WithMany(p => p.PrescriptionMedicinePrescriptionNavigations)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionMedicines_Prescriptions");
        });

        modelBuilder.Entity<PrescriptionTherapy>(entity =>
        {
            entity.HasKey(e => e.PrescriptionTherapyId).HasName("PK__Prescrip__07ECFFB415225663");

            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionTherapyPrescriptions)
                .HasPrincipalKey(p => p.PrescriptionGuid)
                .HasForeignKey(d => d.PrescriptionGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionTherapies_Prescriptions_Guid");

            entity.HasOne(d => d.PrescriptionNavigation).WithMany(p => p.PrescriptionTherapyPrescriptionNavigations)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionTherapies_Prescriptions");

            entity.HasOne(d => d.Therapy).WithMany(p => p.PrescriptionTherapies)
                .HasForeignKey(d => d.TherapyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionTherapies_Therapies");
        });

        modelBuilder.Entity<Therapy>(entity =>
        {
            entity.HasKey(e => e.TherapyId).HasName("PK__Therapie__2D1FD1E2D65BA3BC");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TherapyName).HasMaxLength(150);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__Visits__4D3AA1DE7F3FBDFE");

            entity.HasIndex(e => e.VisitGuid, "UQ_Visits_VisitGuid").IsUnique();

            entity.Property(e => e.Complaint).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.VisitDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VisitGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PatientGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visits_Patients_Guid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
