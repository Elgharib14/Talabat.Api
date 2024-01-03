using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleverTime",
                table: "DeleveryMethods",
                newName: "DeliveryTime");

            migrationBuilder.AlterColumn<int>(
                name: "DeleveryMethodId",
                table: "orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeleveryMethods",
                newName: "DeleverTime");

            migrationBuilder.AlterColumn<int>(
                name: "DeleveryMethodId",
                table: "orders",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
