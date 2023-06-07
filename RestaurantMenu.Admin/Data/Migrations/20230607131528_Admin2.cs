using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMenu.Admin.Data.Migrations
{
    /// <inheritdoc />
    public partial class Admin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35097eed-ee95-477a-8c2c-3ba7f1ccb42f", 0, "45807f2f-e888-4677-abe4-18774b5135a2", "GigiGagaRestAdmin@mail.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPc4rAnIrA1NVhlYL1A4Omv8Cngx34B0PMCk2SD3bDKvDp8tuBjqd5Fd99rm+HRfqg==", null, false, "a90dd826-9f95-4a17-b291-d352905a074c", false, "GigiGagaRestAdmin@mail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35097eed-ee95-477a-8c2c-3ba7f1ccb42f");
        }
    }
}
