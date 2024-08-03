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
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "credential",
                columns: table => new
                {
                    cred_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cred_roleid = table.Column<int>(type: "int", nullable: false),
                    cred_userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cred_passWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cred_createDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credential", x => x.cred_id);
                    table.ForeignKey(
                        name: "FK_credential_role_cred_roleid",
                        column: x => x.cred_roleid,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_credential_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_status = table.Column<bool>(type: "bit", nullable: false),
                    post_createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    post_hidden = table.Column<bool>(type: "bit", nullable: false),
                    post_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_post_user_post_id",
                        column: x => x.post_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_category_temp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_category_temp", x => x.id);
                    table.ForeignKey(
                        name: "FK_post_category_temp_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_category_temp_post_post_id",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "volume",
                columns: table => new
                {
                    volume_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    volume_createDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volume", x => x.volume_id);
                    table.ForeignKey(
                        name: "FK_volume_post_post_id",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapter",
                columns: table => new
                {
                    chapter_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    chapter_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chapter_content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapter", x => x.chapter_id);
                    table.ForeignKey(
                        name: "FK_chapter_volume_volume_id",
                        column: x => x.volume_id,
                        principalTable: "volume",
                        principalColumn: "volume_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chapter_volume_id",
                table: "chapter",
                column: "volume_id");

            migrationBuilder.CreateIndex(
                name: "IX_credential_cred_roleid",
                table: "credential",
                column: "cred_roleid");

            migrationBuilder.CreateIndex(
                name: "IX_credential_user_id",
                table: "credential",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_category_temp_category_id",
                table: "post_category_temp",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_category_temp_post_id",
                table: "post_category_temp",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_volume_post_id",
                table: "volume",
                column: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chapter");

            migrationBuilder.DropTable(
                name: "credential");

            migrationBuilder.DropTable(
                name: "post_category_temp");

            migrationBuilder.DropTable(
                name: "volume");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
