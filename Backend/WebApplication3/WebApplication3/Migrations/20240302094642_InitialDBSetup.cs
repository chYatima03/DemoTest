using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    docno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    docname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    factoryno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    factoryname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    lotno = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    qty = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    expiredate = table.Column<DateTime>(type: "datetime2", maxLength: 10, nullable: false),
                    currentwmsno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    stockstatus = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: true),
                    outstore = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    instore = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    currentwmsno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Document",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StorageLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FactoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageLocations_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transferdetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TransfersId = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    outwmsno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    inwmsno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferdetails_Transfers_TransfersId",
                        column: x => x.TransfersId,
                        principalTable: "Transfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    factoriesid = table.Column<int>(type: "int", nullable: true),
                    FactoryId = table.Column<int>(type: "int", nullable: true),
                    storagelocid = table.Column<int>(type: "int", nullable: true),
                    StorageLocationId = table.Column<int>(type: "int", nullable: true),
                    zone = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    layer = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    road = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    column = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    row = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    position = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedby = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_StorageLocations_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "ECE", "ECE Department" },
                    { 2, "CSE", "CSE Department" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "DepartmentId", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "India", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "venkat@gmail.com", "Venkat" },
                    { 2, "India", new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "nehanth@gmail.com", "Nehanth" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_FactoryId",
                table: "Locations",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StorageLocationId",
                table: "Locations",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocations_FactoryId",
                table: "StorageLocations",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferdetails_TransfersId",
                table: "Transferdetails",
                column: "TransfersId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DocumentId",
                table: "Transfers",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Transferdetails");

            migrationBuilder.DropTable(
                name: "StorageLocations");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Factories");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
