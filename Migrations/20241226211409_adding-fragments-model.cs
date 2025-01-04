using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapitest.Migrations
{
    /// <inheritdoc />
    public partial class addingfragmentsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FragmentModel_Artifacts_ArtifactId",
                table: "FragmentModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FragmentModel",
                table: "FragmentModel");

            migrationBuilder.RenameTable(
                name: "FragmentModel",
                newName: "Fragments");

            migrationBuilder.RenameIndex(
                name: "IX_FragmentModel_ArtifactId",
                table: "Fragments",
                newName: "IX_Fragments_ArtifactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fragments",
                table: "Fragments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fragments_Artifacts_ArtifactId",
                table: "Fragments",
                column: "ArtifactId",
                principalTable: "Artifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fragments_Artifacts_ArtifactId",
                table: "Fragments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fragments",
                table: "Fragments");

            migrationBuilder.RenameTable(
                name: "Fragments",
                newName: "FragmentModel");

            migrationBuilder.RenameIndex(
                name: "IX_Fragments_ArtifactId",
                table: "FragmentModel",
                newName: "IX_FragmentModel_ArtifactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FragmentModel",
                table: "FragmentModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FragmentModel_Artifacts_ArtifactId",
                table: "FragmentModel",
                column: "ArtifactId",
                principalTable: "Artifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
