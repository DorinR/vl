using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapitest.Migrations
{
    /// <inheritdoc />
    public partial class distortionstableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distortion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distortion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistortionThought",
                columns: table => new
                {
                    DistortionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThoughtsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistortionThought", x => new { x.DistortionsId, x.ThoughtsId });
                    table.ForeignKey(
                        name: "FK_DistortionThought_Distortion_DistortionsId",
                        column: x => x.DistortionsId,
                        principalTable: "Distortion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistortionThought_Thoughts_ThoughtsId",
                        column: x => x.ThoughtsId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistortionThought_ThoughtsId",
                table: "DistortionThought",
                column: "ThoughtsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistortionThought");

            migrationBuilder.DropTable(
                name: "Distortion");
        }
    }
}
