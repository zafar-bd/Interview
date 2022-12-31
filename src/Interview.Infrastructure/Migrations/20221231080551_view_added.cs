using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class viewadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW Vw_Restaurant
                                    AS
                                    	SELECT
                                    	  dbo.Restaurant.[Name],
                                    	  dbo.Schedule.[Start],
                                    	  dbo.Schedule.[End],
                                    	  dbo.[Day].[Name] AS [DAY]
                                    	FROM
                                    	  dbo.Restaurant
                                    	  INNER JOIN dbo.Schedule ON dbo.Restaurant.Id = dbo.Schedule.RestaurantId
                                    	  INNER JOIN dbo.[Day] ON dbo.Schedule.DayId = dbo.[Day].Id
                                    GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW Vw_Restaurant");
        }
    }
}
