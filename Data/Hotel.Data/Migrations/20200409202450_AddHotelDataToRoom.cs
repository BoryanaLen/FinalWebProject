using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.Data.Migrations
{
    public partial class AddHotelDataToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelDataId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelDataId",
                table: "Rooms",
                column: "HotelDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelDataId",
                table: "Rooms",
                column: "HotelDataId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelDataId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelDataId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HotelDataId",
                table: "Rooms");
        }
    }
}
