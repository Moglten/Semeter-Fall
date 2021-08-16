using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Semester_Fall.Models
{
    public partial class SemesterDbContext : DbContext
    {
        public SemesterDbContext()
        {
        }

        public SemesterDbContext(DbContextOptions<SemesterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<ProfTeaches> ProfTeaches { get; set; }
        public virtual DbSet<Professors> Professors { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<TimmingSchedule> TimmingSchedule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SemesterDb;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Courses__A25C5AA6DCA348CC");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Consumedperweek).HasColumnName("consumedperweek");

                entity.Property(e => e.Contacthoursperweek).HasColumnName("contacthoursperweek");

                entity.Property(e => e.Coursename)
                    .HasColumnName("coursename")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Halls>(entity =>
            {
                entity.HasKey(e => e.Hallid)
                    .HasName("PK__ClassRoo__CB1927C09F997299");

                entity.Property(e => e.Hallid).HasColumnName("hallid");

                entity.Property(e => e.Hallname)
                    .HasColumnName("hallname")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfTeaches>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Profid });

                entity.HasIndex(e => new { e.Code, e.Profid })
                    .HasName("uc_codeProff")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Profid).HasColumnName("profid");

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.ProfTeaches)
                    .HasForeignKey(d => d.Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProfTeach__Code__30F848ED");

                entity.HasOne(d => d.Prof)
                    .WithMany(p => p.ProfTeaches)
                    .HasForeignKey(d => d.Profid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProfTeach__Proff__31EC6D26");
            });

            modelBuilder.Entity<Professors>(entity =>
            {
                entity.HasKey(e => e.Profid)
                    .HasName("PK__Professo__BA10A5B3F816B929");

                entity.Property(e => e.Profid).HasColumnName("profid");

                entity.Property(e => e.Committedto).HasColumnName("committedto");

                entity.Property(e => e.Professorname)
                    .IsRequired()
                    .HasColumnName("professorname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Teachingload).HasColumnName("teachingload");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Profid ,e.Hallid,e.Timeid});

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Hallid).HasColumnName("hallid");

                entity.Property(e => e.Profid).HasColumnName("profid");

                entity.Property(e => e.Timeid).HasColumnName("timeid");

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__code__4CA06362");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Hallid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__hallid__4D94879B");

                entity.HasOne(d => d.Prof)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Profid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__profid__4E88ABD4");

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Timeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__timeid__4F7CD00D");
            });

            modelBuilder.Entity<TimmingSchedule>(entity =>
            {
                entity.HasKey(e => e.Timeid)
                    .HasName("PK__TimmingS__E04ED94787526146");

                entity.Property(e => e.Timeid).HasColumnName("timeid");

                entity.Property(e => e.Occupied).HasColumnName("occupied");

                entity.Property(e => e.Period).HasColumnName("period");

                entity.Property(e => e.Timmingperiod)
                    .HasColumnName("timmingperiod")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
