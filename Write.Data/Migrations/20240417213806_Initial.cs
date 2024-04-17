using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Write.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "iqs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    building_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("iq_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    access_level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    IqId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lock_iqs_IqId",
                        column: x => x.IqId,
                        principalTable: "iqs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IqUser",
                columns: table => new
                {
                    IqAssignedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IqUser", x => new { x.IqAssignedId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_IqUser_iqs_IqAssignedId",
                        column: x => x.IqAssignedId,
                        principalTable: "iqs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IqUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IqUser_UsersId",
                table: "IqUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Lock_IqId",
                table: "Lock",
                column: "IqId");

            migrationBuilder.CreateIndex(
                name: "IX_iqs_building_name",
                table: "iqs",
                column: "building_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IqUser");

            migrationBuilder.DropTable(
                name: "Lock");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "iqs");
        }
    }
}
