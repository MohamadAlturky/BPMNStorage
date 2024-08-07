﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectsManagement.Storage.Adapters.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityResourceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContributionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvitationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    ProjectType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectType,
                        principalTable: "ProjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Project = table.Column<int>(type: "integer", nullable: false),
                    ActivityType = table.Column<int>(type: "integer", nullable: false),
                    ActivityResourceType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_ProjectActivityResourceTypes_ProjectActivityResourceTypeId",
                        column: x => x.ActivityResourceType,
                        principalTable: "ActivityResourceType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectActivities_ProjectActivityTypes_ProjectActivityTypeId",
                        column: x => x.ActivityType,
                        principalTable: "ActivityType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Projects_ProjectId",
                        column: x => x.Project,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributionMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Project = table.Column<int>(type: "integer", nullable: false),
                    Contributor = table.Column<int>(type: "integer", nullable: false),
                    ContributionType = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributionMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributionMembers_ContributionTypes_ContributionTypeId",
                        column: x => x.ContributionType,
                        principalTable: "ContributionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributionMembers_Projects_ProjectId",
                        column: x => x.Project,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Contributor = table.Column<int>(type: "integer", nullable: false),
                    InvitationStatus = table.Column<int>(type: "integer", nullable: false),
                    Project = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_InvitationStatuses_InvitationStatusId",
                        column: x => x.InvitationStatus,
                        principalTable: "InvitationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Projects_ProjectId",
                        column: x => x.Project,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Project = table.Column<int>(type: "integer", nullable: false),
                    TaskStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_ProjectTaskStatuses_ProjectTaskStatusId",
                        column: x => x.TaskStatus,
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectId",
                        column: x => x.Project,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityPrecedents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activity = table.Column<int>(type: "integer", nullable: false),
                    Precedent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityPrecedents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivityPrecedents_ProjectActivities_ActivityPrecedentId",
                        column: x => x.Precedent,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectActivityPrecedents_ProjectActivities_ProjectActivityId",
                        column: x => x.Activity,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectActivityResourceTypeId",
                table: "Activity",
                column: "ActivityResourceType");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectActivityTypeId",
                table: "Activity",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectId",
                table: "Activity",
                column: "Project");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityPrecedents_ActivityPrecedentId",
                table: "ActivityPrecedents",
                column: "Precedent");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityPrecedents_ProjectActivityId",
                table: "ActivityPrecedents",
                column: "Activity");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionMembers_ContributionTypeId",
                table: "ContributionMember",
                column: "ContributionType");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionMembers_ProjectId",
                table: "ContributionMember",
                column: "Project");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitationStatusId",
                table: "Invitation",
                column: "InvitationStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ProjectId",
                table: "Invitation",
                column: "Project");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                table: "Project",
                column: "ProjectType");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "Task",
                column: "Project");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectTaskStatusId",
                table: "Task",
                column: "TaskStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityPrecedents");

            migrationBuilder.DropTable(
                name: "ContributionMember");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "ContributionType");

            migrationBuilder.DropTable(
                name: "InvitationStatus");

            migrationBuilder.DropTable(
                name: "TaskStatus");

            migrationBuilder.DropTable(
                name: "ActivityResourceType");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ProjectType");
        }
    }
}
