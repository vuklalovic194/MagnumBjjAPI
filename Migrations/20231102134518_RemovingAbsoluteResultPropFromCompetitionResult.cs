using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnum_API_web_application.Migrations
{
    /// <inheritdoc />
    public partial class RemovingAbsoluteResultPropFromCompetitionResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsoluteResult",
                table: "CompetitionResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AbsoluteResult",
                table: "CompetitionResults",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
