using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _5_LayeredArchitecture.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fKey = table.Column<int>(type: "integer", nullable: false),
                    Blok = table.Column<string>(type: "text", nullable: false),
                    Nitelik = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParcelNo = table.Column<int>(type: "integer", nullable: false),
                    Pafta = table.Column<string>(type: "text", nullable: false),
                    Ada = table.Column<int>(type: "integer", nullable: false),
                    il = table.Column<string>(type: "text", nullable: false),
                    ilce = table.Column<string>(type: "text", nullable: false),
                    mahalle = table.Column<string>(type: "text", nullable: false),
                    nitelik = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Blok", "Nitelik", "fKey" },
                values: new object[,]
                {
                    { 1, "b1", "n1", 1 },
                    { 2, "b2", "n2", 2 }
                });

            migrationBuilder.InsertData(
                table: "Parcels",
                columns: new[] { "Id", "Ada", "Pafta", "ParcelNo", "il", "ilce", "mahalle", "nitelik" },
                values: new object[,]
                {
                    { 1, 1, "p1", 1, "il1", "ilce1", "m1", "n1" },
                    { 2, 2, "p2", 2, "il2", "ilce2", "m2", "n2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Parcels");
        }
    }
}
