using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_companyid",
                table: "subscriptions",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_companyid",
                table: "orders",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_menuitemid",
                table: "orderitems",
                column: "menuitemid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_menuitems_menuid",
                table: "menuitems",
                column: "menuid");

            migrationBuilder.AddForeignKey(
                name: "FK_menuitems_menus_menuid",
                table: "menuitems",
                column: "menuid",
                principalTable: "menus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems",
                column: "menuitemid",
                principalTable: "menuitems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_orders_orderid",
                table: "orderitems",
                column: "orderid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_companies_companyid",
                table: "orders",
                column: "companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_companies_companyid",
                table: "subscriptions",
                column: "companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuitems_menus_menuid",
                table: "menuitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_orders_orderid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_companies_companyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_companies_companyid",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_companyid",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_orders_companyid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orderitems_menuitemid",
                table: "orderitems");

            migrationBuilder.DropIndex(
                name: "IX_orderitems_orderid",
                table: "orderitems");

            migrationBuilder.DropIndex(
                name: "IX_menuitems_menuid",
                table: "menuitems");
        }
    }
}
