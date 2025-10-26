using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sutido.Model.Migrations
{
    /// <inheritdoc />
    public partial class FixEnumDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__TutorProf__UserI__534D60F1",
                table: "TutorProfile");

            migrationBuilder.DropForeignKey(
                name: "FK__User__RoleId__3B75D760",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_TutorProfile_UserId",
                table: "TutorProfile");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "TutorProfile");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "TutorProfile",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "ApprovedAt",
                table: "TutorProfile",
                newName: "ReviewedAt");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "User",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Education",
                table: "TutorProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TutorProfile",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReviewerBy",
                table: "TutorProfile",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "TutorProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    CertificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorProfileId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubmittedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReviewedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.CertificationId);
                    table.ForeignKey(
                        name: "FK_Certification_ReviewedBy_User",
                        column: x => x.ReviewedBy,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Certification_TutorProfile",
                        column: x => x.TutorProfileId,
                        principalTable: "TutorProfile",
                        principalColumn: "TutorProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorProfile_ReviewerBy",
                table: "TutorProfile",
                column: "ReviewerBy");

            migrationBuilder.CreateIndex(
                name: "IX_TutorProfile_UserId",
                table: "TutorProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certification_ReviewedBy",
                table: "Certification",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_TutorProfileId",
                table: "Certification",
                column: "TutorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorProfile_ReviewedBy_User",
                table: "TutorProfile",
                column: "ReviewerBy",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorProfile_User",
                table: "TutorProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorProfile_ReviewedBy_User",
                table: "TutorProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorProfile_User",
                table: "TutorProfile");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropIndex(
                name: "IX_TutorProfile_ReviewerBy",
                table: "TutorProfile");

            migrationBuilder.DropIndex(
                name: "IX_TutorProfile_UserId",
                table: "TutorProfile");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TutorProfile");

            migrationBuilder.DropColumn(
                name: "ReviewerBy",
                table: "TutorProfile");

            migrationBuilder.DropColumn(
                name: "status",
                table: "TutorProfile");

            migrationBuilder.RenameColumn(
                name: "ReviewedAt",
                table: "TutorProfile",
                newName: "ApprovedAt");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "TutorProfile",
                newName: "Bio");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Education",
                table: "TutorProfile",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "TutorProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE1AD1258896", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewedBy = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    SubmittedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Verifica__306D490753D10F80", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK__Verificat__Revie__4F7CD00D",
                        column: x => x.ReviewedBy,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Verificat__UserI__4D94879B",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorProfile_UserId",
                table: "TutorProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ReviewedBy",
                table: "Verification",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_UserId",
                table: "Verification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__TutorProf__UserI__534D60F1",
                table: "TutorProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__User__RoleId__3B75D760",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }
    }
}
