using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BACKENDD.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentAndContactType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactTypeId",
                table: "Contacts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Contacts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactTypes_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Departments_DepartmentId",
                table: "Contacts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactTypes_ContactTypeId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Departments_DepartmentId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactTypeId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Contacts");
        }
    }
}
