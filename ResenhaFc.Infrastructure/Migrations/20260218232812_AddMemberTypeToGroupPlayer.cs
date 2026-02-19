using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResenhaFc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberTypeToGroupPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberType",
                table: "GroupPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GroupPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberType",
                table: "GroupPlayers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GroupPlayers");
        }
    }
}
