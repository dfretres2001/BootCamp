using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SolicitudActualizadoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Currencies_CurrencyId1",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Customers_CustomerId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CurrencyId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CustomerId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CurrencyId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId1",
                table: "Requests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Requests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CurrencyId1",
                table: "Requests",
                column: "CurrencyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId1",
                table: "Requests",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Currencies_CurrencyId1",
                table: "Requests",
                column: "CurrencyId1",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Customers_CustomerId1",
                table: "Requests",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
