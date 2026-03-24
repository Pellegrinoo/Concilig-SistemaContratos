using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concilig_SistemaContratos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Importacoes_ImportacaoId",
                table: "Contratos");

            migrationBuilder.AlterColumn<int>(
                name: "ImportacaoId",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Importacoes_ImportacaoId",
                table: "Contratos",
                column: "ImportacaoId",
                principalTable: "Importacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Importacoes_ImportacaoId",
                table: "Contratos");

            migrationBuilder.AlterColumn<int>(
                name: "ImportacaoId",
                table: "Contratos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Importacoes_ImportacaoId",
                table: "Contratos",
                column: "ImportacaoId",
                principalTable: "Importacoes",
                principalColumn: "Id");
        }
    }
}
