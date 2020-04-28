namespace Hotel.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddHotelDataToSpecialOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelDataId",
                table: "SpecialOffers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOffers_HotelDataId",
                table: "SpecialOffers",
                column: "HotelDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialOffers_Hotels_HotelDataId",
                table: "SpecialOffers",
                column: "HotelDataId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialOffers_Hotels_HotelDataId",
                table: "SpecialOffers");

            migrationBuilder.DropIndex(
                name: "IX_SpecialOffers_HotelDataId",
                table: "SpecialOffers");

            migrationBuilder.DropColumn(
                name: "HotelDataId",
                table: "SpecialOffers");
        }
    }
}
