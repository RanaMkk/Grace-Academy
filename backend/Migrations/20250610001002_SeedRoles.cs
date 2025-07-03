using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                  table: "Roles",
                  columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                  values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
                  table: "Roles",
                  columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                  values: new object[] { Guid.NewGuid().ToString(), "Student", "Student".ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
                  table: "Roles",
                  columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                  values: new object[] { Guid.NewGuid().ToString(), "Teacher", "Teacher".ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
                 table: "Roles",
                 columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                 values: new object[] { Guid.NewGuid().ToString(), "Assessor", "Assessor".ToUpper(), Guid.NewGuid().ToString() }
           );
            migrationBuilder.InsertData(
                 table: "Roles",
                 columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                 values: new object[] { Guid.NewGuid().ToString(), "Assistant", "Assistant".ToUpper(), Guid.NewGuid().ToString() }
           );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Sales", "Sales".ToUpper(), Guid.NewGuid().ToString() }
          ); 
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Supervisor", "Supervisor".ToUpper(), Guid.NewGuid().ToString() }
          );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Quality", "Quality".ToUpper(), Guid.NewGuid().ToString() }
          );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all seeded roles by their normalized names
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "ADMIN"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "STUDENT"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "TEACHER"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "ASSESSOR"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "ASSISTANT"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "SALES"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "SUPERVISOR"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValue: "QUALITY"
            );
        }
    }
}
