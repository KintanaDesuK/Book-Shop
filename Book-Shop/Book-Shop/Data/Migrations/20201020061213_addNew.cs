using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Book_Shop.Data.Migrations
{
    public partial class addNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_SalesPersonId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForOrders_Books_BookId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForOrders_Orders_OrderId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SalesPersonId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ProductsSelectedForOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "ProductsSelectedForOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShopDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForOrders_Books_BookId",
                table: "ProductsSelectedForOrders",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForOrders_Orders_OrderId",
                table: "ProductsSelectedForOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForOrders_Books_BookId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSelectedForOrders_Orders_OrderId",
                table: "ProductsSelectedForOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShopDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ProductsSelectedForOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "ProductsSelectedForOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "ProductsSelectedForOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductsSelectedForOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SalesPersonId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalesPersonId",
                table: "Orders",
                column: "SalesPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_SalesPersonId",
                table: "Orders",
                column: "SalesPersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForOrders_Books_BookId",
                table: "ProductsSelectedForOrders",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSelectedForOrders_Orders_OrderId",
                table: "ProductsSelectedForOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
