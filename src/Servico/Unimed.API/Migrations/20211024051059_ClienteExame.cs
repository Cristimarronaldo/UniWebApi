using Microsoft.EntityFrameworkCore.Migrations;

namespace Unimed.API.Migrations
{
    public partial class ClienteExame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClienteExames_ClienteId",
                table: "ClienteExames");

            migrationBuilder.DropIndex(
                name: "IX_ClienteExames_ExameId",
                table: "ClienteExames");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteExames_ClienteId",
                table: "ClienteExames",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteExames_ExameId",
                table: "ClienteExames",
                column: "ExameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClienteExames_ClienteId",
                table: "ClienteExames");

            migrationBuilder.DropIndex(
                name: "IX_ClienteExames_ExameId",
                table: "ClienteExames");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteExames_ClienteId",
                table: "ClienteExames",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteExames_ExameId",
                table: "ClienteExames",
                column: "ExameId",
                unique: true);
        }
    }
}
