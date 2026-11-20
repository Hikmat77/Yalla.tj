using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationshipDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_companies_companyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_companies_companyid",
                table: "subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems",
                column: "menuitemid",
                principalTable: "menuitems",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_companies_companyid",
                table: "orders",
                column: "companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_companies_companyid",
                table: "subscriptions",
                column: "companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_companies_companyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_companies_companyid",
                table: "subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_menuitems_menuitemid",
                table: "orderitems",
                column: "menuitemid",
                principalTable: "menuitems",
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
    }
}
