using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScientificLabManagementApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dropsupervisoruniqueindexonlabstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Labs_SupervisiorId",
                table: "Labs");

            migrationBuilder.CreateIndex(
                name: "IX_Labs_SupervisiorId",
                table: "Labs",
                column: "SupervisiorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Labs_SupervisiorId",
                table: "Labs");

            migrationBuilder.CreateIndex(
                name: "IX_Labs_SupervisiorId",
                table: "Labs",
                column: "SupervisiorId",
                unique: true);
        }
    }
}
