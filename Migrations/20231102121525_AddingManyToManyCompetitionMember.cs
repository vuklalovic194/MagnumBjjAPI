using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnum_API_web_application.Migrations
{
    /// <inheritdoc />
    public partial class AddingManyToManyCompetitionMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Members_Competitions_CompetitionId",
                table: "Competitions_Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Members_Members_MemberId",
                table: "Competitions_Members");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_Members_MemberId",
                table: "Competitions_Members");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Competitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AbsoluteResult",
                table: "CompetitionResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionMember",
                columns: table => new
                {
                    CompetitionsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionMember", x => new { x.CompetitionsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_CompetitionMember_Competitions_CompetitionsId",
                        column: x => x.CompetitionsId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_MemberId",
                table: "Competitions_Members",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMember_MembersId",
                table: "CompetitionMember",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Members_Competitions_CompetitionId",
                table: "Competitions_Members",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Members_Members_MemberId",
                table: "Competitions_Members",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Members_Competitions_CompetitionId",
                table: "Competitions_Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Members_Members_MemberId",
                table: "Competitions_Members");

            migrationBuilder.DropTable(
                name: "CompetitionMember");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_Members_MemberId",
                table: "Competitions_Members");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "AbsoluteResult",
                table: "CompetitionResults");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_MemberId",
                table: "Competitions_Members",
                column: "MemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Members_Competitions_CompetitionId",
                table: "Competitions_Members",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Members_Members_MemberId",
                table: "Competitions_Members",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
