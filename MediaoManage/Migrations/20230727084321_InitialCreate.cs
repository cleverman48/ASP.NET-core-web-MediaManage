using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaoManage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    container_id = table.Column<int>(type: "int", nullable: true),
                    media_file_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_file_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_uploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    media_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    media_status = table.Column<int>(type: "int", nullable: false),
                    media_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    media_deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMedia");
        }
    }
}
