using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomEremenko.Data.Migrations
{
    public partial class AddLoyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loyalties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    UserLoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loyalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loyalties_UsersLoyaltie_UserLoyaltyId",
                        column: x => x.UserLoyaltyId,
                        principalTable: "UsersLoyaltie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loyalties_UserLoyaltyId",
                table: "Loyalties",
                column: "UserLoyaltyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loyalties");
        }
    }
}
