using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "New Delhi", "India", "Infosys Pvt. Ltd." },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Plot 13, Gurgaon", "India", "Nagarro Software Pvt. Ltd." }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("1703d193-34a4-40ea-9423-bc8452e158d1"), 23, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Nikhil Mishra", "Intern" },
                    { new Guid("616222b0-e9e3-47df-887b-d78476410fac"), 25, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sourya Gupta", "Junior Developer" },
                    { new Guid("929ab1b5-16a0-45e0-81ec-c621d363ebbf"), 24, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Manish Jaiswal", "Senior Engineer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("1703d193-34a4-40ea-9423-bc8452e158d1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("616222b0-e9e3-47df-887b-d78476410fac"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("929ab1b5-16a0-45e0-81ec-c621d363ebbf"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
