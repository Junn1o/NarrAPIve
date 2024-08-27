using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseMigrationv53 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_user_post_user_id",
                table: "post");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "user_birthday",
                table: "user",
                newName: "user_birthdate");

            migrationBuilder.RenameColumn(
                name: "post_user_id",
                table: "post",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_post_user_id",
                table: "post",
                newName: "IX_post_user_id");

            migrationBuilder.AddColumn<bool>(
                name: "user_gender",
                table: "user",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_user_id",
                table: "post",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_user_user_id",
                table: "post");

            migrationBuilder.DropColumn(
                name: "user_gender",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "user_birthdate",
                table: "user",
                newName: "user_birthday");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "post",
                newName: "post_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_user_id",
                table: "post",
                newName: "IX_post_post_user_id");

            migrationBuilder.AddColumn<bool>(
                name: "gender",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_post_user_id",
                table: "post",
                column: "post_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
