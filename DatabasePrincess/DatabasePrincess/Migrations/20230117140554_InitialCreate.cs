using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabasePrincess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.AttemptId);
                });

            migrationBuilder.CreateTable(
                name: "ContenderDto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    rating = table.Column<int>(type: "INTEGER", nullable: false),
                    AttemptId = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    AttemptDtoAttemptId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenderDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContenderDto_Attempts_AttemptDtoAttemptId",
                        column: x => x.AttemptDtoAttemptId,
                        principalTable: "Attempts",
                        principalColumn: "AttemptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContenderDto_AttemptDtoAttemptId",
                table: "ContenderDto",
                column: "AttemptDtoAttemptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContenderDto");

            migrationBuilder.DropTable(
                name: "Attempts");
        }
    }
}
