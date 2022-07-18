using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace products.Domain.Infra.Migrations
{
    public partial class OrdersContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Itens",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Itens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_OrderId",
                table: "Itens",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Orders_OrderId",
                table: "Itens",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Orders_OrderId",
                table: "Itens");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Itens_OrderId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Itens");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Itens",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
