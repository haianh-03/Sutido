using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sutido.Model.Migrations
{
    /// <inheritdoc />
    public partial class Update_Point : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "Point");

            migrationBuilder.DropIndex(
                name: "IX_Point_UserId",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Point");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Point",
                newName: "TotalPoint");

            migrationBuilder.AddColumn<long>(
                name: "PointId",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PointId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "TotalPoint",
                table: "Point",
                newName: "Points");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Point",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Point",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Point_UserId",
                table: "Point",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Point__UserId__07C12930",
                table: "Point",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
