using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaStore.Data.Migrations
{
    public partial class ChangeTitleValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
