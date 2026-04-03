using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressCustomers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAmount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "StreetAddress" },
                values: new object[] { 1, "Uddevalla", "Sweden", "Uddevallagatan 1" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Levi´s" },
                    { 2, "Weekday" },
                    { 3, "Nike" },
                    { 4, "Tommy Hilfiger" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pants" },
                    { 2, "Shirt" },
                    { 3, "Jacket" },
                    { 4, "Shoes" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "Phone" },
                values: new object[] { 1, new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erik.eriksson@gmail.se", "Erik Eriksson", "0705679901" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Shippings",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Postnord", 4m },
                    { 2, "Bring", 2m }
                });

            migrationBuilder.InsertData(
                table: "AddressCustomers",
                columns: new[] { "Id", "AddressId", "CustomerId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderNumber", "PaymentId", "ShippingId" },
                values: new object[,]
                {
                    { 1, 1, "1001050608", 1, 1 },
                    { 2, 1, "1001050609", 2, 2 },
                    { 3, 1, "1001070819", 2, 1 },
                    { 4, 1, "1001037849", 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "LongDescription", "Name", "OnSale", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, 1, 1, "Made from 98% cotton and 2% elastane. Machine wash at 30°C, do not tumble dry. Iron inside out on low heat. Not water resistant.", "White Slim Fit Jeans", false, 60m, "Clean and crisp white jeans for any casual occasion." },
                    { 2, 2, 1, "Crafted from 100% premium cotton. Dry clean recommended. Iron on medium heat. Tailored fit with a straight leg cut.", "Black Suit Trousers in Cotton", false, 80m, "Elegant black trousers perfect for the office or a night out." },
                    { 3, 2, 1, "Made from soft cotton blend. Machine wash at 40°C. Tumble dry on low. Iron on medium heat. Versatile enough for both casual and smart casual looks.", "Grey Relaxed Chinos", false, 55m, "Comfortable everyday chinos with a modern relaxed fit." },
                    { 4, 1, 1, "Made from durable ripstop fabric, 100% cotton. Machine wash at 30°C. Do not tumble dry. Water resistant coating on the outer layer.", "Navy Blue Cargo Pants", false, 60m, "Functional cargo pants with plenty of pocket space." },
                    { 5, 1, 1, "95% cotton, 5% elastane. Machine wash at 30°C inside out to preserve color. Do not bleach. Iron on low heat.", "Dark Wash Straight Leg Jeans", true, 70m, "A timeless dark wash jean that goes with everything." },
                    { 6, 4, 2, "100% cotton Oxford weave. Machine wash at 40°C. Iron on medium to high heat while slightly damp for best results. Not water resistant.", "White Oxford Button-Down Shirt", false, 45m, "A classic white Oxford shirt for a clean everyday look." },
                    { 7, 4, 2, "Made from 100% cotton. Machine wash at 30°C. Iron on medium heat. Slim fit cut designed to be tucked in.", "Slim Fit Striped Dress Shirt", false, 50m, "Sharp striped dress shirt for a polished office look." },
                    { 8, 2, 2, "100% brushed cotton flannel. Machine wash at 40°C. Tumble dry on low. Iron on medium heat. Can be worn open as a light jacket.", "Oversized Flannel Shirt", false, 40m, "Cozy flannel shirt perfect for layering on cooler days." },
                    { 9, 2, 2, "55% linen, 45% cotton. Machine wash at 30°C or hand wash. Do not tumble dry. Iron on medium heat while damp. Lightweight and breathable.", "Black Linen Shirt", false, 45m, "Breathable linen shirt ideal for warm weather." },
                    { 10, 1, 2, "100% cotton denim. Machine wash at 30°C inside out to preserve color. Do not bleach. Iron on medium heat.", "Denim Shirt Regular Fit", true, 47m, "Versatile denim shirt that works as a shirt or light jacket." },
                    { 11, 3, 3, "Shell: 100% polyester. Filling: 80% down, 20% feather. Machine wash at 30°C on gentle cycle. Water resistant outer shell. Suitable for temperatures down to -10°C.", "Black Puffer Jacket", false, 13m, "Warm and lightweight puffer jacket for cold days." },
                    { 12, 2, 3, "100% nylon shell with polyester lining. Hand wash or dry clean only. Water resistant. Not suitable for heavy rain. Iron on low heat on reverse side only.", "Navy Bomber Jacket", true, 10m, "Classic bomber jacket with a modern slim fit." },
                    { 13, 3, 3, "65% polyester, 35% cotton. Machine wash at 30°C. Water resistant coating. Suitable for light rain. Do not tumble dry. Iron on low heat.", "Khaki Field Jacket", false, 11m, "Rugged field jacket with multiple pockets for outdoor adventures." },
                    { 14, 4, 3, "60% wool, 40% polyester. Dry clean only. Not water resistant — avoid heavy rain. Iron on low heat with a damp cloth. Provides excellent insulation in cold weather.", "Wool Blend Overcoat", false, 200m, "Elegant overcoat perfect for a smart winter look." },
                    { 15, 3, 3, "100% recycled polyester. Machine wash at 30°C. Highly wind resistant and water repellent. Packable into its own pocket. Great for hiking and running.", "Windbreaker Jacket", false, 85m, "Lightweight windbreaker ideal for outdoor activities." },
                    { 16, 3, 4, "Upper: 100% leather. Sole: rubber. Wipe clean with a damp cloth. Use leather conditioner to maintain quality. Not suitable for heavy rain or wet conditions.", "White Leather Sneakers", false, 90m, "Clean white leather sneakers that go with almost anything." },
                    { 17, 3, 4, "Mesh upper for breathability. Rubber outsole with grip pattern for various surfaces. Machine washable at 30°C in a laundry bag. Remove insoles before washing. Not waterproof.", "Black Running Shoes", false, 120m, "Lightweight running shoes with excellent cushioning." },
                    { 18, 4, 4, "Upper: genuine leather. Sole: rubber. Clean with a damp cloth and leather conditioner. Water resistant but avoid prolonged exposure to rain. Polish regularly to maintain finish.", "Brown Chelsea Boots", false, 150m, "Classic Chelsea boots that elevate any outfit." },
                    { 19, 2, 4, "Canvas upper, rubber sole. Machine wash at 30°C in a laundry bag. Air dry only, do not tumble dry. Lightweight and flexible for all-day comfort.", "Grey Slip-On Sneakers", false, 70m, "Easy slip-on sneakers for effortless everyday wear." },
                    { 20, 3, 4, "100% cotton canvas upper, rubber sole. Machine wash at 30°C. Air dry only. Not water resistant. Available in multiple colorways.", "High-Top Canvas Sneakers", false, 70m, "Iconic high-top sneakers with a streetwear edge." }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductAmount", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 2, 1 },
                    { 2, 2, 1, 5 },
                    { 3, 3, 1, 10 },
                    { 4, 4, 3, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressCustomers_AddressId",
                table: "AddressCustomers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCustomers_CustomerId",
                table: "AddressCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingId",
                table: "Orders",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressCustomers");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
