using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concilig_SistemaContratos.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Importacoes_Usuarios_usuarioId",
                table: "Importacoes");

            migrationBuilder.DropIndex(
                name: "IX_Importacoes_usuarioId",
                table: "Importacoes");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "Importacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Importacoes_IdUsuario",
                table: "Importacoes",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Importacoes_Usuarios_IdUsuario",
                table: "Importacoes",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Importacoes_Usuarios_IdUsuario",
                table: "Importacoes");

            migrationBuilder.DropIndex(
                name: "IX_Importacoes_IdUsuario",
                table: "Importacoes");

            migrationBuilder.AddColumn<int>(
                name: "usuarioId",
                table: "Importacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Importacoes_usuarioId",
                table: "Importacoes",
                column: "usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Importacoes_Usuarios_usuarioId",
                table: "Importacoes",
                column: "usuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
