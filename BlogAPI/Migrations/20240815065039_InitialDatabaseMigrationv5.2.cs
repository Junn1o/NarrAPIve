using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseMigrationv52 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_user_post_id",
                table: "post");

            migrationBuilder.AddColumn<Guid>(
                name: "post_user_id",
                table: "post",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_post_post_user_id",
                table: "post",
                column: "post_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_post_user_id",
                table: "post",
                column: "post_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_user_post_user_id",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_post_post_user_id",
                table: "post");

            migrationBuilder.DropColumn(
                name: "post_user_id",
                table: "post");

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_post_id",
                table: "post",
                column: "post_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
