using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Write.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lock_iqs_IqId",
                table: "Lock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lock",
                table: "Lock");

            migrationBuilder.RenameTable(
                name: "Lock",
                newName: "locks");

            migrationBuilder.RenameColumn(
                name: "AccessLevel",
                table: "locks",
                newName: "access_level");

            migrationBuilder.RenameIndex(
                name: "IX_Lock_IqId",
                table: "locks",
                newName: "IX_locks_IqId");

            migrationBuilder.AlterColumn<string>(
                name: "access_level",
                table: "locks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "lock_pkey",
                table: "locks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_locks_iqs_IqId",
                table: "locks",
                column: "IqId",
                principalTable: "iqs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locks_iqs_IqId",
                table: "locks");

            migrationBuilder.DropPrimaryKey(
                name: "lock_pkey",
                table: "locks");

            migrationBuilder.RenameTable(
                name: "locks",
                newName: "Lock");

            migrationBuilder.RenameColumn(
                name: "access_level",
                table: "Lock",
                newName: "AccessLevel");

            migrationBuilder.RenameIndex(
                name: "IX_locks_IqId",
                table: "Lock",
                newName: "IX_Lock_IqId");

            migrationBuilder.AlterColumn<int>(
                name: "AccessLevel",
                table: "Lock",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lock",
                table: "Lock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lock_iqs_IqId",
                table: "Lock",
                column: "IqId",
                principalTable: "iqs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
