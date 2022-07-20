using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace products.Domain.Infra.Migrations
{
    public partial class ContextCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_EnderecoEntrega_EnderecoEntregaId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_EnderecoEntregaId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EnderecoEntregaId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "EnderecoEntrega",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoEntrega_CustomerId",
                table: "EnderecoEntrega",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoEntrega_Customers_CustomerId",
                table: "EnderecoEntrega",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoEntrega_Customers_CustomerId",
                table: "EnderecoEntrega");

            migrationBuilder.DropIndex(
                name: "IX_EnderecoEntrega_CustomerId",
                table: "EnderecoEntrega");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "EnderecoEntrega");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoEntregaId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EnderecoEntregaId",
                table: "Customers",
                column: "EnderecoEntregaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_EnderecoEntrega_EnderecoEntregaId",
                table: "Customers",
                column: "EnderecoEntregaId",
                principalTable: "EnderecoEntrega",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
