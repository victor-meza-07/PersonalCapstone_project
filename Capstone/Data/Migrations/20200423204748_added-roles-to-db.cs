using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Data.Migrations
{
    public partial class addedrolestodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d185a43-ebe7-4f6d-90b3-2025739eff15");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "743c177d-df49-4527-b753-d491602216ec", "6dd495c2-09b5-4a2f-b877-8509ac5e567f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "495d22b0-9581-4a20-a4fa-e6b95f747bed", "436f49b3-2507-40f7-8082-7035f8a90d1a", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c49ee8e1-d8ed-48dc-b913-9b739b5dd4c1", "c4ae4c02-5fd4-49d0-839b-19224e891483", "Analyst", "ANALYST" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "495d22b0-9581-4a20-a4fa-e6b95f747bed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743c177d-df49-4527-b753-d491602216ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49ee8e1-d8ed-48dc-b913-9b739b5dd4c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d185a43-ebe7-4f6d-90b3-2025739eff15", "33e92eb6-ec93-45db-bc21-a3a2cb11df83", "Admin", "ADMIN" });
        }
    }
}
