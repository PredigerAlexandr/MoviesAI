using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteMoviesFieldAddDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FavoriteMovieIds",
                table: "Users",
                type: "jsonb",
                nullable: true,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FavoriteMovieIds",
                table: "Users",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true,
                oldDefaultValue: "[]");
        }
    }
}
