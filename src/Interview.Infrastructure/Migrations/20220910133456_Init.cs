using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview.Infrastructure.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Day",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Day", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Restaurant",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Restaurant", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Schedule",
            columns: table => new
            {
                RestaurantId = table.Column<int>(type: "int", nullable: false),
                DayId = table.Column<int>(type: "int", nullable: false),
                Start = table.Column<TimeSpan>(type: "time", nullable: false),
                End = table.Column<TimeSpan>(type: "time", nullable: false),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Schedule", x => new { x.DayId, x.RestaurantId });
                table.ForeignKey(
                    name: "FK_Schedule_Day_DayId",
                    column: x => x.DayId,
                    principalTable: "Day",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Schedule_Restaurant_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurant",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Day_Name",
            table: "Day",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Restaurant_Name",
            table: "Restaurant",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Schedule_RestaurantId",
            table: "Schedule",
            column: "RestaurantId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Schedule");

        migrationBuilder.DropTable(
            name: "Day");

        migrationBuilder.DropTable(
            name: "Restaurant");
    }
}
