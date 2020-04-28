namespace Hotel.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPaymentTypeToReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentTypeId",
                table: "Reservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PaymentTypeId",
                table: "Reservations",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PaymentTypes_PaymentTypeId",
                table: "Reservations",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PaymentTypes_PaymentTypeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PaymentTypeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Reservations");
        }
    }
}
