using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Date.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_EmployeesId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeesId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentsId",
                table: "Employees",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentsId",
                table: "Employees",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeesId",
                table: "Departments",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_EmployeesId",
                table: "Departments",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
