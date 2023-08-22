using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScrapingIntegration.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebScrapingProces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenQuery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebScrapingProces", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebScrapingProces");
        }
    }
}
