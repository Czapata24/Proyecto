using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HankoSpa.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Citas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
