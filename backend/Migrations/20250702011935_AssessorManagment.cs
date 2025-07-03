using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AssessorManagment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmploymentType = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    College = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Specializations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguagesSpoken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsNative = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAssessments = table.Column<int>(type: "int", nullable: false),
                    PendingAssessments = table.Column<int>(type: "int", nullable: false),
                    weekAssessments = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessors_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessorSlot",
                columns: table => new
                {
                    AssessorsId = table.Column<int>(type: "int", nullable: false),
                    AvailableSlotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessorSlot", x => new { x.AssessorsId, x.AvailableSlotsId });
                    table.ForeignKey(
                        name: "FK_AssessorSlot_Assessors_AssessorsId",
                        column: x => x.AssessorsId,
                        principalTable: "Assessors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessorSlot_Slots_AvailableSlotsId",
                        column: x => x.AvailableSlotsId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessors_UserId",
                table: "Assessors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessorSlot_AvailableSlotsId",
                table: "AssessorSlot",
                column: "AvailableSlotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessorSlot");

            migrationBuilder.DropTable(
                name: "Assessors");
        }
    }
}
