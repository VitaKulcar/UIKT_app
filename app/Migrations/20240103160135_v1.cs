using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delavec_Vloga_VlogaId",
                table: "Delavec");

            migrationBuilder.DropIndex(
                name: "IX_Delavec_VlogaId",
                table: "Delavec");

            migrationBuilder.DropColumn(
                name: "VlogaId",
                table: "Delavec");

            migrationBuilder.CreateTable(
                name: "DelavecVloga",
                columns: table => new
                {
                    DelavciId = table.Column<int>(type: "INTEGER", nullable: false),
                    VlogeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelavecVloga", x => new { x.DelavciId, x.VlogeId });
                    table.ForeignKey(
                        name: "FK_DelavecVloga_Delavec_DelavciId",
                        column: x => x.DelavciId,
                        principalTable: "Delavec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DelavecVloga_Vloga_VlogeId",
                        column: x => x.VlogeId,
                        principalTable: "Vloga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelavecVloga_VlogeId",
                table: "DelavecVloga",
                column: "VlogeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelavecVloga");

            migrationBuilder.AddColumn<int>(
                name: "VlogaId",
                table: "Delavec",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delavec_VlogaId",
                table: "Delavec",
                column: "VlogaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delavec_Vloga_VlogaId",
                table: "Delavec",
                column: "VlogaId",
                principalTable: "Vloga",
                principalColumn: "Id");
        }
    }
}
