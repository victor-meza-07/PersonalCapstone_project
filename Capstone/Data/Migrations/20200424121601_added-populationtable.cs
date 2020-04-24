using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Data.Migrations
{
    public partial class addedpopulationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49e94125-14d9-4d7c-9d5a-45efa07d42d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc8772c0-cfbf-4412-b466-9426cb9d3bd2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f62ab906-b80f-499e-a431-aea9c88e3225");

            migrationBuilder.CreateTable(
                name: "PopulationLocation",
                columns: table => new
                {
                    PK = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeRange = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false),
                    Percent = table.Column<string>(nullable: true),
                    MaleTotal = table.Column<int>(nullable: false),
                    MalePercent = table.Column<string>(nullable: true),
                    FemaleTotal = table.Column<string>(nullable: true),
                    PercentFemale = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulationLocation", x => x.PK);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57400c69-bfcc-432c-8379-e1c031c6eb7b", "5f769684-5476-4d9a-ba00-3797bfc040df", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7121cd42-9e12-4b01-8222-3f9e1afd46e9", "ac72cdb0-029d-4462-98fd-80d3d1335c1a", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fff71b05-1875-44e5-a133-34773686d066", "8fb50d7d-99ed-433f-8505-0f9374f53b22", "Analyst", "ANALYST" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopulationLocation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57400c69-bfcc-432c-8379-e1c031c6eb7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7121cd42-9e12-4b01-8222-3f9e1afd46e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fff71b05-1875-44e5-a133-34773686d066");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49e94125-14d9-4d7c-9d5a-45efa07d42d2", "4afd8c16-ff8c-4fe1-b771-9c30d7202269", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc8772c0-cfbf-4412-b466-9426cb9d3bd2", "f04e6904-b8a8-4a63-a7e0-2a93a9fe8975", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f62ab906-b80f-499e-a431-aea9c88e3225", "1494b896-bbc0-4e2f-be5d-92c7400f877e", "Analyst", "ANALYST" });
        }
    }
}
