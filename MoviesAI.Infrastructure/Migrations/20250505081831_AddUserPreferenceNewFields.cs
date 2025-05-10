using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPreferenceNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<Guid>>(
                name: "RatedFilmIds",
                table: "UserPreferences",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserPreferenceEntityId",
                table: "Movies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserPreferenceEntityId",
                table: "Movies",
                column: "UserPreferenceEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_UserPreferences_UserPreferenceEntityId",
                table: "Movies",
                column: "UserPreferenceEntityId",
                principalTable: "UserPreferences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_UserPreferences_UserPreferenceEntityId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserPreferenceEntityId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RatedFilmIds",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "UserPreferenceEntityId",
                table: "Movies");
        }
    }
}
