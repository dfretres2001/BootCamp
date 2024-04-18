using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Movement2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Accounts_AccountId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_AccountId",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Movements",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Movements",
                newName: "OriginalAccountId");

            migrationBuilder.AddColumn<int>(
                name: "DestinationAccountId",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementType",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "OriginalAccountId",
                table: "Movements",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Movements",
                newName: "Destination");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_AccountId",
                table: "Movements",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Accounts_AccountId",
                table: "Movements",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
