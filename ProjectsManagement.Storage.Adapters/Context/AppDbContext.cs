using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Invitations;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.Storage.Adapters.Model;

namespace ProjectsManagement.Storage.Adapters.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }
    public AppDbContext() : base() { }
    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<ActivityPrecedent> ActivityPrecedents { get; set; }

    public virtual DbSet<ActivityResourceType> ActivityResourceTypes { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<ContributionMember> ContributionMembers { get; set; }

    public virtual DbSet<ContributionType> ContributionTypes { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<InvitationStatus> InvitationStatuses { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<ProjectTaskStatus> ProjectTaskStatus { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Host=172.29.3.110;Port=5466;Database=ProjectManagement;Username=projects;Password=projects@1234");
    //    }
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectActivities");

            entity.ToTable("Activity");

            entity.HasIndex(e => e.ActivityResourceType, "IX_ProjectActivities_ProjectActivityResourceTypeId");

            entity.HasIndex(e => e.ActivityType, "IX_ProjectActivities_ProjectActivityTypeId");

            entity.HasIndex(e => e.Project, "IX_ProjectActivities_ProjectId");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ActivityResourceTypeNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityResourceType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectActivities_ProjectActivityResourceTypes_ProjectActivityResourceTypeId");

            entity.HasOne(d => d.ActivityTypeNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectActivities_ProjectActivityTypes_ProjectActivityTypeId");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("FK_ProjectActivities_Projects_ProjectId");
        });

        modelBuilder.Entity<ActivityPrecedent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectActivityPrecedents");

            entity.HasIndex(e => e.Precedent, "IX_ProjectActivityPrecedents_ActivityPrecedentId");

            entity.HasIndex(e => e.Activity, "IX_ProjectActivityPrecedents_ProjectActivityId");

            entity.HasOne(d => d.ActivityNavigation).WithMany(p => p.ActivityPrecedentActivityNavigations)
                .HasForeignKey(d => d.Activity)
                .HasConstraintName("FK_ProjectActivityPrecedents_ProjectActivities_ProjectActivityId");

            entity.HasOne(d => d.PrecedentNavigation).WithMany(p => p.ActivityPrecedentPrecedentNavigations)
                .HasForeignKey(d => d.Precedent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectActivityPrecedents_ProjectActivities_ActivityPrecedentId");
        });

        modelBuilder.Entity<ActivityResourceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectActivityResourceTypes");

            entity.ToTable("ActivityResourceType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectActivityTypes");

            entity.ToTable("ActivityType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContributionMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ContributionMembers");

            entity.ToTable("ContributionMember");

            entity.HasIndex(e => e.ContributionType, "IX_ContributionMembers_ContributionTypeId");

            entity.HasIndex(e => e.Project, "IX_ContributionMembers_ProjectId");

            entity.HasOne(d => d.ContributionTypeNavigation).WithMany(p => p.ContributionMembers)
                .HasForeignKey(d => d.ContributionType)
                .HasConstraintName("FK_ContributionMembers_ContributionTypes_ContributionTypeId");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.ContributionMembers)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("FK_ContributionMembers_Projects_ProjectId");
        });

        modelBuilder.Entity<ContributionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ContributionTypes");

            entity.ToTable("ContributionType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Invitations");

            entity.ToTable("Invitation");

            entity.HasIndex(e => e.InvitationStatus, "IX_Invitations_InvitationStatusId");

            entity.HasIndex(e => e.Project, "IX_Invitations_ProjectId");

            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.InvitationStatusNavigation).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.InvitationStatus)
                .HasConstraintName("FK_Invitations_InvitationStatuses_InvitationStatusId");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("FK_Invitations_Projects_ProjectId");
        });

        modelBuilder.Entity<InvitationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InvitationStatuses");

            entity.ToTable("InvitationStatus");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Projects");

            entity.ToTable("Project");

            entity.HasIndex(e => e.ProjectType, "IX_Projects_ProjectTypeId");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProjectTypeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectType)
                .HasConstraintName("FK_Projects_ProjectTypes_ProjectTypeId");
        });

        modelBuilder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectTypes");

            entity.ToTable("ProjectType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectTasks");

            entity.ToTable("Task");

            entity.HasIndex(e => e.Project, "IX_ProjectTasks_ProjectId");

            entity.HasIndex(e => e.TaskStatus, "IX_ProjectTasks_ProjectTaskStatusId");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("FK_ProjectTasks_Projects_ProjectId");

            entity.HasOne(d => d.TaskStatusNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskStatus)
                .HasConstraintName("FK_ProjectTasks_ProjectTaskStatuses_ProjectTaskStatusId");
        });

        modelBuilder.Entity<ProjectTaskStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectTaskStatuses");

            entity.ToTable("TaskStatus");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}