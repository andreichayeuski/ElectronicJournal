using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EJ.Entities.Migrations
{
    public partial class Absence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsenceNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbsenceId = table.Column<int>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarSheduleTimeSpending",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarId = table.Column<int>(nullable: false),
                    SheduleTimeSpendingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarSheduleTimeSpending", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarSheduleTimeSpending_Calendar_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarSheduleTimeSpending_SheduleTimeSpendings_SheduleTimeSpendingId",
                        column: x => x.SheduleTimeSpendingId,
                        principalTable: "SheduleTimeSpendings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarSheduleTimeSpendingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsRespectfulReason = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AbsenceNotificationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absence_AbsenceNotification_AbsenceNotificationId",
                        column: x => x.AbsenceNotificationId,
                        principalTable: "AbsenceNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Absence_CalendarSheduleTimeSpending_CalendarSheduleTimeSpendingId",
                        column: x => x.CalendarSheduleTimeSpendingId,
                        principalTable: "CalendarSheduleTimeSpending",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absence_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absence_AbsenceNotificationId",
                table: "Absence",
                column: "AbsenceNotificationId",
                unique: true,
                filter: "[AbsenceNotificationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Absence_CalendarSheduleTimeSpendingId",
                table: "Absence",
                column: "CalendarSheduleTimeSpendingId");

            migrationBuilder.CreateIndex(
                name: "IX_Absence_UserId",
                table: "Absence",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarSheduleTimeSpending_CalendarId",
                table: "CalendarSheduleTimeSpending",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarSheduleTimeSpending_SheduleTimeSpendingId",
                table: "CalendarSheduleTimeSpending",
                column: "SheduleTimeSpendingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence");

            migrationBuilder.DropTable(
                name: "AbsenceNotification");

            migrationBuilder.DropTable(
                name: "CalendarSheduleTimeSpending");

            migrationBuilder.DropTable(
                name: "Calendar");
        }
    }
}
