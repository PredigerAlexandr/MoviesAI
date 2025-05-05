using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProducerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Movies",
                type: "text",
                nullable: true);
        }
    }
}
