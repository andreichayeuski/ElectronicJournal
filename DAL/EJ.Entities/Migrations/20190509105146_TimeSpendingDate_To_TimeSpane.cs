using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EJ.Entities.Migrations
{
    public partial class TimeSpendingDate_To_TimeSpane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupShedule_Groups_GroupId",
                table: "GroupShedule");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupShedule_Semester_SemesterId",
                table: "GroupShedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleSubject_GroupShedule_GroupSheduleId",
                table: "SheduleSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleSubject_Subject_SubjectId",
                table: "SheduleSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpending_Auditorium_AuditoriumId",
                table: "SheduleTimeSpending");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpending_ClassType_ClassTypeId",
                table: "SheduleTimeSpending");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpending_SheduleSubject_SheduleSubjectId",
                table: "SheduleTimeSpending");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpending_TimeSpending_TimeSpendingId",
                table: "SheduleTimeSpending");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpending_WeekDay_WeekDayId",
                table: "SheduleTimeSpending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSpending",
                table: "TimeSpending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SheduleTimeSpending",
                table: "SheduleTimeSpending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SheduleSubject",
                table: "SheduleSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupShedule",
                table: "GroupShedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassType",
                table: "ClassType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditorium",
                table: "Auditorium");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TimeSpending");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TimeSpending");

            migrationBuilder.RenameTable(
                name: "WeekDay",
                newName: "WeekDays");

            migrationBuilder.RenameTable(
                name: "TimeSpending",
                newName: "TimeSpendings");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "SheduleTimeSpending",
                newName: "SheduleTimeSpendings");

            migrationBuilder.RenameTable(
                name: "SheduleSubject",
                newName: "SheduleSubjects");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "Semesters");

            migrationBuilder.RenameTable(
                name: "GroupShedule",
                newName: "GroupShedules");

            migrationBuilder.RenameTable(
                name: "ClassType",
                newName: "ClassTypes");

            migrationBuilder.RenameTable(
                name: "Auditorium",
                newName: "Auditoriums");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpending_WeekDayId",
                table: "SheduleTimeSpendings",
                newName: "IX_SheduleTimeSpendings_WeekDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpending_TimeSpendingId",
                table: "SheduleTimeSpendings",
                newName: "IX_SheduleTimeSpendings_TimeSpendingId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpending_SheduleSubjectId",
                table: "SheduleTimeSpendings",
                newName: "IX_SheduleTimeSpendings_SheduleSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpending_ClassTypeId",
                table: "SheduleTimeSpendings",
                newName: "IX_SheduleTimeSpendings_ClassTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpending_AuditoriumId",
                table: "SheduleTimeSpendings",
                newName: "IX_SheduleTimeSpendings_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleSubject_SubjectId",
                table: "SheduleSubjects",
                newName: "IX_SheduleSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleSubject_GroupSheduleId",
                table: "SheduleSubjects",
                newName: "IX_SheduleSubjects_GroupSheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupShedule_SemesterId",
                table: "GroupShedules",
                newName: "IX_GroupShedules_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupShedule_GroupId",
                table: "GroupShedules",
                newName: "IX_GroupShedules_GroupId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TimeSpendings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TimeSpendings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Semesters",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Semesters",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeekDays",
                table: "WeekDays",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSpendings",
                table: "TimeSpendings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheduleTimeSpendings",
                table: "SheduleTimeSpendings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheduleSubjects",
                table: "SheduleSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupShedules",
                table: "GroupShedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassTypes",
                table: "ClassTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditoriums",
                table: "Auditoriums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupShedules_Groups_GroupId",
                table: "GroupShedules",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupShedules_Semesters_SemesterId",
                table: "GroupShedules",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleSubjects_GroupShedules_GroupSheduleId",
                table: "SheduleSubjects",
                column: "GroupSheduleId",
                principalTable: "GroupShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleSubjects_Subjects_SubjectId",
                table: "SheduleSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpendings_Auditoriums_AuditoriumId",
                table: "SheduleTimeSpendings",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpendings_ClassTypes_ClassTypeId",
                table: "SheduleTimeSpendings",
                column: "ClassTypeId",
                principalTable: "ClassTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpendings_SheduleSubjects_SheduleSubjectId",
                table: "SheduleTimeSpendings",
                column: "SheduleSubjectId",
                principalTable: "SheduleSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpendings_TimeSpendings_TimeSpendingId",
                table: "SheduleTimeSpendings",
                column: "TimeSpendingId",
                principalTable: "TimeSpendings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpendings_WeekDays_WeekDayId",
                table: "SheduleTimeSpendings",
                column: "WeekDayId",
                principalTable: "WeekDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupShedules_Groups_GroupId",
                table: "GroupShedules");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupShedules_Semesters_SemesterId",
                table: "GroupShedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleSubjects_GroupShedules_GroupSheduleId",
                table: "SheduleSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleSubjects_Subjects_SubjectId",
                table: "SheduleSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpendings_Auditoriums_AuditoriumId",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpendings_ClassTypes_ClassTypeId",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpendings_SheduleSubjects_SheduleSubjectId",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpendings_TimeSpendings_TimeSpendingId",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropForeignKey(
                name: "FK_SheduleTimeSpendings_WeekDays_WeekDayId",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekDays",
                table: "WeekDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSpendings",
                table: "TimeSpendings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SheduleTimeSpendings",
                table: "SheduleTimeSpendings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SheduleSubjects",
                table: "SheduleSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupShedules",
                table: "GroupShedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassTypes",
                table: "ClassTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditoriums",
                table: "Auditoriums");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimeSpendings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TimeSpendings");

            migrationBuilder.RenameTable(
                name: "WeekDays",
                newName: "WeekDay");

            migrationBuilder.RenameTable(
                name: "TimeSpendings",
                newName: "TimeSpending");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "SheduleTimeSpendings",
                newName: "SheduleTimeSpending");

            migrationBuilder.RenameTable(
                name: "SheduleSubjects",
                newName: "SheduleSubject");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "Semester");

            migrationBuilder.RenameTable(
                name: "GroupShedules",
                newName: "GroupShedule");

            migrationBuilder.RenameTable(
                name: "ClassTypes",
                newName: "ClassType");

            migrationBuilder.RenameTable(
                name: "Auditoriums",
                newName: "Auditorium");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpendings_WeekDayId",
                table: "SheduleTimeSpending",
                newName: "IX_SheduleTimeSpending_WeekDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpendings_TimeSpendingId",
                table: "SheduleTimeSpending",
                newName: "IX_SheduleTimeSpending_TimeSpendingId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpendings_SheduleSubjectId",
                table: "SheduleTimeSpending",
                newName: "IX_SheduleTimeSpending_SheduleSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpendings_ClassTypeId",
                table: "SheduleTimeSpending",
                newName: "IX_SheduleTimeSpending_ClassTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleTimeSpendings_AuditoriumId",
                table: "SheduleTimeSpending",
                newName: "IX_SheduleTimeSpending_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleSubjects_SubjectId",
                table: "SheduleSubject",
                newName: "IX_SheduleSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SheduleSubjects_GroupSheduleId",
                table: "SheduleSubject",
                newName: "IX_SheduleSubject_GroupSheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupShedules_SemesterId",
                table: "GroupShedule",
                newName: "IX_GroupShedule_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupShedules_GroupId",
                table: "GroupShedule",
                newName: "IX_GroupShedule_GroupId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TimeSpending",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TimeSpending",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Semester",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Semester",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDay",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSpending",
                table: "TimeSpending",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheduleTimeSpending",
                table: "SheduleTimeSpending",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheduleSubject",
                table: "SheduleSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupShedule",
                table: "GroupShedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassType",
                table: "ClassType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditorium",
                table: "Auditorium",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupShedule_Groups_GroupId",
                table: "GroupShedule",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupShedule_Semester_SemesterId",
                table: "GroupShedule",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleSubject_GroupShedule_GroupSheduleId",
                table: "SheduleSubject",
                column: "GroupSheduleId",
                principalTable: "GroupShedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleSubject_Subject_SubjectId",
                table: "SheduleSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpending_Auditorium_AuditoriumId",
                table: "SheduleTimeSpending",
                column: "AuditoriumId",
                principalTable: "Auditorium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpending_ClassType_ClassTypeId",
                table: "SheduleTimeSpending",
                column: "ClassTypeId",
                principalTable: "ClassType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpending_SheduleSubject_SheduleSubjectId",
                table: "SheduleTimeSpending",
                column: "SheduleSubjectId",
                principalTable: "SheduleSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpending_TimeSpending_TimeSpendingId",
                table: "SheduleTimeSpending",
                column: "TimeSpendingId",
                principalTable: "TimeSpending",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheduleTimeSpending_WeekDay_WeekDayId",
                table: "SheduleTimeSpending",
                column: "WeekDayId",
                principalTable: "WeekDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
