using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackYourExpenseApi.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfTransaction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacations_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[,]
                {
                    { new Guid("7855e981-32bc-4ce2-a32e-89421876462d"), "Monthly Rent Expense", "Rent" },
                    { new Guid("390d54b7-ed6e-42b1-99cb-109aafe1ae2e"), "Monthly Rent Expense", "Miscellaneous" }
                });

            migrationBuilder.InsertData(
                table: "Transacations",
                columns: new[] { "Id", "Amount", "DateOfTransaction", "Description", "ExpenseId", "Text" },
                values: new object[,]
                {
                    { new Guid("cab11e4c-62b8-449d-8197-a5c788af85b4"), 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Car Rental Expenses", null, "Car Rent" },
                    { new Guid("f6489e6f-45af-4cb1-95ce-1ac658f891af"), 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Rent Expense", null, "Miscellaneous" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacations_ExpenseId",
                table: "Transacations",
                column: "ExpenseId",
                unique: true,
                filter: "[ExpenseId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
