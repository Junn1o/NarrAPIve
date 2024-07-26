using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cred_role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cred_userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cred_passWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cred_createDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_birthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Credentials_user_id",
                        column: x => x.user_id,
                        principalTable: "Credentials",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_shortDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_longDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_status = table.Column<bool>(type: "bit", nullable: false),
                    post_createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    post_hidden = table.Column<bool>(type: "bit", nullable: false),
                    post_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_post_id",
                        column: x => x.post_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.id);
                    table.ForeignKey(
                        name: "FK_PostCategories_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategories_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    volume_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.volume_id);
                    table.ForeignKey(
                        name: "FK_Volumes_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    chapter_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    chapter_content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.chapter_id);
                    table.ForeignKey(
                        name: "FK_Chapters_Volumes_volume_id",
                        column: x => x.volume_id,
                        principalTable: "Volumes",
                        principalColumn: "volume_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_volume_id",
                table: "Chapters",
                column: "volume_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_category_id",
                table: "PostCategories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_post_id",
                table: "PostCategories",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_post_id",
                table: "Volumes",
                column: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Credentials");
        }
    }
}
