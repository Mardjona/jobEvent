using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using jobEvent.Models;

namespace jobEvent.Context;

public partial class MarsyBaseContext : DbContext
{
    public MarsyBaseContext()
    {
    }

    public MarsyBaseContext(DbContextOptions<MarsyBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Absencestype> Absencestypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventtype> Eventtypes { get; set; }

    public virtual DbSet<Kabinet> Kabinets { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Training> Trainings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=89.110.53.87;Port=5522;Database=marsy_base;Username=marsy;password=jona");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("absences_pkey");

            entity.ToTable("absences", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Substituteemployeeid).HasColumnName("substituteemployeeid");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Employee).WithMany(p => p.AbsenceEmployees)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("absences_employeeid_fkey");

            entity.HasOne(d => d.Substituteemployee).WithMany(p => p.AbsenceSubstituteemployees)
                .HasForeignKey(d => d.Substituteemployeeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("absences_substituteemployeeid_fkey");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("absences_absencestype_fk");
        });

        modelBuilder.Entity<Absencestype>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("absencestype_pk");

            entity.ToTable("absencestype", "Working");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");

            entity.ToTable("departments", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Departmentdirector).HasColumnName("departmentdirector");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Mainsupervisor).HasColumnName("mainsupervisor");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.DepartmentdirectorNavigation).WithMany(p => p.InverseDepartmentdirectorNavigation)
                .HasForeignKey(d => d.Departmentdirector)
                .HasConstraintName("departments_departments_fk");

            entity.HasOne(d => d.Departmentdirector1).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Departmentdirector)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("departments_managerid_fkey");

            entity.HasMany(d => d.Departmentdepends).WithMany(p => p.Departments)
                .UsingEntity<Dictionary<string, object>>(
                    "Departmentdepend",
                    r => r.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentdependId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("departmentdepend_departments_fk_1"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("departmentdepend_departments_fk"),
                    j =>
                    {
                        j.HasKey("DepartmentId", "DepartmentdependId").HasName("departmentdepend_pk");
                        j.ToTable("departmentdepend", "Working");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("department_id");
                        j.IndexerProperty<int>("DepartmentdependId").HasColumnName("departmentdepend_id");
                    });

            entity.HasMany(d => d.Departments).WithMany(p => p.Departmentdepends)
                .UsingEntity<Dictionary<string, object>>(
                    "Departmentdepend",
                    r => r.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("departmentdepend_departments_fk"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentdependId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("departmentdepend_departments_fk_1"),
                    j =>
                    {
                        j.HasKey("DepartmentId", "DepartmentdependId").HasName("departmentdepend_pk");
                        j.ToTable("departmentdepend", "Working");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("department_id");
                        j.IndexerProperty<int>("DepartmentdependId").HasColumnName("departmentdepend_id");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assistantid).HasColumnName("assistantid");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Mobilephone)
                .HasMaxLength(20)
                .HasColumnName("mobilephone");
            entity.Property(e => e.Office).HasColumnName("office");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.Supervisorid).HasColumnName("supervisorid");
            entity.Property(e => e.Workphone)
                .HasMaxLength(20)
                .HasColumnName("workphone");

            entity.HasOne(d => d.Assistant).WithMany(p => p.InverseAssistant)
                .HasForeignKey(d => d.Assistantid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_assistantid_fkey");

            entity.HasOne(d => d.OfficeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Office)
                .HasConstraintName("employees_kabinet_fk");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Position)
                .HasConstraintName("employees_departments_fk");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.Supervisorid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_supervisorid_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddatetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Responsibleemployeeid).HasColumnName("responsibleemployeeid");
            entity.Property(e => e.Startdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdatetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Responsibleemployee).WithMany(p => p.Events)
                .HasForeignKey(d => d.Responsibleemployeeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("events_responsibleemployeeid_fkey");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_eventtype_fk");

            entity.HasMany(d => d.Employees).WithMany(p => p.EventsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Eventparticipant",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("Employeeid")
                        .HasConstraintName("eventparticipants_employeeid_fkey"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("Eventid")
                        .HasConstraintName("eventparticipants_eventid_fkey"),
                    j =>
                    {
                        j.HasKey("Eventid", "Employeeid").HasName("eventparticipants_pkey");
                        j.ToTable("eventparticipants", "Working");
                        j.IndexerProperty<int>("Eventid").HasColumnName("eventid");
                        j.IndexerProperty<int>("Employeeid").HasColumnName("employeeid");
                    });
        });

        modelBuilder.Entity<Eventtype>(entity =>
        {
            entity.HasKey(e => e.EventtypeId).HasName("eventtype_pk");

            entity.ToTable("eventtype", "Working");

            entity.Property(e => e.EventtypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("eventtype_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Kabinet>(entity =>
        {
            entity.HasKey(e => e.KabinetId).HasName("kabinet_pk");

            entity.ToTable("kabinet", "Working");

            entity.Property(e => e.KabinetId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("kabinet_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materials_pkey");

            entity.ToTable("materials", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Approvaldate).HasColumnName("approvaldate");
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .HasColumnName("area");
            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Modificationdate).HasColumnName("modificationdate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Author).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Authorid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("materials_authorid_fkey");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("resumes_pkey");

            entity.ToTable("resumes", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicationdate).HasColumnName("applicationdate");
            entity.Property(e => e.Candidatename)
                .HasMaxLength(255)
                .HasColumnName("candidatename");
            entity.Property(e => e.Direction)
                .HasMaxLength(255)
                .HasColumnName("direction");
            entity.Property(e => e.Resumetext).HasColumnName("resumetext");
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainings_pkey");

            entity.ToTable("trainings", "Working");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasMany(d => d.Employees).WithMany(p => p.Training)
                .UsingEntity<Dictionary<string, object>>(
                    "Trainingparticipant",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("Employeeid")
                        .HasConstraintName("trainingparticipants_employeeid_fkey"),
                    l => l.HasOne<Training>().WithMany()
                        .HasForeignKey("Trainingid")
                        .HasConstraintName("trainingparticipants_trainingid_fkey"),
                    j =>
                    {
                        j.HasKey("Trainingid", "Employeeid").HasName("trainingparticipants_pkey");
                        j.ToTable("trainingparticipants", "Working");
                        j.IndexerProperty<int>("Trainingid").HasColumnName("trainingid");
                        j.IndexerProperty<int>("Employeeid").HasColumnName("employeeid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
