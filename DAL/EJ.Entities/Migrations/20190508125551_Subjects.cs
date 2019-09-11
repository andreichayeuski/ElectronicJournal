using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EJ.Entities.Migrations
{
    public partial class Subjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorium",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSpending",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSpending", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(nullable: true),
                    NumberOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupShedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupShedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupShedule_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupShedule_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SheduleSubject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupSheduleId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheduleSubject_GroupShedule_GroupSheduleId",
                        column: x => x.GroupSheduleId,
                        principalTable: "GroupShedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SheduleTimeSpending",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SheduleSubjectId = table.Column<int>(nullable: false),
                    WeekDayId = table.Column<int>(nullable: false),
                    TimeSpendingId = table.Column<int>(nullable: false),
                    AuditoriumId = table.Column<int>(nullable: false),
                    ClassTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleTimeSpending", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheduleTimeSpending_Auditorium_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "Auditorium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleTimeSpending_ClassType_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "ClassType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleTimeSpending_SheduleSubject_SheduleSubjectId",
                        column: x => x.SheduleSubjectId,
                        principalTable: "SheduleSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleTimeSpending_TimeSpending_TimeSpendingId",
                        column: x => x.TimeSpendingId,
                        principalTable: "TimeSpending",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleTimeSpending_WeekDay_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupShedule_GroupId",
                table: "GroupShedule",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupShedule_SemesterId",
                table: "GroupShedule",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleSubject_GroupSheduleId",
                table: "SheduleSubject",
                column: "GroupSheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleSubject_SubjectId",
                table: "SheduleSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleTimeSpending_AuditoriumId",
                table: "SheduleTimeSpending",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleTimeSpending_ClassTypeId",
                table: "SheduleTimeSpending",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleTimeSpending_SheduleSubjectId",
                table: "SheduleTimeSpending",
                column: "SheduleSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleTimeSpending_TimeSpendingId",
                table: "SheduleTimeSpending",
                column: "TimeSpendingId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleTimeSpending_WeekDayId",
                table: "SheduleTimeSpending",
                column: "WeekDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheduleTimeSpending");

            migrationBuilder.DropTable(
                name: "Auditorium");

            migrationBuilder.DropTable(
                name: "ClassType");

            migrationBuilder.DropTable(
                name: "SheduleSubject");

            migrationBuilder.DropTable(
                name: "TimeSpending");

            migrationBuilder.DropTable(
                name: "WeekDay");

            migrationBuilder.DropTable(
                name: "GroupShedule");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Semester");
        }
    }
}
