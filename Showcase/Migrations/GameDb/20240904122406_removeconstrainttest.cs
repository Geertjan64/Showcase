using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showcase.Migrations.GameDb
{
    /// <inheritdoc />
    public partial class removeconstrainttest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GameResults_GameId",
                table: "GameResults",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameResults_GameId",
                table: "GameResults");
        }
    }
}
