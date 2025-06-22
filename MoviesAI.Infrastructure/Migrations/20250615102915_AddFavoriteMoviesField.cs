using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteMoviesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteMovieIds",
                table: "Users",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteMovieIds",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
