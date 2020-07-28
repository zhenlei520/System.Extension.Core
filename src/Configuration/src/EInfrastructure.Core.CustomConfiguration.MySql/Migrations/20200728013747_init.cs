using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EInfrastructure.Core.CustomConfiguration.MySql.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app.namespaces",
                columns: table => new
                {
                    id = table.Column<long>(maxLength: 36, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    appid = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    format = table.Column<string>(maxLength: 50, nullable: false),
                    remark = table.Column<string>(maxLength: 200, nullable: true),
                    add_time = table.Column<DateTime>(nullable: false),
                    edit_time = table.Column<DateTime>(nullable: false),
                    is_del = table.Column<bool>(nullable: false),
                    del_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app.namespaces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "apps",
                columns: table => new
                {
                    id = table.Column<long>(maxLength: 36, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    appid = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    edit_time = table.Column<DateTime>(nullable: false),
                    is_del = table.Column<bool>(nullable: false),
                    del_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "namespace.items",
                columns: table => new
                {
                    id = table.Column<long>(maxLength: 36, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    environment_name = table.Column<string>(maxLength: 50, nullable: false),
                    key = table.Column<string>(maxLength: 50, nullable: false),
                    value = table.Column<string>(maxLength: 50, nullable: false),
                    remark = table.Column<string>(maxLength: 200, nullable: true),
                    app_namespace_id = table.Column<long>(maxLength: 36, nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    edit_time = table.Column<DateTime>(nullable: false),
                    is_del = table.Column<bool>(nullable: false),
                    del_time = table.Column<DateTime>(nullable: true),
                    AppNamespacesId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_namespace.items", x => x.id);
                    table.ForeignKey(
                        name: "FK_namespace.items_app.namespaces_app_namespace_id",
                        column: x => x.app_namespace_id,
                        principalTable: "app.namespaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_namespace.items_app.namespaces_AppNamespacesId1",
                        column: x => x.AppNamespacesId1,
                        principalTable: "app.namespaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_namespace.items_app_namespace_id",
                table: "namespace.items",
                column: "app_namespace_id");

            migrationBuilder.CreateIndex(
                name: "IX_namespace.items_AppNamespacesId1",
                table: "namespace.items",
                column: "AppNamespacesId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apps");

            migrationBuilder.DropTable(
                name: "namespace.items");

            migrationBuilder.DropTable(
                name: "app.namespaces");
        }
    }
}
