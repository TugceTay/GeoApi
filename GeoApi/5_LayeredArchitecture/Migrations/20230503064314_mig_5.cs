using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace _5_LayeredArchitecture.Migrations
{
    public partial class mig_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Buildings");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<Geometry>(
                name: "geom",
                table: "Parcels",
                type: "geometry",
                nullable: true);

            migrationBuilder.AddColumn<Geometry>(
                name: "geom",
                table: "Buildings",
                type: "geometry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "geom",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "geom",
                table: "Buildings");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "Parcels",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "Parcels",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "Buildings",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "Buildings",
                type: "double precision",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "X", "Y" },
                values: new object[] { 39.100000000000001, 33.100000000000001 });

            migrationBuilder.UpdateData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "X", "Y" },
                values: new object[] { 39.0, 33.0 });
        }
    }
}
