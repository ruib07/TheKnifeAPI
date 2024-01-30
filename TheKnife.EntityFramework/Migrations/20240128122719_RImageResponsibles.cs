using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheKnife.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RImageResponsibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "RestaurantResponsibles",
                newName: "RImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "RestaurantResponsibles",
                newName: "RImage");
        }
    }
}
