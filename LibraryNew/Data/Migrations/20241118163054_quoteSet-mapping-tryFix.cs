using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryNew.Data.Migrations
{
    /// <inheritdoc />
    public partial class quoteSetmappingtryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Quotes");
        }
    }
}
