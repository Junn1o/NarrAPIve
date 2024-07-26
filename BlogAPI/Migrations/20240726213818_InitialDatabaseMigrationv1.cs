using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseMigrationv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "post_id",
                table: "Chapters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "post_id",
                table: "Chapters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
