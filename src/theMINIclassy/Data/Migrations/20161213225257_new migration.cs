using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace theMINIclassy.Data.Migrations
{
    public partial class newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Product",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "Product",
                nullable: true);
        }
    }
}
