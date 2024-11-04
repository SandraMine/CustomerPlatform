using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerPlatform.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation_PrimaryMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation_SecondaryMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
