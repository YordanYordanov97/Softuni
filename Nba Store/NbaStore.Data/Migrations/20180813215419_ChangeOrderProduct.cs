using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaStore.Data.Migrations
{
    public partial class ChangeOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductTitle",
                table: "OrderProducts",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductPicture",
                table: "OrderProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize",
                table: "OrderProducts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "OrderProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ProductTitle",
                table: "OrderProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ProductPicture",
                table: "OrderProducts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
