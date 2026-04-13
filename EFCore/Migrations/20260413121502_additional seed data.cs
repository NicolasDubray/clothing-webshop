using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class additionalseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "StreetAddress" },
                values: new object[,]
                {
                    { 2, "Stockholm", "Sweden", "Biblioteksgatan 5" },
                    { 3, "Uddevalla", "Sweden", "Kaserngården 6" },
                    { 4, "Göteborg", "Sweden", "Göteborgsgatan 15" },
                    { 5, "Stenungsund", "Sweden", "Hallerna Hills 20" },
                    { 6, "Göteborg", "Sweden", "Pressarevägen" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 2, new DateTime(2002, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "majaek@gmail.se", "Maja Ek", "0705586547" },
                    { 3, new DateTime(1972, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "lasse@hotmail.se", "Lasse Olsson", "0765004545" },
                    { 4, new DateTime(1990, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "frida1990@gmail.com", "Frida Bengtsson", "0703441919" },
                    { 5, new DateTime(1972, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "ronny.david@hotmail.se", "Ronny Davidsson", "0756789900" },
                    { 6, new DateTime(1980, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "vera.eriksson@hotmail.se", "Vera Eriksson", "0708908888" }
                });

            migrationBuilder.InsertData(
                table: "AddressCustomers",
                columns: new[] { "Id", "AddressId", "CustomerId" },
                values: new object[,]
                {
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderNumber", "PaymentId", "ShippingId" },
                values: new object[,]
                {
                    { 5, 2, "1001045646", 3, 1 },
                    { 6, 2, "1001012482", 3, 1 },
                    { 7, 3, "1001014742", 1, 1 },
                    { 8, 4, "1001014455", 1, 1 },
                    { 9, 4, "1001054587", 2, 2 },
                    { 10, 5, "1001057719", 2, 2 },
                    { 11, 5, "1001046466", 3, 1 },
                    { 12, 6, "1001078419", 1, 2 },
                    { 13, 6, "1001038888", 2, 1 },
                    { 14, 6, "1001031243", 1, 2 },
                    { 15, 6, "1001010100", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductAmount", "ProductId" },
                values: new object[,]
                {
                    { 5, 5, 1, 2 },
                    { 6, 6, 2, 3 },
                    { 7, 7, 3, 4 },
                    { 8, 8, 1, 5 },
                    { 9, 9, 2, 6 },
                    { 10, 10, 3, 7 },
                    { 11, 10, 2, 8 },
                    { 12, 10, 1, 9 },
                    { 13, 11, 1, 12 },
                    { 14, 12, 1, 13 },
                    { 15, 13, 2, 14 },
                    { 16, 14, 2, 18 },
                    { 17, 15, 5, 20 },
                    { 18, 15, 3, 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AddressCustomers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AddressCustomers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AddressCustomers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AddressCustomers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AddressCustomers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
