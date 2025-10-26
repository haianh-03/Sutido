using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sutido.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseAfterChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_User_PointId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Point",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Point_UserId",
                table: "Point",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "Point",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "Point");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Point_UserId",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Point");

            migrationBuilder.AddColumn<long>(
                name: "PointId",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PointId",
                table: "User",
                column: "PointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "User",
                column: "PointId",
                principalTable: "Point",
                principalColumn: "PointId");
        }
    }
}
