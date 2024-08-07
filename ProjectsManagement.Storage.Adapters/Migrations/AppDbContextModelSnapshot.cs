﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectsManagement.Storage.Adapters.Context;

#nullable disable

namespace ProjectsManagement.Storage.Adapters.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectsManagement.Core.Activities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityResourceType")
                        .HasColumnType("integer");

                    b.Property<int>("ActivityType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Project")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_ProjectActivities");

                    b.HasIndex(new[] { "ActivityResourceType" }, "IX_ProjectActivities_ProjectActivityResourceTypeId");

                    b.HasIndex(new[] { "ActivityType" }, "IX_ProjectActivities_ProjectActivityTypeId");

                    b.HasIndex(new[] { "Project" }, "IX_ProjectActivities_ProjectId");

                    b.ToTable("Activity", (string)null);
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityPrecedent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Activity")
                        .HasColumnType("integer");

                    b.Property<int>("Precedent")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_ProjectActivityPrecedents");

                    b.HasIndex(new[] { "Precedent" }, "IX_ProjectActivityPrecedents_ActivityPrecedentId");

                    b.HasIndex(new[] { "Activity" }, "IX_ProjectActivityPrecedents_ProjectActivityId");

                    b.ToTable("ActivityPrecedents");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityResourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_ProjectActivityResourceTypes");

                    b.ToTable("ActivityResourceType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Name = "Pdf"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Process Description"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Diagram"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Image"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_ProjectActivityTypes");

                    b.ToTable("ActivityType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Name = "Closed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Updated"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Initialized"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Merged"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Core.Contributions.ContributionMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContributionType")
                        .HasColumnType("integer");

                    b.Property<int>("Contributor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Project")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_ContributionMembers");

                    b.HasIndex(new[] { "ContributionType" }, "IX_ContributionMembers_ContributionTypeId");

                    b.HasIndex(new[] { "Project" }, "IX_ContributionMembers_ProjectId");

                    b.ToTable("ContributionMember", (string)null);
                });

            modelBuilder.Entity("ProjectsManagement.Core.Contributions.ContributionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_ContributionTypes");

                    b.ToTable("ContributionType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "Contributor"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Owner"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Core.Invitations.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Contributor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InvitationStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("Project")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_Invitations");

                    b.HasIndex(new[] { "InvitationStatus" }, "IX_Invitations_InvitationStatusId");

                    b.HasIndex(new[] { "Project" }, "IX_Invitations_ProjectId");

                    b.ToTable("Invitation", (string)null);
                });

            modelBuilder.Entity("ProjectsManagement.Core.ProjectTasks.ProjectTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Project")
                        .HasColumnType("integer");

                    b.Property<int>("TaskStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_ProjectTasks");

                    b.HasIndex(new[] { "Project" }, "IX_ProjectTasks_ProjectId");

                    b.HasIndex(new[] { "TaskStatus" }, "IX_ProjectTasks_ProjectTaskStatusId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("ProjectsManagement.Core.ProjectTasks.ProjectTaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_ProjectTaskStatuses");

                    b.ToTable("TaskStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Name = "Finished"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Failed"
                        },
                        new
                        {
                            Id = 1,
                            Name = "On Working"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Core.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ProjectType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_Projects");

                    b.HasIndex(new[] { "ProjectType" }, "IX_Projects_ProjectTypeId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ProjectsManagement.Core.Projects.ProjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_ProjectTypes");

                    b.ToTable("ProjectType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Public"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Private"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Storage.Adapters.Model.InvitationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id")
                        .HasName("PK_InvitationStatuses");

                    b.ToTable("InvitationStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rejected"
                        });
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.Activity", b =>
                {
                    b.HasOne("ProjectsManagement.Core.Activities.ActivityResourceType", "ActivityResourceTypeNavigation")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityResourceType")
                        .IsRequired()
                        .HasConstraintName("FK_ProjectActivities_ProjectActivityResourceTypes_ProjectActivityResourceTypeId");

                    b.HasOne("ProjectsManagement.Core.Activities.ActivityType", "ActivityTypeNavigation")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityType")
                        .IsRequired()
                        .HasConstraintName("FK_ProjectActivities_ProjectActivityTypes_ProjectActivityTypeId");

                    b.HasOne("ProjectsManagement.Core.Projects.Project", "ProjectNavigation")
                        .WithMany("Activities")
                        .HasForeignKey("Project")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectActivities_Projects_ProjectId");

                    b.Navigation("ActivityResourceTypeNavigation");

                    b.Navigation("ActivityTypeNavigation");

                    b.Navigation("ProjectNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityPrecedent", b =>
                {
                    b.HasOne("ProjectsManagement.Core.Activities.Activity", "ActivityNavigation")
                        .WithMany("ActivityPrecedentActivityNavigations")
                        .HasForeignKey("Activity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectActivityPrecedents_ProjectActivities_ProjectActivityId");

                    b.HasOne("ProjectsManagement.Core.Activities.Activity", "PrecedentNavigation")
                        .WithMany("ActivityPrecedentPrecedentNavigations")
                        .HasForeignKey("Precedent")
                        .IsRequired()
                        .HasConstraintName("FK_ProjectActivityPrecedents_ProjectActivities_ActivityPrecedentId");

                    b.Navigation("ActivityNavigation");

                    b.Navigation("PrecedentNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Contributions.ContributionMember", b =>
                {
                    b.HasOne("ProjectsManagement.Core.Contributions.ContributionType", "ContributionTypeNavigation")
                        .WithMany("ContributionMembers")
                        .HasForeignKey("ContributionType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ContributionMembers_ContributionTypes_ContributionTypeId");

                    b.HasOne("ProjectsManagement.Core.Projects.Project", "ProjectNavigation")
                        .WithMany("ContributionMembers")
                        .HasForeignKey("Project")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ContributionMembers_Projects_ProjectId");

                    b.Navigation("ContributionTypeNavigation");

                    b.Navigation("ProjectNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Invitations.Invitation", b =>
                {
                    b.HasOne("ProjectsManagement.Storage.Adapters.Model.InvitationStatus", "InvitationStatusNavigation")
                        .WithMany("Invitations")
                        .HasForeignKey("InvitationStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Invitations_InvitationStatuses_InvitationStatusId");

                    b.HasOne("ProjectsManagement.Core.Projects.Project", "ProjectNavigation")
                        .WithMany("Invitations")
                        .HasForeignKey("Project")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Invitations_Projects_ProjectId");

                    b.Navigation("InvitationStatusNavigation");

                    b.Navigation("ProjectNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.ProjectTasks.ProjectTask", b =>
                {
                    b.HasOne("ProjectsManagement.Core.Projects.Project", "ProjectNavigation")
                        .WithMany("Tasks")
                        .HasForeignKey("Project")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectTasks_Projects_ProjectId");

                    b.HasOne("ProjectsManagement.Core.ProjectTasks.ProjectTaskStatus", "TaskStatusNavigation")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectTasks_ProjectTaskStatuses_ProjectTaskStatusId");

                    b.Navigation("ProjectNavigation");

                    b.Navigation("TaskStatusNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Projects.Project", b =>
                {
                    b.HasOne("ProjectsManagement.Core.Projects.ProjectType", "ProjectTypeNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Projects_ProjectTypes_ProjectTypeId");

                    b.Navigation("ProjectTypeNavigation");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.Activity", b =>
                {
                    b.Navigation("ActivityPrecedentActivityNavigations");

                    b.Navigation("ActivityPrecedentPrecedentNavigations");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityResourceType", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Activities.ActivityType", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Contributions.ContributionType", b =>
                {
                    b.Navigation("ContributionMembers");
                });

            modelBuilder.Entity("ProjectsManagement.Core.ProjectTasks.ProjectTaskStatus", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Projects.Project", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("ContributionMembers");

                    b.Navigation("Invitations");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ProjectsManagement.Core.Projects.ProjectType", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectsManagement.Storage.Adapters.Model.InvitationStatus", b =>
                {
                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}
