using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapitest.Migrations
{
    /// <inheritdoc />
    public partial class eventstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Situation = table.Column<string>(type: "text", nullable: false),
                    Thoughts = table.Column<string>(type: "text", nullable: false),
                    Emotions = table.Column<string>(type: "text", nullable: false),
                    ResultingBehaviour = table.Column<string>(type: "text", nullable: false),
                    ChallengePrompt = table.Column<string>(type: "text", nullable: false),
                    Challenge = table.Column<string>(type: "text", nullable: false),
                    Outcome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistortionEvent",
                columns: table => new
                {
                    DistortionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistortionEvent", x => new { x.DistortionsId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_DistortionEvent_Distortion_DistortionsId",
                        column: x => x.DistortionsId,
                        principalTable: "Distortion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistortionEvent_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistortionEvent_EventsId",
                table: "DistortionEvent",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistortionEvent");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
