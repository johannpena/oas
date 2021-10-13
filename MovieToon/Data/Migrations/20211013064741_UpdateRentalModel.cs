using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieToon.Data.Migrations
{
    public partial class UpdateRentalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Customer_CustomerModelId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_CustomerModelId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "CustomerModelId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Rental",
                newName: "EndDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RentalMovie",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Movie",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Membership",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Membership",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CustomerId",
                table: "Rental",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_CustomerId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Rental",
                newName: "EndTime");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "RentalMovie",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerModelId",
                table: "Rental",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Membership",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Membership",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CustomerModelId",
                table: "Rental",
                column: "CustomerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Customer_CustomerModelId",
                table: "Rental",
                column: "CustomerModelId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
