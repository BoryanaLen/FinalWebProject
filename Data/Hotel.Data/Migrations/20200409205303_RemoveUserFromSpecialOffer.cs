namespace Hotel.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveUserFromSpecialOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialOffers_AspNetUsers_UserId",
                table: "SpecialOffers");

            migrationBuilder.DropIndex(
                name: "IX_SpecialOffers_UserId",
                table: "SpecialOffers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SpecialOffers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SpecialOffers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOffers_UserId",
                table: "SpecialOffers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialOffers_AspNetUsers_UserId",
                table: "SpecialOffers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
