using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnum_API_web_application.Migrations
{
    /// <inheritdoc />
    public partial class AddingManyToManyCompetitionCompResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionMember");

            migrationBuilder.DropTable(
                name: "Competitions_Members");

            migrationBuilder.CreateTable(
                name: "Competitions_Members_Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions_Members_Results", x => new { x.Id, x.MemberId, x.CompetitionId, x.ResultId });
                    table.ForeignKey(
                        name: "FK_Competitions_Members_Results_CompetitionResults_ResultId",
                        column: x => x.ResultId,
                        principalTable: "CompetitionResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitions_Members_Results_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitions_Members_Results_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_Results_CompetitionId",
                table: "Competitions_Members_Results",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_Results_MemberId",
                table: "Competitions_Members_Results",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_Results_ResultId",
                table: "Competitions_Members_Results",
                column: "ResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competitions_Members_Results");

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

            migrationBuilder.CreateTable(
                name: "Competitions_Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    ResultNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_Members_CompetitionResults_ResultId",
                        column: x => x.ResultId,
                        principalTable: "CompetitionResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitions_Members_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitions_Members_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionMember_MembersId",
                table: "CompetitionMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_CompetitionId",
                table: "Competitions_Members",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_MemberId",
                table: "Competitions_Members",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_Members_ResultId",
                table: "Competitions_Members",
                column: "ResultId");
        }
    }
}
