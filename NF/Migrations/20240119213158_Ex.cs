using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NF.Migrations
{
    /// <inheritdoc />
    public partial class Ex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_NotasFiscais_NotaFiscalId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_NotaFiscalId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "NotaFiscalId",
                table: "Produtos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotaFiscalId",
                table: "Produtos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_NotaFiscalId",
                table: "Produtos",
                column: "NotaFiscalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_NotasFiscais_NotaFiscalId",
                table: "Produtos",
                column: "NotaFiscalId",
                principalTable: "NotasFiscais",
                principalColumn: "Id");
        }
    }
}
