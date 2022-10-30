using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCorePersonal.Repository.Migrations
{
    public partial class updateColumnBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MofifiedDate",
                table: "User",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "MofifiedDate",
                table: "Projects",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "MofifiedDate",
                table: "Experiences",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "MofifiedDate",
                table: "Contacts",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "MofifiedDate",
                table: "Categories",
                newName: "ModifiedDate");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "About", "CreatedDate", "Email", "FullName", "ModifiedDate", "Password", "Phone", "ProfileImage", "RePassword", "Username", "WebSite" },
                values: new object[] { 1, "bilgisayar mühendisi", new DateTime(2022, 10, 24, 20, 23, 18, 759, DateTimeKind.Local).AddTicks(5878), "baranvelat021@gmail.com", "Welat BARAN", null, "123123", "05393711268", "no-image.jpg", "123123", "wbaran", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "User",
                newName: "MofifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Projects",
                newName: "MofifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Experiences",
                newName: "MofifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Contacts",
                newName: "MofifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Categories",
                newName: "MofifiedDate");
        }
    }
}
