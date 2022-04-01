using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagerPersistance.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0713bce4-701f-4dbd-89e1-c0620b950c63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ba7489f-c7df-4fb8-8823-f6ac000c43b8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cd75f4b-85ee-47c8-a449-554dbf797c4e", "22bf33ee-f8bf-4800-a712-1420ed0fd20e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74aafe0e-d369-4e43-82bb-d07e1dfb6ef7", "3dd38431-cfa6-4483-9ca2-5ae6b70879d2", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74aafe0e-d369-4e43-82bb-d07e1dfb6ef7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cd75f4b-85ee-47c8-a449-554dbf797c4e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ba7489f-c7df-4fb8-8823-f6ac000c43b8", "a47e5998-a667-4409-9df3-a618566d75f7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0713bce4-701f-4dbd-89e1-c0620b950c63", "cbf479cb-bb78-460a-83fe-0a2946b46b02", "Administrator", "ADMINISTRATOR" });
        }
    }
}
