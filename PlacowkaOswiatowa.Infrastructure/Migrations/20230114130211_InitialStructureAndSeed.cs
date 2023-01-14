using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlacowkaOswiatowa.Infrastructure.Migrations
{
    public partial class InitialStructureAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etaty",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etaty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Miejscowosci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miejscowosci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Panstwa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panstwa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DniUrlopu = table.Column<int>(type: "int", nullable: true),
                    NrTelefonu = table.Column<string>(type: "varchar(11)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrugieImie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "date", maxLength: 10, nullable: true),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Przedmioty",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkroconaNazwa = table.Column<string>(type: "char(3)", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmioty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stanowiska",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanowiska", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ulice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oddzialy",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oddzialy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oddzialy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urlopy",
                columns: table => new
                {
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    PoczatekUrlopu = table.Column<DateTime>(type: "date", nullable: false),
                    KoniecUrlopu = table.Column<DateTime>(type: "date", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urlopy", x => new { x.PracownikId, x.PoczatekUrlopu });
                    table.ForeignKey(
                        name: "FK_Urlopy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrzedmiotyPracownicy",
                columns: table => new
                {
                    PrzedmiotId = table.Column<byte>(type: "tinyint", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzedmiotyPracownicy", x => new { x.PracownikId, x.PrzedmiotId });
                    table.ForeignKey(
                        name: "FK_PrzedmiotyPracownicy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrzedmiotyPracownicy_Przedmioty_PrzedmiotId",
                        column: x => x.PrzedmiotId,
                        principalTable: "Przedmioty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HashHasla = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RolaId = table.Column<byte>(type: "tinyint", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_Role_RolaId",
                        column: x => x.RolaId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adresy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanstwoId = table.Column<int>(type: "int", nullable: false),
                    MiejscowoscId = table.Column<int>(type: "int", nullable: false),
                    UlicaId = table.Column<int>(type: "int", nullable: true),
                    NumerDomu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NumerMieszkania = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    KodPocztowy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresy_Miejscowosci_MiejscowoscId",
                        column: x => x.MiejscowoscId,
                        principalTable: "Miejscowosci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adresy_Panstwa_PanstwoId",
                        column: x => x.PanstwoId,
                        principalTable: "Panstwa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adresy_Ulice_UlicaId",
                        column: x => x.UlicaId,
                        principalTable: "Ulice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Migawki",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZmiany = table.Column<DateTime>(type: "datetime", nullable: false, computedColumnSql: "getdate()"),
                    Szczegoly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Migawki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Migawki_Uzytkownicy_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pracodawcy",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    Regon = table.Column<string>(type: "char(9)", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracodawcy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracodawcy_Adresy_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracownicyAdresy",
                columns: table => new
                {
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracownicyAdresy", x => new { x.PracownikId, x.AdresId });
                    table.ForeignKey(
                        name: "FK_PracownicyAdresy_Adresy_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracownicyAdresy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uczniowie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OddzialId = table.Column<byte>(type: "tinyint", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrugieImie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "date", maxLength: 10, nullable: true),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczniowie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Adresy_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Uczniowie_Oddzialy_OddzialId",
                        column: x => x.OddzialId,
                        principalTable: "Oddzialy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Umowy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    PracodawcaId = table.Column<byte>(type: "tinyint", nullable: false),
                    WynagrodzenieBrutto = table.Column<decimal>(type: "money", precision: 7, scale: 2, nullable: false),
                    CzyZwolnionyOdPodatku = table.Column<bool>(type: "bit", nullable: false),
                    OpisWynagrodzenia = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MiejsceWykonywaniaPracy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    WymiarCzasuPracy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    WymiarGodzinowy = table.Column<double>(type: "float", nullable: true),
                    OkresPracy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DataZawarciaUmowy = table.Column<DateTime>(type: "date", nullable: false),
                    EtatId = table.Column<byte>(type: "tinyint", nullable: false),
                    StanowiskoId = table.Column<byte>(type: "tinyint", nullable: false),
                    DataRozpoczeciaPracy = table.Column<DateTime>(type: "date", nullable: false),
                    DataZakonczeniaPracy = table.Column<DateTime>(type: "date", nullable: true),
                    InneWarunkiZatrudnienia = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PrzyczynyZawarciaUmowy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime", nullable: false, computedColumnSql: "getdate()"),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Umowy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Umowy_Etaty_EtatId",
                        column: x => x.EtatId,
                        principalTable: "Etaty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Umowy_Pracodawcy_PracodawcaId",
                        column: x => x.PracodawcaId,
                        principalTable: "Pracodawcy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Umowy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Umowy_Stanowiska_StanowiskoId",
                        column: x => x.StanowiskoId,
                        principalTable: "Stanowiska",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Oceny",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WystawionaOcena = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    UczenId = table.Column<int>(type: "int", nullable: false),
                    PrzedmiotId = table.Column<byte>(type: "tinyint", nullable: false),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    DataWystawienia = table.Column<DateTime>(type: "datetime", nullable: false),
                    PoprzedniaOcena = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    DataPoprawienia = table.Column<DateTime>(type: "datetime", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oceny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oceny_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oceny_Przedmioty_PrzedmiotId",
                        column: x => x.PrzedmiotId,
                        principalTable: "Przedmioty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oceny_Uczniowie_UczenId",
                        column: x => x.UczenId,
                        principalTable: "Uczniowie",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Etaty",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { (byte)1, true, "PracownikAdministracyjny", "Pracownik Administracyjny" },
                    { (byte)2, true, "PracownikPedagogiczny", "Pracownik Pedagogiczny" },
                    { (byte)3, true, "PracownikObslugi", "Pracownik Obsługi" }
                });

            migrationBuilder.InsertData(
                table: "Miejscowosci",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { 1, true, "Warszawa", null },
                    { 2, true, "Kraków", null },
                    { 3, true, "Łódź", null },
                    { 4, true, "Wrocław", null },
                    { 5, true, "Poznań", null },
                    { 6, true, "Gdańsk", null },
                    { 7, true, "Szczecin", null },
                    { 8, true, "Bydgoszcz", null },
                    { 9, true, "Lublin", null },
                    { 10, true, "Białystok", null },
                    { 11, true, "Katowice", null },
                    { 12, true, "Gdynia", null },
                    { 13, true, "Częstochowa", null },
                    { 14, true, "Radom", null },
                    { 15, true, "Sosnowiec", null },
                    { 16, true, "Toruń", null },
                    { 17, true, "Kielce", null },
                    { 18, true, "Rzeszów", null },
                    { 19, true, "Gliwice", null },
                    { 20, true, "Zabrze", null },
                    { 21, true, "Olsztyn", null }
                });

            migrationBuilder.InsertData(
                table: "Panstwa",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { 1, true, "Polska", null },
                    { 2, true, "Niemcy", null },
                    { 3, true, "Czechy", null },
                    { 4, true, "Słowacja", null },
                    { 5, true, "Ukraina", null },
                    { 6, true, "Białoruś", null },
                    { 7, true, "Litwa", null },
                    { 8, true, "Rosja", null }
                });

            migrationBuilder.InsertData(
                table: "Pracownicy",
                columns: new[] { "Id", "CzyAktywny", "DataUrodzenia", "DniUrlopu", "DrugieImie", "Email", "Imie", "Nazwisko", "NrTelefonu", "Pesel" },
                values: new object[,]
                {
                    { 1, true, new DateTime(1970, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null, "Monica_Reichert@hotmail.com", "Monica", "Reichert", "060-078-495", "6835611161" },
                    { 2, true, new DateTime(1975, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 88, null, "Marty_Strosin33@gmail.com", "Marty", "Strosin", "556-053-466", "8843638993" },
                    { 3, true, new DateTime(1963, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 62, null, "Leslie_Labadie@hotmail.com", "Leslie", "Labadie", "033-004-579", "9089725672" },
                    { 4, true, new DateTime(1952, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 75, null, "Joseph.Jacobi@hotmail.com", "Joseph", "Jacobi", "485-722-030", "2295548870" },
                    { 5, true, new DateTime(1957, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 89, null, "Eunice82@hotmail.com", "Eunice", "Moore", "901-536-430", "6045731617" },
                    { 6, true, new DateTime(1969, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 66, null, "Jaime_Will67@gmail.com", "Jaime", "Will", "939-757-798", "9603903767" },
                    { 7, true, new DateTime(1978, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 52, null, "Glenn94@yahoo.com", "Glenn", "Beatty", "290-666-357", "1835996032" },
                    { 8, true, new DateTime(1967, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 78, null, "Brad35@yahoo.com", "Brad", "Quigley", "519-778-535", "6508402749" },
                    { 9, true, new DateTime(1992, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 54, null, "Marsha.Pfeffer@yahoo.com", "Marsha", "Pfeffer", "536-047-226", "8876567003" },
                    { 10, true, new DateTime(1996, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, null, "Leroy_Wuckert0@gmail.com", "Leroy", "Wuckert", "164-497-616", "6942166487" }
                });

            migrationBuilder.InsertData(
                table: "Pracownicy",
                columns: new[] { "Id", "CzyAktywny", "DataUrodzenia", "DniUrlopu", "DrugieImie", "Email", "Imie", "Nazwisko", "NrTelefonu", "Pesel" },
                values: new object[,]
                {
                    { 11, true, new DateTime(1944, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 87, null, "Steven.Harvey46@gmail.com", "Steven", "Harvey", "186-792-612", "7649245620" },
                    { 12, true, new DateTime(1995, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, "Hubert_Huel5@hotmail.com", "Hubert", "Huel", "558-130-611", "7227925329" },
                    { 13, true, new DateTime(1991, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 81, null, "Enrique_Ritchie@gmail.com", "Enrique", "Ritchie", "179-542-512", "3755753959" },
                    { 14, true, new DateTime(1977, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, null, "Rafael49@gmail.com", "Rafael", "Kunde", "775-445-970", "0924506445" },
                    { 15, true, new DateTime(1993, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, "Katie13@hotmail.com", "Katie", "Halvorson", "523-404-696", "2005178941" },
                    { 16, true, new DateTime(1983, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null, "Betty13@hotmail.com", "Betty", "Streich", "572-244-400", "1103649206" },
                    { 17, true, new DateTime(1949, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null, "Robin.Hodkiewicz@yahoo.com", "Robin", "Hodkiewicz", "361-718-599", "7619012958" },
                    { 18, true, new DateTime(1982, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null, "Donna_Stiedemann49@gmail.com", "Donna", "Stiedemann", "853-257-871", "5811351941" },
                    { 19, true, new DateTime(1982, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 44, null, "Verna_Mitchell56@hotmail.com", "Verna", "Mitchell", "289-832-237", "2621864394" },
                    { 20, true, new DateTime(1948, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 89, null, "Harvey.Little@hotmail.com", "Harvey", "Little", "880-436-231", "7819882894" },
                    { 21, true, new DateTime(1945, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 99, null, "Nettie.Rutherford11@hotmail.com", "Nettie", "Rutherford", "035-588-376", "8284220091" },
                    { 22, true, new DateTime(1988, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 42, null, "Kay.Kling32@yahoo.com", "Kay", "Kling", "138-891-242", "9164651589" },
                    { 23, true, new DateTime(1950, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null, "Kelly26@hotmail.com", "Kelly", "Kilback", "195-310-380", "3070453999" },
                    { 24, true, new DateTime(1956, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 39, null, "Marshall.Koss@hotmail.com", "Marshall", "Koss", "958-524-673", "8466777926" },
                    { 25, true, new DateTime(1950, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 83, null, "Marion26@hotmail.com", "Marion", "Casper", "534-129-319", "2453535651" },
                    { 26, true, new DateTime(1950, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null, "Melanie_Flatley31@yahoo.com", "Melanie", "Flatley", "131-342-180", "2117460917" },
                    { 27, true, new DateTime(1993, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null, "Woodrow_Strosin@hotmail.com", "Woodrow", "Strosin", "047-461-000", "1966009558" },
                    { 28, true, new DateTime(1948, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "Al.Gutkowski27@yahoo.com", "Al", "Gutkowski", "773-544-728", "3685916388" },
                    { 29, true, new DateTime(1948, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 74, null, "Travis19@hotmail.com", "Travis", "Homenick", "043-599-483", "8422801549" },
                    { 30, true, new DateTime(1954, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 59, null, "Lionel19@gmail.com", "Lionel", "Bailey", "858-804-798", "0706943478" },
                    { 31, true, new DateTime(1995, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 39, null, "Dora.Rolfson69@gmail.com", "Dora", "Rolfson", "306-782-019", "1255688237" },
                    { 32, true, new DateTime(1969, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null, "Ann.Buckridge@hotmail.com", "Ann", "Buckridge", "243-683-241", "1312678389" },
                    { 33, true, new DateTime(1947, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 73, null, "Vanessa11@hotmail.com", "Vanessa", "Gutkowski", "104-126-562", "0944954098" },
                    { 34, true, new DateTime(1999, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, null, "Janis.Hauck@hotmail.com", "Janis", "Hauck", "229-927-612", "5782327485" },
                    { 35, true, new DateTime(1977, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, "Katrina37@gmail.com", "Katrina", "Dietrich", "177-585-493", "7989173704" },
                    { 36, true, new DateTime(1974, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, "Joann56@yahoo.com", "Joann", "Rogahn", "712-140-539", "1249691080" },
                    { 37, true, new DateTime(1989, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 43, null, "Jeannette_Turcotte@yahoo.com", "Jeannette", "Turcotte", "404-355-910", "2675106383" },
                    { 38, true, new DateTime(1944, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "Claudia_Shanahan77@gmail.com", "Claudia", "Shanahan", "713-425-775", "0605725323" },
                    { 39, true, new DateTime(1961, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, "Eleanor_Schuster40@hotmail.com", "Eleanor", "Schuster", "304-489-999", "6511101605" },
                    { 40, true, new DateTime(1992, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 34, null, "Teresa_Turner94@hotmail.com", "Teresa", "Turner", "704-191-763", "4859549171" },
                    { 41, true, new DateTime(1970, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null, "Courtney_Bauch18@hotmail.com", "Courtney", "Bauch", "778-327-635", "7944457287" },
                    { 42, true, new DateTime(1951, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 55, null, "Opal_Kreiger@hotmail.com", "Opal", "Kreiger", "567-593-569", "6535332361" },
                    { 43, true, new DateTime(1975, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, null, "Marty.Kertzmann@gmail.com", "Marty", "Kertzmann", "724-297-891", "8294028242" },
                    { 44, true, new DateTime(1965, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, null, "Cary_Jenkins58@hotmail.com", "Cary", "Jenkins", "378-772-727", "8833730765" },
                    { 45, true, new DateTime(1945, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, "Edgar88@hotmail.com", "Edgar", "Christiansen", "591-916-687", "4862475450" },
                    { 46, true, new DateTime(1993, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 61, null, "Jimmie_Effertz@hotmail.com", "Jimmie", "Effertz", "892-319-350", "1554095082" },
                    { 47, true, new DateTime(1948, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 67, null, "Ervin.Barrows14@hotmail.com", "Ervin", "Barrows", "464-689-576", "7804423054" },
                    { 48, true, new DateTime(1989, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, null, "Alice.Stiedemann@gmail.com", "Alice", "Stiedemann", "297-459-155", "0080893985" },
                    { 49, true, new DateTime(1953, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 93, null, "Matt_Pfeffer72@yahoo.com", "Matt", "Pfeffer", "039-810-658", "2100180628" },
                    { 50, true, new DateTime(1987, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 94, null, "Shelia.Kiehn90@yahoo.com", "Shelia", "Kiehn", "460-082-443", "5907523455" }
                });

            migrationBuilder.InsertData(
                table: "Przedmioty",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "SkroconaNazwa" },
                values: new object[,]
                {
                    { (byte)1, true, "EdukacjaWczesnoszkolna", null, null },
                    { (byte)2, true, "Informatyka", null, null }
                });

            migrationBuilder.InsertData(
                table: "Przedmioty",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "SkroconaNazwa" },
                values: new object[,]
                {
                    { (byte)3, true, "Matematyka", null, null },
                    { (byte)4, true, "JęzykPolski", null, null },
                    { (byte)5, true, "JęzykAngielski", null, null },
                    { (byte)6, true, "JęzykNiemiecki", null, null },
                    { (byte)7, true, "Fizyka", null, null },
                    { (byte)8, true, "Biologia", null, null },
                    { (byte)9, true, "Chemia", null, null },
                    { (byte)10, true, "Historia", null, null },
                    { (byte)11, true, "Geografia", null, null },
                    { (byte)12, true, "Muzyka", null, null },
                    { (byte)13, true, "Plastyka", null, null },
                    { (byte)14, true, "Technika", null, null },
                    { (byte)15, true, "Przyroda", null, null },
                    { (byte)16, true, "WiedzaOSpołeczeństwie", null, null },
                    { (byte)17, true, "WychowanieFizyczne", null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { (byte)1, true, "Gość", null },
                    { (byte)2, true, "Przeglądający", null },
                    { (byte)3, true, "Edytor", null },
                    { (byte)4, true, "Użytkownik", null },
                    { (byte)5, true, "Administrator", null }
                });

            migrationBuilder.InsertData(
                table: "Stanowiska",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { (byte)1, true, "Obsluga", "Obsługa" },
                    { (byte)2, true, "Konserwator", "Konserwator" },
                    { (byte)3, true, "Pomoc", "Pomoc" },
                    { (byte)4, true, "Pedagog", "Pedagog" },
                    { (byte)5, true, "Kierownik", "Kierownik" },
                    { (byte)6, true, "Dyrektor", "Dyrektor" }
                });

            migrationBuilder.InsertData(
                table: "Ulice",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis" },
                values: new object[,]
                {
                    { 1, true, "Polna", null },
                    { 2, true, "Leśna", null },
                    { 3, true, "Słoneczna", null },
                    { 4, true, "Krótka", null },
                    { 5, true, "Szkolna", null },
                    { 6, true, "Ogrodowa", null },
                    { 7, true, "Lipowa", null },
                    { 8, true, "Brzozowa", null },
                    { 9, true, "Łąkowa", null },
                    { 10, true, "Kwiatowa", null }
                });

            migrationBuilder.InsertData(
                table: "Adresy",
                columns: new[] { "Id", "CzyAktywny", "KodPocztowy", "MiejscowoscId", "NumerDomu", "NumerMieszkania", "PanstwoId", "UlicaId" },
                values: new object[,]
                {
                    { 1, true, "75-619", 19, "199", "177", 4, 5 },
                    { 2, true, "60-899", 3, "144", "81", 1, 10 },
                    { 3, true, "10-600", 8, "80", "7", 6, 1 },
                    { 4, true, "28-764", 2, "93", "71", 1, 8 },
                    { 5, true, "82-731", 5, "169", "69", 2, 7 },
                    { 6, true, "18-097", 13, "126", "135", 7, 6 },
                    { 7, true, "93-431", 21, "27", "172", 6, 6 },
                    { 8, true, "47-102", 7, "104", "153", 7, 2 },
                    { 9, true, "45-172", 8, "189", "28", 7, 3 },
                    { 10, true, "16-593", 17, "171", "152", 4, 8 },
                    { 11, true, "54-982", 17, "55", "60", 4, 4 },
                    { 12, true, "37-693", 16, "148", "186", 4, 10 },
                    { 13, true, "05-375", 6, "24", "144", 4, 8 },
                    { 14, true, "09-965", 4, "133", "157", 7, 2 },
                    { 15, true, "67-094", 12, "119", "173", 1, 3 },
                    { 16, true, "10-252", 2, "125", "2", 4, 6 },
                    { 17, true, "25-398", 2, "54", "84", 8, 3 },
                    { 18, true, "89-120", 17, "80", "58", 3, 10 },
                    { 19, true, "66-566", 12, "71", "35", 3, 5 },
                    { 20, true, "93-916", 16, "90", "126", 8, 5 },
                    { 21, true, "61-131", 10, "14", "9", 1, 1 },
                    { 22, true, "46-807", 21, "100", "178", 2, 10 },
                    { 23, true, "63-573", 1, "72", "172", 5, 7 },
                    { 24, true, "84-335", 1, "121", "189", 4, 10 },
                    { 25, true, "07-951", 11, "93", "85", 5, 3 },
                    { 26, true, "18-724", 10, "91", "21", 3, 9 },
                    { 27, true, "10-350", 14, "7", "142", 3, 4 },
                    { 28, true, "07-198", 8, "114", "31", 8, 6 },
                    { 29, true, "01-458", 10, "116", "200", 7, 10 },
                    { 30, true, "91-172", 17, "177", "51", 1, 2 },
                    { 31, true, "98-320", 9, "105", "52", 1, 4 },
                    { 32, true, "86-706", 13, "79", "119", 3, 8 },
                    { 33, true, "12-632", 11, "146", "197", 1, 9 },
                    { 34, true, "41-430", 13, "138", "158", 8, 5 },
                    { 35, true, "40-724", 17, "88", "102", 2, 2 },
                    { 36, true, "55-650", 8, "16", "36", 3, 2 },
                    { 37, true, "71-061", 5, "18", "37", 7, 1 },
                    { 38, true, "23-425", 6, "2", "106", 6, 5 },
                    { 39, true, "54-352", 10, "175", "188", 2, 4 },
                    { 40, true, "97-127", 11, "46", "56", 8, 9 },
                    { 41, true, "09-158", 20, "27", "148", 3, 2 },
                    { 42, true, "28-116", 2, "2", "188", 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Adresy",
                columns: new[] { "Id", "CzyAktywny", "KodPocztowy", "MiejscowoscId", "NumerDomu", "NumerMieszkania", "PanstwoId", "UlicaId" },
                values: new object[,]
                {
                    { 43, true, "23-275", 17, "66", "51", 3, 6 },
                    { 44, true, "23-219", 5, "89", "50", 5, 2 },
                    { 45, true, "80-829", 7, "127", "19", 5, 9 },
                    { 46, true, "43-885", 13, "115", "102", 2, 10 },
                    { 47, true, "99-977", 15, "196", "69", 6, 5 },
                    { 48, true, "99-924", 12, "48", "16", 8, 1 },
                    { 49, true, "92-697", 11, "148", "87", 1, 10 },
                    { 50, true, "99-821", 12, "122", "52", 7, 4 },
                    { 51, true, "66-745", 3, "116", "12", 8, 2 },
                    { 52, true, "17-654", 9, "90", "82", 5, 6 },
                    { 53, true, "41-391", 7, "200", "171", 6, 1 },
                    { 54, true, "93-368", 4, "23", "104", 1, 7 },
                    { 55, true, "49-920", 12, "77", "52", 3, 2 },
                    { 56, true, "61-176", 5, "175", "82", 2, 3 },
                    { 57, true, "37-494", 5, "69", "100", 4, 8 },
                    { 58, true, "11-674", 8, "143", "21", 7, 10 },
                    { 59, true, "88-778", 5, "181", "191", 4, 2 },
                    { 60, true, "18-789", 6, "5", "80", 7, 7 },
                    { 61, true, "82-572", 11, "157", "55", 6, 10 },
                    { 62, true, "10-836", 10, "63", "44", 4, 4 },
                    { 63, true, "57-939", 1, "9", "111", 4, 8 },
                    { 64, true, "92-460", 9, "168", "153", 3, 8 },
                    { 65, true, "38-382", 7, "94", "86", 5, 1 },
                    { 66, true, "67-927", 21, "41", "84", 1, 4 },
                    { 67, true, "07-552", 6, "155", "175", 2, 3 },
                    { 68, true, "10-261", 13, "59", "21", 8, 3 },
                    { 69, true, "49-541", 8, "24", "183", 6, 2 },
                    { 70, true, "30-253", 7, "185", "191", 2, 2 },
                    { 71, true, "47-002", 7, "102", "50", 6, 8 },
                    { 72, true, "71-184", 11, "15", "1", 6, 4 },
                    { 73, true, "64-237", 4, "86", "28", 1, 2 },
                    { 74, true, "35-684", 17, "52", "96", 2, 7 },
                    { 75, true, "69-191", 19, "40", "99", 6, 5 },
                    { 76, true, "52-335", 19, "25", "57", 2, 4 },
                    { 77, true, "87-056", 10, "61", "10", 1, 8 },
                    { 78, true, "17-994", 10, "87", "167", 2, 3 },
                    { 79, true, "24-599", 7, "34", "121", 6, 4 },
                    { 80, true, "11-116", 5, "59", "76", 2, 3 },
                    { 81, true, "76-921", 19, "60", "195", 8, 6 },
                    { 82, true, "71-577", 6, "184", "172", 7, 5 },
                    { 83, true, "91-906", 5, "127", "90", 4, 2 },
                    { 84, true, "87-018", 12, "59", "113", 1, 8 }
                });

            migrationBuilder.InsertData(
                table: "Adresy",
                columns: new[] { "Id", "CzyAktywny", "KodPocztowy", "MiejscowoscId", "NumerDomu", "NumerMieszkania", "PanstwoId", "UlicaId" },
                values: new object[,]
                {
                    { 85, true, "88-346", 11, "173", "30", 6, 3 },
                    { 86, true, "67-889", 12, "77", "46", 1, 5 },
                    { 87, true, "24-130", 14, "127", "18", 5, 9 },
                    { 88, true, "99-245", 1, "169", "18", 8, 4 },
                    { 89, true, "40-309", 17, "85", "121", 2, 6 },
                    { 90, true, "06-678", 19, "40", "57", 1, 3 },
                    { 91, true, "01-623", 15, "85", "135", 4, 1 },
                    { 92, true, "70-406", 12, "61", "167", 6, 5 },
                    { 93, true, "08-150", 3, "91", "186", 2, 1 },
                    { 94, true, "47-175", 7, "121", "30", 3, 5 },
                    { 95, true, "54-935", 11, "153", "38", 3, 1 },
                    { 96, true, "26-546", 4, "32", "143", 8, 7 },
                    { 97, true, "40-813", 21, "53", "199", 2, 1 },
                    { 98, true, "60-223", 20, "88", "65", 8, 7 },
                    { 99, true, "16-388", 20, "10", "13", 5, 9 },
                    { 100, true, "34-877", 6, "29", "84", 7, 7 },
                    { 101, true, "04-180", 15, "4", "109", 4, 8 },
                    { 102, true, "21-928", 7, "24", "9", 3, 3 },
                    { 103, true, "73-509", 17, "60", "95", 6, 10 },
                    { 104, true, "81-850", 4, "96", "180", 2, 6 },
                    { 105, true, "59-511", 11, "173", "116", 6, 5 },
                    { 106, true, "77-458", 10, "183", "16", 2, 7 },
                    { 107, true, "07-821", 20, "169", "156", 6, 2 },
                    { 108, true, "12-079", 8, "33", "39", 1, 5 },
                    { 109, true, "85-631", 18, "171", "165", 7, 5 },
                    { 110, true, "73-604", 16, "139", "68", 1, 3 },
                    { 111, true, "89-478", 3, "189", "20", 7, 3 },
                    { 112, true, "84-309", 16, "125", "8", 4, 8 },
                    { 113, true, "30-784", 4, "8", "11", 5, 5 },
                    { 114, true, "65-579", 15, "47", "32", 7, 10 },
                    { 115, true, "39-664", 2, "102", "155", 7, 9 },
                    { 116, true, "57-287", 2, "23", "188", 3, 7 },
                    { 117, true, "77-543", 4, "56", "12", 7, 7 },
                    { 118, true, "42-926", 20, "8", "22", 3, 6 },
                    { 119, true, "70-825", 10, "76", "29", 3, 2 },
                    { 120, true, "74-640", 6, "2", "43", 8, 7 },
                    { 121, true, "35-504", 11, "52", "17", 5, 2 },
                    { 122, true, "30-212", 18, "42", "78", 3, 10 },
                    { 123, true, "03-583", 20, "180", "98", 6, 3 },
                    { 124, true, "82-827", 14, "47", "16", 1, 5 },
                    { 125, true, "02-468", 11, "165", "50", 3, 9 },
                    { 126, true, "62-279", 4, "35", "72", 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "Adresy",
                columns: new[] { "Id", "CzyAktywny", "KodPocztowy", "MiejscowoscId", "NumerDomu", "NumerMieszkania", "PanstwoId", "UlicaId" },
                values: new object[,]
                {
                    { 127, true, "63-715", 21, "122", "90", 1, 8 },
                    { 128, true, "10-363", 3, "69", "2", 1, 10 },
                    { 129, true, "81-283", 19, "192", "137", 7, 10 },
                    { 130, true, "37-235", 21, "63", "152", 5, 2 },
                    { 131, true, "52-111", 11, "24", "157", 6, 3 },
                    { 132, true, "10-795", 19, "76", "34", 5, 7 },
                    { 133, true, "10-657", 2, "148", "180", 5, 9 },
                    { 134, true, "26-810", 18, "14", "137", 4, 1 },
                    { 135, true, "73-716", 1, "137", "158", 6, 2 },
                    { 136, true, "66-526", 21, "156", "16", 3, 4 },
                    { 137, true, "78-568", 12, "92", "156", 5, 6 },
                    { 138, true, "49-291", 5, "166", "191", 5, 7 },
                    { 139, true, "45-290", 6, "191", "87", 7, 6 },
                    { 140, true, "04-690", 2, "104", "106", 6, 6 },
                    { 141, true, "03-486", 17, "56", "24", 7, 1 },
                    { 142, true, "20-603", 9, "7", "173", 6, 7 },
                    { 143, true, "46-028", 14, "129", "40", 2, 6 },
                    { 144, true, "02-283", 7, "185", "73", 1, 3 },
                    { 145, true, "24-562", 1, "78", "79", 1, 5 },
                    { 146, true, "11-306", 18, "111", "158", 1, 7 },
                    { 147, true, "31-668", 6, "194", "118", 4, 2 },
                    { 148, true, "12-129", 17, "127", "59", 1, 8 },
                    { 149, true, "16-711", 2, "11", "157", 8, 5 },
                    { 150, true, "78-480", 18, "34", "1", 7, 2 },
                    { 151, true, "20-025", 17, "165", "41", 1, 9 },
                    { 152, true, "38-114", 1, "25", "138", 8, 2 },
                    { 153, true, "16-665", 9, "95", "199", 6, 6 },
                    { 154, true, "12-685", 18, "43", "170", 3, 4 },
                    { 155, true, "87-046", 15, "110", "59", 1, 5 },
                    { 156, true, "72-480", 15, "74", "74", 8, 3 },
                    { 157, true, "06-924", 11, "151", "49", 6, 4 },
                    { 158, true, "98-911", 5, "137", "72", 1, 3 },
                    { 159, true, "65-081", 13, "188", "115", 4, 2 },
                    { 160, true, "53-732", 17, "168", "172", 8, 8 },
                    { 161, true, "74-503", 17, "197", "120", 2, 8 },
                    { 162, true, "03-416", 6, "134", "108", 7, 3 },
                    { 163, true, "24-487", 13, "31", "110", 5, 1 },
                    { 164, true, "68-138", 4, "195", "192", 6, 7 },
                    { 165, true, "65-138", 21, "96", "59", 2, 1 },
                    { 166, true, "56-642", 19, "139", "139", 2, 6 },
                    { 167, true, "65-278", 21, "48", "135", 3, 8 },
                    { 168, true, "56-688", 5, "65", "82", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Adresy",
                columns: new[] { "Id", "CzyAktywny", "KodPocztowy", "MiejscowoscId", "NumerDomu", "NumerMieszkania", "PanstwoId", "UlicaId" },
                values: new object[,]
                {
                    { 169, true, "86-923", 7, "4", "110", 6, 2 },
                    { 170, true, "22-786", 5, "9", "121", 1, 8 },
                    { 171, true, "97-221", 7, "93", "118", 1, 5 },
                    { 172, true, "98-768", 2, "116", "116", 8, 8 },
                    { 173, true, "05-071", 13, "71", "167", 1, 10 },
                    { 174, true, "39-645", 18, "47", "149", 3, 9 },
                    { 175, true, "94-146", 9, "22", "73", 1, 8 },
                    { 176, true, "53-065", 11, "191", "119", 4, 1 },
                    { 177, true, "12-581", 16, "23", "179", 4, 6 },
                    { 178, true, "68-899", 3, "29", "161", 1, 4 },
                    { 179, true, "77-862", 9, "10", "155", 5, 6 },
                    { 180, true, "38-522", 19, "169", "15", 1, 1 },
                    { 181, true, "22-919", 20, "57", "134", 6, 3 },
                    { 182, true, "01-431", 11, "145", "11", 6, 1 },
                    { 183, true, "10-404", 9, "124", "27", 4, 9 },
                    { 184, true, "51-335", 7, "157", "51", 6, 7 },
                    { 185, true, "58-379", 18, "137", "112", 5, 8 },
                    { 186, true, "20-615", 10, "17", "160", 2, 3 },
                    { 187, true, "38-244", 16, "135", "42", 2, 9 },
                    { 188, true, "57-105", 18, "166", "116", 8, 1 },
                    { 189, true, "30-590", 16, "16", "61", 3, 7 },
                    { 190, true, "89-041", 9, "169", "67", 5, 2 },
                    { 191, true, "33-452", 11, "64", "3", 6, 9 },
                    { 192, true, "40-739", 19, "117", "42", 5, 7 },
                    { 193, true, "56-247", 6, "94", "192", 3, 4 },
                    { 194, true, "88-309", 15, "173", "168", 6, 8 },
                    { 195, true, "68-569", 13, "156", "94", 8, 1 },
                    { 196, true, "42-334", 4, "115", "175", 6, 7 },
                    { 197, true, "81-773", 3, "150", "114", 6, 10 },
                    { 198, true, "72-503", 15, "82", "193", 5, 8 },
                    { 199, true, "40-193", 3, "118", "5", 7, 7 },
                    { 200, true, "86-326", 11, "121", "63", 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Oddzialy",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "PracownikId" },
                values: new object[,]
                {
                    { (byte)1, true, "Biedronki", null, 1 },
                    { (byte)2, true, "Misie", null, 2 },
                    { (byte)3, true, "Malinki", null, 3 },
                    { (byte)4, true, "Żabki", null, 4 },
                    { (byte)5, true, "Kotki", null, 5 },
                    { (byte)6, true, "Papużki", null, 6 },
                    { (byte)7, true, "Wróbelki", null, 7 },
                    { (byte)8, true, "Duszki", null, 8 },
                    { (byte)9, true, "Delfinki", null, 9 },
                    { (byte)10, true, "Tygryski", null, 10 }
                });

            migrationBuilder.InsertData(
                table: "Oddzialy",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "PracownikId" },
                values: new object[,]
                {
                    { (byte)11, true, "Jagódki", null, 11 },
                    { (byte)12, true, "PierwszaA", null, 12 },
                    { (byte)13, true, "PierwszaB", null, 13 },
                    { (byte)14, true, "PierwszaC", null, 14 },
                    { (byte)15, true, "DrugaA", null, 15 },
                    { (byte)16, true, "DrugaB", null, 16 },
                    { (byte)17, true, "DrugaC", null, 17 },
                    { (byte)18, true, "TrzeciaA", null, 18 },
                    { (byte)19, true, "TrzeciaB", null, 19 },
                    { (byte)20, true, "TrzeciaC", null, 20 },
                    { (byte)21, true, "CzwartaA", null, 21 },
                    { (byte)22, true, "CzwartaB", null, 22 },
                    { (byte)23, true, "CzwartaC", null, 23 },
                    { (byte)24, true, "PiataA", null, 24 },
                    { (byte)25, true, "PiataB", null, 25 },
                    { (byte)26, true, "PiataC", null, 26 },
                    { (byte)27, true, "SzostaA", null, 27 },
                    { (byte)28, true, "SzostaB", null, 28 },
                    { (byte)29, true, "SzostaC", null, 29 },
                    { (byte)30, true, "SiódmaA", null, 30 },
                    { (byte)31, true, "SiódmaB", null, 31 },
                    { (byte)32, true, "SiódmaC", null, 32 },
                    { (byte)33, true, "ÓsmaA", null, 33 },
                    { (byte)34, true, "ÓsmaB", null, 34 },
                    { (byte)35, true, "ÓsmaC", null, 35 }
                });

            migrationBuilder.InsertData(
                table: "PrzedmiotyPracownicy",
                columns: new[] { "PracownikId", "PrzedmiotId" },
                values: new object[,]
                {
                    { 2, (byte)9 },
                    { 2, (byte)17 },
                    { 3, (byte)3 },
                    { 4, (byte)6 },
                    { 6, (byte)14 },
                    { 8, (byte)14 },
                    { 10, (byte)2 },
                    { 10, (byte)12 },
                    { 10, (byte)16 },
                    { 10, (byte)17 },
                    { 11, (byte)4 },
                    { 11, (byte)9 },
                    { 14, (byte)10 },
                    { 16, (byte)6 },
                    { 17, (byte)12 },
                    { 20, (byte)17 },
                    { 21, (byte)10 }
                });

            migrationBuilder.InsertData(
                table: "PrzedmiotyPracownicy",
                columns: new[] { "PracownikId", "PrzedmiotId" },
                values: new object[,]
                {
                    { 21, (byte)11 },
                    { 22, (byte)7 },
                    { 22, (byte)17 },
                    { 23, (byte)5 },
                    { 23, (byte)9 },
                    { 23, (byte)14 },
                    { 25, (byte)2 },
                    { 25, (byte)15 },
                    { 26, (byte)15 },
                    { 27, (byte)3 },
                    { 27, (byte)13 },
                    { 28, (byte)7 },
                    { 28, (byte)11 },
                    { 28, (byte)13 },
                    { 29, (byte)10 },
                    { 32, (byte)11 },
                    { 33, (byte)15 },
                    { 34, (byte)12 },
                    { 36, (byte)4 },
                    { 38, (byte)2 },
                    { 38, (byte)14 },
                    { 39, (byte)4 },
                    { 39, (byte)8 },
                    { 40, (byte)16 },
                    { 43, (byte)4 },
                    { 43, (byte)12 },
                    { 43, (byte)13 },
                    { 44, (byte)4 },
                    { 47, (byte)15 },
                    { 47, (byte)17 },
                    { 48, (byte)15 }
                });

            migrationBuilder.InsertData(
                table: "Urlopy",
                columns: new[] { "PoczatekUrlopu", "PracownikId", "CzyAktywny", "KoniecUrlopu" },
                values: new object[,]
                {
                    { new DateTime(2022, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, new DateTime(2022, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, true, new DateTime(2022, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, true, new DateTime(2022, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, true, new DateTime(2022, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, true, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, true, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, true, new DateTime(2022, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, true, new DateTime(2022, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Urlopy",
                columns: new[] { "PoczatekUrlopu", "PracownikId", "CzyAktywny", "KoniecUrlopu" },
                values: new object[,]
                {
                    { new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, true, new DateTime(2022, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, true, new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, true, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, true, new DateTime(2022, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, true, new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, true, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, true, new DateTime(2022, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, true, new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, true, new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, true, new DateTime(2022, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, true, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, true, new DateTime(2022, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, true, new DateTime(2022, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, true, new DateTime(2022, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, true, new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, true, new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, true, new DateTime(2022, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, true, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 41, true, new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 42, true, new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 43, true, new DateTime(2022, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 44, true, new DateTime(2022, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 47, true, new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Uzytkownicy",
                columns: new[] { "Id", "CzyAktywny", "Email", "HashHasla", "Imie", "Nazwisko", "RolaId" },
                values: new object[,]
                {
                    { 1, true, "jan@kowalski.mail.com", "AAA", "Jan", "Kowalski", (byte)2 },
                    { 2, true, "roman@nowak.mail.com", "BBB", "Roman", "Nowak", (byte)3 },
                    { 3, true, "adam@wisniewski.mail.com", "CCC", "Adam", "Wiśniewski", (byte)4 }
                });

            migrationBuilder.InsertData(
                table: "Pracodawcy",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "Nazwa", "Regon" },
                values: new object[] { (byte)1, 1, true, "Zespół Szkół nr 1 im. Janusza Korczaka", "012345678" });

            migrationBuilder.InsertData(
                table: "PracownicyAdresy",
                columns: new[] { "AdresId", "PracownikId" },
                values: new object[,]
                {
                    { 18, 1 },
                    { 153, 2 },
                    { 186, 2 },
                    { 87, 3 },
                    { 133, 3 },
                    { 33, 7 },
                    { 119, 7 },
                    { 126, 7 },
                    { 163, 7 },
                    { 72, 8 },
                    { 108, 8 },
                    { 162, 8 },
                    { 19, 9 },
                    { 113, 10 },
                    { 164, 10 },
                    { 157, 12 },
                    { 13, 13 },
                    { 157, 14 },
                    { 14, 15 },
                    { 87, 15 },
                    { 189, 15 },
                    { 34, 16 },
                    { 187, 18 },
                    { 197, 18 },
                    { 60, 20 },
                    { 21, 22 },
                    { 95, 23 },
                    { 48, 24 },
                    { 138, 24 },
                    { 78, 25 },
                    { 82, 26 },
                    { 119, 29 },
                    { 191, 30 },
                    { 86, 32 },
                    { 72, 33 },
                    { 168, 33 },
                    { 188, 34 },
                    { 4, 39 },
                    { 37, 39 },
                    { 46, 40 },
                    { 69, 40 }
                });

            migrationBuilder.InsertData(
                table: "PracownicyAdresy",
                columns: new[] { "AdresId", "PracownikId" },
                values: new object[,]
                {
                    { 132, 44 },
                    { 192, 44 },
                    { 93, 45 },
                    { 188, 45 },
                    { 199, 45 },
                    { 130, 46 },
                    { 72, 47 },
                    { 98, 48 },
                    { 103, 49 }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel" },
                values: new object[,]
                {
                    { 1, 145, true, new DateTime(2015, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfred", "Gulgowski", (byte)25, "8532821970" },
                    { 2, 108, true, new DateTime(2008, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Glenda", "Nitzsche", (byte)23, "0350930201" },
                    { 3, 118, true, new DateTime(2017, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Edgar", "Abshire", (byte)27, "2721701066" },
                    { 4, 147, true, new DateTime(2013, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Stacey", "Veum", (byte)31, "9043444110" },
                    { 5, 123, true, new DateTime(2015, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Calvin", "Oberbrunner", (byte)29, "8192051985" },
                    { 6, 139, true, new DateTime(2007, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Catherine", "Weimann", (byte)23, "5222772134" },
                    { 7, 196, true, new DateTime(2012, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Keith", "Crona", (byte)29, "0681359086" },
                    { 8, 96, true, new DateTime(2008, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elizabeth", "Cole", (byte)27, "5058779437" },
                    { 9, 15, true, new DateTime(2009, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alberta", "Pouros", (byte)3, "6669341338" },
                    { 10, 84, true, new DateTime(2007, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sara", "Wintheiser", (byte)19, "4222152467" },
                    { 11, 116, true, new DateTime(2007, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elbert", "Smitham", (byte)2, "9114531034" },
                    { 12, 85, true, new DateTime(2012, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Regina", "Rice", (byte)16, "8677405838" },
                    { 13, 59, true, new DateTime(2018, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Catherine", "Bogisich", (byte)8, "4598720739" },
                    { 14, 135, true, new DateTime(2008, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eula", "Haley", (byte)30, "2323581878" },
                    { 15, 124, true, new DateTime(2013, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lula", "Wisozk", (byte)32, "2549923325" },
                    { 16, 164, true, new DateTime(2007, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Allen", "Oberbrunner", (byte)19, "5812216077" },
                    { 17, 100, true, new DateTime(2016, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Doreen", "Harris", (byte)4, "1651358560" },
                    { 18, 79, true, new DateTime(2006, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veronica", "Block", (byte)30, "6938755102" },
                    { 19, 162, true, new DateTime(2017, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rudy", "Mueller", (byte)6, "6255084375" },
                    { 20, 183, true, new DateTime(2012, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Amanda", "Dicki", (byte)12, "4676776628" },
                    { 21, 52, true, new DateTime(2010, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nellie", "Rohan", (byte)23, "7383155725" },
                    { 22, 78, true, new DateTime(2018, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phil", "Runte", (byte)12, "9552370891" },
                    { 23, 145, true, new DateTime(2012, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jeannie", "Gleichner", (byte)28, "7800264314" },
                    { 24, 118, true, new DateTime(2006, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brenda", "Denesik", (byte)25, "7136215292" },
                    { 25, 10, true, new DateTime(2008, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Julie", "Schmitt", (byte)4, "7041872386" },
                    { 26, 127, true, new DateTime(2008, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Janet", "Ziemann", (byte)31, "6425685027" },
                    { 27, 63, true, new DateTime(2007, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brian", "Buckridge", (byte)31, "3118065192" },
                    { 28, 39, true, new DateTime(2008, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wilson", "Koepp", (byte)17, "5372458641" },
                    { 29, 34, true, new DateTime(2012, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elijah", "O'Hara", (byte)5, "6415280664" },
                    { 30, 177, true, new DateTime(2012, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Janet", "Weber", (byte)14, "0402055856" },
                    { 31, 124, true, new DateTime(2013, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Christopher", "Johns", (byte)13, "1211364035" },
                    { 32, 22, true, new DateTime(2005, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sharon", "O'Conner", (byte)17, "9937418673" },
                    { 33, 68, true, new DateTime(2012, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Krystal", "Weimann", (byte)23, "3348859335" }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel" },
                values: new object[,]
                {
                    { 34, 7, true, new DateTime(2009, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gayle", "Doyle", (byte)15, "6787165129" },
                    { 35, 91, true, new DateTime(2019, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ashley", "Morar", (byte)21, "9693895989" },
                    { 36, 70, true, new DateTime(2015, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Irene", "Gleason", (byte)30, "1939249454" },
                    { 37, 100, true, new DateTime(2015, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfredo", "Breitenberg", (byte)25, "2403187507" },
                    { 38, 157, true, new DateTime(2016, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Noah", "Simonis", (byte)13, "2166765924" },
                    { 39, 107, true, new DateTime(2013, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anne", "Blick", (byte)21, "4000939509" },
                    { 40, 76, true, new DateTime(2009, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anne", "Waelchi", (byte)26, "3503427265" },
                    { 41, 167, true, new DateTime(2009, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Yolanda", "Champlin", (byte)2, "9034569562" },
                    { 42, 65, true, new DateTime(2013, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lynn", "Bode", (byte)30, "4458863764" },
                    { 43, 54, true, new DateTime(2011, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orlando", "McDermott", (byte)21, "0384176466" },
                    { 44, 108, true, new DateTime(2019, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marianne", "Altenwerth", (byte)14, "5779375208" },
                    { 45, 58, true, new DateTime(2005, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alex", "Wintheiser", (byte)18, "1635802303" },
                    { 46, 59, true, new DateTime(2010, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Peter", "Hane", (byte)20, "2481537960" },
                    { 47, 53, true, new DateTime(2014, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anthony", "Bins", (byte)9, "7236875737" },
                    { 48, 153, true, new DateTime(2007, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Juana", "Weissnat", (byte)12, "4432074018" },
                    { 49, 183, true, new DateTime(2007, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bennie", "Armstrong", (byte)28, "6809950043" },
                    { 50, 15, true, new DateTime(2009, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ivan", "Kris", (byte)22, "0823433326" },
                    { 51, 137, true, new DateTime(2011, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maurice", "Schneider", (byte)14, "9116473419" },
                    { 52, 156, true, new DateTime(2007, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nathaniel", "Medhurst", (byte)2, "0014167340" },
                    { 53, 11, true, new DateTime(2012, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Opal", "Leffler", (byte)13, "8667720500" },
                    { 54, 36, true, new DateTime(2013, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Janis", "Wolf", (byte)8, "5494309803" },
                    { 55, 125, true, new DateTime(2012, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Barbara", "Walter", (byte)11, "6772944199" },
                    { 56, 104, true, new DateTime(2017, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", "Feest", (byte)13, "5346587722" },
                    { 57, 65, true, new DateTime(2017, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dana", "Hansen", (byte)16, "6564871021" },
                    { 58, 61, true, new DateTime(2019, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kathy", "Thompson", (byte)3, "2947085370" },
                    { 59, 123, true, new DateTime(2016, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Colleen", "Huel", (byte)27, "5018239098" },
                    { 60, 21, true, new DateTime(2017, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molly", "Becker", (byte)19, "6223501921" },
                    { 61, 113, true, new DateTime(2016, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dennis", "Cummerata", (byte)9, "3021466862" },
                    { 62, 40, true, new DateTime(2012, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ramon", "Simonis", (byte)17, "4124463679" },
                    { 63, 56, true, new DateTime(2015, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Janice", "Heidenreich", (byte)30, "8372997413" },
                    { 64, 105, true, new DateTime(2017, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emily", "McLaughlin", (byte)24, "5230677971" },
                    { 65, 78, true, new DateTime(2019, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Toby", "Dibbert", (byte)30, "9172541213" },
                    { 66, 110, true, new DateTime(2016, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sally", "Mann", (byte)5, "4400622486" },
                    { 67, 29, true, new DateTime(2011, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mattie", "Hettinger", (byte)30, "5390394016" },
                    { 68, 60, true, new DateTime(2012, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Matthew", "Kunde", (byte)27, "9329900124" },
                    { 69, 27, true, new DateTime(2012, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Muriel", "MacGyver", (byte)20, "0216487093" },
                    { 70, 159, true, new DateTime(2013, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roberto", "Boyer", (byte)28, "7875618819" },
                    { 71, 56, true, new DateTime(2012, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Charles", "Weimann", (byte)28, "3146969611" },
                    { 72, 141, true, new DateTime(2018, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tanya", "Roberts", (byte)20, "7736623949" },
                    { 73, 199, true, new DateTime(2019, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Terence", "Marvin", (byte)17, "8486104090" },
                    { 74, 147, true, new DateTime(2013, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chester", "Howell", (byte)25, "8318934039" },
                    { 75, 192, true, new DateTime(2018, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carmen", "O'Hara", (byte)28, "8176041068" }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel" },
                values: new object[,]
                {
                    { 76, 17, true, new DateTime(2012, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brenda", "Stiedemann", (byte)6, "0799350967" },
                    { 77, 114, true, new DateTime(2010, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joanne", "Murray", (byte)4, "4048831015" },
                    { 78, 111, true, new DateTime(2013, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maxine", "Yost", (byte)23, "7902577813" },
                    { 79, 58, true, new DateTime(2019, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Warren", "Hane", (byte)24, "0469722486" },
                    { 80, 141, true, new DateTime(2014, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joey", "Kihn", (byte)18, "4919660516" },
                    { 81, 158, true, new DateTime(2019, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fernando", "Jenkins", (byte)11, "5018379318" },
                    { 82, 178, true, new DateTime(2013, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mildred", "Breitenberg", (byte)35, "2251092848" },
                    { 83, 33, true, new DateTime(2017, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ginger", "Pouros", (byte)34, "3806913298" },
                    { 84, 116, true, new DateTime(2018, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tony", "Bartell", (byte)13, "8855688631" },
                    { 85, 27, true, new DateTime(2011, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Isabel", "Tremblay", (byte)25, "5055910869" },
                    { 86, 75, true, new DateTime(2018, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cathy", "Purdy", (byte)23, "1560212441" },
                    { 87, 5, true, new DateTime(2015, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pamela", "Doyle", (byte)22, "4416325490" },
                    { 88, 45, true, new DateTime(2016, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jack", "Stark", (byte)5, "9022475394" },
                    { 89, 52, true, new DateTime(2019, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trevor", "Kuhn", (byte)24, "5837576881" },
                    { 90, 105, true, new DateTime(2016, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ivan", "Abshire", (byte)30, "4850756354" },
                    { 91, 103, true, new DateTime(2007, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sara", "Wilderman", (byte)5, "6588074656" },
                    { 92, 163, true, new DateTime(2017, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Yvette", "Doyle", (byte)25, "2441589141" },
                    { 93, 69, true, new DateTime(2010, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shawna", "Conn", (byte)32, "4287241727" },
                    { 94, 62, true, new DateTime(2014, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pamela", "Price", (byte)24, "6578001029" },
                    { 95, 6, true, new DateTime(2016, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Diana", "Pacocha", (byte)13, "7181011166" },
                    { 96, 118, true, new DateTime(2013, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kayla", "Greenfelder", (byte)28, "6218668956" },
                    { 97, 162, true, new DateTime(2006, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorraine", "O'Reilly", (byte)25, "1346338363" },
                    { 98, 53, true, new DateTime(2009, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eloise", "Weber", (byte)23, "3994838694" },
                    { 99, 142, true, new DateTime(2013, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rene", "Maggio", (byte)2, "8268140976" },
                    { 100, 176, true, new DateTime(2006, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Betsy", "Larkin", (byte)13, "1475064298" }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 1L, true, null, new DateTime(2022, 11, 1, 7, 39, 2, 150, DateTimeKind.Local).AddTicks(8799), null, 36, (byte)11, 21, 1.3224047344747955m },
                    { 2L, true, null, new DateTime(2022, 11, 27, 11, 31, 37, 620, DateTimeKind.Local).AddTicks(4992), null, 10, (byte)13, 51, 2.97421061199820m },
                    { 3L, true, null, new DateTime(2022, 12, 6, 4, 27, 43, 889, DateTimeKind.Local).AddTicks(827), null, 28, (byte)6, 89, 5.683763424252980m },
                    { 4L, true, null, new DateTime(2022, 10, 12, 12, 0, 27, 189, DateTimeKind.Local).AddTicks(1578), null, 42, (byte)10, 23, 4.619625218501140m },
                    { 5L, true, null, new DateTime(2022, 12, 29, 8, 52, 2, 641, DateTimeKind.Local).AddTicks(974), null, 23, (byte)6, 66, 3.289330662362895m },
                    { 6L, true, null, new DateTime(2022, 12, 12, 23, 30, 36, 139, DateTimeKind.Local).AddTicks(1104), null, 7, (byte)11, 5, 5.29666342879490m },
                    { 7L, true, null, new DateTime(2022, 10, 11, 15, 38, 22, 911, DateTimeKind.Local).AddTicks(7305), null, 13, (byte)12, 12, 4.304368948240005m },
                    { 8L, true, null, new DateTime(2022, 12, 3, 3, 22, 7, 468, DateTimeKind.Local).AddTicks(7234), null, 31, (byte)5, 4, 4.379544703466605m },
                    { 9L, true, null, new DateTime(2023, 1, 8, 10, 52, 39, 368, DateTimeKind.Local).AddTicks(3038), null, 43, (byte)11, 81, 1.571441878830755m },
                    { 10L, true, null, new DateTime(2022, 12, 28, 13, 52, 19, 378, DateTimeKind.Local).AddTicks(9208), null, 26, (byte)4, 74, 3.611762067587935m },
                    { 11L, true, null, new DateTime(2022, 12, 12, 11, 52, 36, 567, DateTimeKind.Local).AddTicks(4274), null, 21, (byte)3, 1, 1.0677779503482290m },
                    { 12L, true, null, new DateTime(2022, 11, 14, 17, 52, 12, 367, DateTimeKind.Local).AddTicks(3700), null, 3, (byte)3, 91, 5.511636213637720m },
                    { 13L, true, null, new DateTime(2022, 12, 22, 6, 23, 33, 521, DateTimeKind.Local).AddTicks(4224), null, 8, (byte)15, 89, 1.3439475876949485m },
                    { 14L, true, null, new DateTime(2022, 10, 27, 22, 44, 50, 234, DateTimeKind.Local).AddTicks(1680), null, 22, (byte)11, 19, 5.937103856325665m },
                    { 15L, true, null, new DateTime(2023, 1, 6, 1, 28, 45, 650, DateTimeKind.Local).AddTicks(9539), null, 40, (byte)15, 98, 2.497401158091335m },
                    { 16L, true, null, new DateTime(2022, 11, 10, 20, 2, 58, 636, DateTimeKind.Local).AddTicks(9551), null, 33, (byte)4, 3, 2.98275006235705m },
                    { 17L, true, null, new DateTime(2022, 10, 15, 17, 56, 9, 184, DateTimeKind.Local).AddTicks(417), null, 20, (byte)2, 82, 4.446321260857545m },
                    { 18L, true, null, new DateTime(2022, 10, 16, 8, 6, 9, 527, DateTimeKind.Local).AddTicks(330), null, 13, (byte)12, 95, 2.649870060686895m },
                    { 19L, true, null, new DateTime(2022, 12, 17, 16, 8, 7, 164, DateTimeKind.Local).AddTicks(8471), null, 36, (byte)6, 89, 1.3375318112492245m },
                    { 20L, true, null, new DateTime(2022, 11, 12, 4, 56, 3, 856, DateTimeKind.Local).AddTicks(8931), null, 49, (byte)10, 96, 5.405517617429380m },
                    { 21L, true, null, new DateTime(2022, 12, 8, 3, 14, 2, 195, DateTimeKind.Local).AddTicks(3879), null, 17, (byte)5, 10, 4.679909977912860m },
                    { 22L, true, null, new DateTime(2022, 11, 7, 19, 43, 21, 887, DateTimeKind.Local).AddTicks(8111), null, 22, (byte)15, 13, 5.641617652793235m },
                    { 23L, true, null, new DateTime(2022, 12, 24, 10, 13, 19, 39, DateTimeKind.Local).AddTicks(3586), null, 2, (byte)13, 73, 2.27416398668390m },
                    { 24L, true, null, new DateTime(2022, 10, 7, 17, 11, 2, 370, DateTimeKind.Local).AddTicks(1751), null, 17, (byte)3, 79, 1.802071742155620m },
                    { 25L, true, null, new DateTime(2023, 1, 6, 8, 19, 27, 484, DateTimeKind.Local).AddTicks(7434), null, 25, (byte)6, 50, 1.4247420981641590m },
                    { 26L, true, null, new DateTime(2023, 1, 3, 6, 23, 35, 578, DateTimeKind.Local).AddTicks(8171), null, 46, (byte)14, 64, 1.1017741440337960m },
                    { 27L, true, null, new DateTime(2022, 12, 13, 23, 23, 48, 185, DateTimeKind.Local).AddTicks(3511), null, 35, (byte)6, 46, 4.813787800638840m },
                    { 28L, true, null, new DateTime(2022, 11, 28, 15, 24, 15, 112, DateTimeKind.Local).AddTicks(8815), null, 16, (byte)7, 57, 2.573816370951860m },
                    { 29L, true, null, new DateTime(2022, 10, 18, 15, 25, 45, 248, DateTimeKind.Local).AddTicks(2630), null, 6, (byte)13, 7, 4.261716844635885m },
                    { 30L, true, null, new DateTime(2022, 10, 12, 1, 34, 58, 151, DateTimeKind.Local).AddTicks(1642), null, 50, (byte)2, 15, 5.279585280120180m },
                    { 31L, true, null, new DateTime(2022, 12, 30, 13, 27, 30, 805, DateTimeKind.Local).AddTicks(5138), null, 10, (byte)7, 80, 5.462075328669545m },
                    { 32L, true, null, new DateTime(2022, 11, 28, 19, 40, 29, 485, DateTimeKind.Local).AddTicks(3505), null, 42, (byte)3, 78, 4.268010000823070m },
                    { 33L, true, null, new DateTime(2022, 10, 8, 18, 46, 2, 687, DateTimeKind.Local).AddTicks(4198), null, 41, (byte)15, 33, 5.075694581994645m },
                    { 34L, true, null, new DateTime(2022, 10, 15, 19, 25, 36, 123, DateTimeKind.Local).AddTicks(3930), null, 29, (byte)17, 42, 5.693664877998485m },
                    { 35L, true, null, new DateTime(2022, 10, 19, 7, 45, 3, 988, DateTimeKind.Local).AddTicks(1336), null, 39, (byte)15, 42, 4.454921549863610m },
                    { 36L, true, null, new DateTime(2023, 1, 4, 16, 41, 18, 868, DateTimeKind.Local).AddTicks(1891), null, 21, (byte)12, 74, 2.027644067549910m },
                    { 37L, true, null, new DateTime(2022, 10, 14, 10, 40, 29, 30, DateTimeKind.Local).AddTicks(289), null, 39, (byte)17, 55, 1.375829413708220m },
                    { 38L, true, null, new DateTime(2022, 11, 27, 15, 47, 52, 435, DateTimeKind.Local).AddTicks(6142), null, 46, (byte)7, 5, 4.062584189727245m },
                    { 39L, true, null, new DateTime(2022, 11, 8, 22, 34, 1, 601, DateTimeKind.Local).AddTicks(947), null, 27, (byte)13, 41, 5.235563384944370m },
                    { 40L, true, null, new DateTime(2022, 12, 14, 7, 6, 44, 192, DateTimeKind.Local).AddTicks(4509), null, 40, (byte)17, 22, 4.371573350565310m },
                    { 41L, true, null, new DateTime(2022, 12, 27, 15, 36, 49, 827, DateTimeKind.Local).AddTicks(6681), null, 29, (byte)3, 17, 5.740815302236385m },
                    { 42L, true, null, new DateTime(2022, 11, 1, 23, 43, 59, 624, DateTimeKind.Local).AddTicks(1592), null, 39, (byte)13, 96, 1.098981198435175m }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 43L, true, null, new DateTime(2022, 11, 5, 3, 52, 59, 706, DateTimeKind.Local).AddTicks(4398), null, 6, (byte)17, 12, 3.77353559284170m },
                    { 44L, true, null, new DateTime(2023, 1, 7, 6, 17, 29, 514, DateTimeKind.Local).AddTicks(1380), null, 13, (byte)14, 37, 5.468691332483985m },
                    { 45L, true, null, new DateTime(2022, 12, 7, 8, 16, 2, 111, DateTimeKind.Local).AddTicks(1308), null, 26, (byte)7, 1, 3.297334895607705m },
                    { 46L, true, null, new DateTime(2023, 1, 6, 19, 55, 43, 226, DateTimeKind.Local).AddTicks(8368), null, 18, (byte)17, 70, 1.122694093791160m },
                    { 47L, true, null, new DateTime(2022, 11, 22, 4, 41, 13, 168, DateTimeKind.Local).AddTicks(9708), null, 10, (byte)17, 79, 5.463219037029530m },
                    { 48L, true, null, new DateTime(2022, 12, 31, 7, 53, 33, 620, DateTimeKind.Local).AddTicks(419), null, 31, (byte)1, 40, 5.479115977640785m },
                    { 49L, true, null, new DateTime(2022, 12, 7, 22, 9, 57, 233, DateTimeKind.Local).AddTicks(526), null, 48, (byte)7, 34, 5.382786145146370m },
                    { 50L, true, null, new DateTime(2022, 12, 4, 10, 28, 40, 955, DateTimeKind.Local).AddTicks(1639), null, 21, (byte)3, 46, 1.714411221311620m },
                    { 51L, true, null, new DateTime(2022, 10, 26, 23, 39, 0, 506, DateTimeKind.Local).AddTicks(1899), null, 22, (byte)1, 44, 4.005438436756580m },
                    { 52L, true, null, new DateTime(2022, 10, 24, 6, 15, 17, 248, DateTimeKind.Local).AddTicks(7316), null, 28, (byte)12, 80, 3.828796474649010m },
                    { 53L, true, null, new DateTime(2022, 11, 21, 22, 11, 24, 1, DateTimeKind.Local).AddTicks(9336), null, 4, (byte)12, 96, 5.711256997991005m },
                    { 54L, true, null, new DateTime(2023, 1, 4, 9, 55, 45, 996, DateTimeKind.Local).AddTicks(7839), null, 16, (byte)2, 13, 1.1040578866862030m },
                    { 55L, true, null, new DateTime(2023, 1, 2, 16, 8, 38, 226, DateTimeKind.Local).AddTicks(8466), null, 43, (byte)16, 37, 4.755892128104295m },
                    { 56L, true, null, new DateTime(2022, 10, 9, 5, 54, 8, 786, DateTimeKind.Local).AddTicks(7009), null, 10, (byte)9, 62, 1.4142715783856210m },
                    { 57L, true, null, new DateTime(2022, 11, 10, 9, 21, 1, 573, DateTimeKind.Local).AddTicks(7770), null, 45, (byte)15, 55, 4.065281078715475m },
                    { 58L, true, null, new DateTime(2022, 10, 8, 19, 22, 13, 3, DateTimeKind.Local).AddTicks(6106), null, 39, (byte)7, 74, 1.4832478917591495m },
                    { 59L, true, null, new DateTime(2023, 1, 8, 6, 58, 15, 682, DateTimeKind.Local).AddTicks(8829), null, 41, (byte)8, 71, 1.3629045213399940m },
                    { 60L, true, null, new DateTime(2023, 1, 8, 7, 4, 48, 185, DateTimeKind.Local).AddTicks(2826), null, 1, (byte)8, 65, 2.699773041391640m },
                    { 61L, true, null, new DateTime(2022, 11, 20, 1, 13, 52, 572, DateTimeKind.Local).AddTicks(3847), null, 15, (byte)3, 40, 1.2058552905013110m },
                    { 62L, true, null, new DateTime(2022, 11, 14, 7, 10, 43, 897, DateTimeKind.Local).AddTicks(9024), null, 3, (byte)6, 56, 3.409829398342330m },
                    { 63L, true, null, new DateTime(2022, 10, 14, 4, 15, 46, 519, DateTimeKind.Local).AddTicks(9961), null, 47, (byte)11, 30, 3.961850954667595m },
                    { 64L, true, null, new DateTime(2022, 10, 29, 6, 51, 44, 401, DateTimeKind.Local).AddTicks(496), null, 27, (byte)2, 10, 2.451494803862410m },
                    { 65L, true, null, new DateTime(2022, 12, 16, 13, 50, 36, 14, DateTimeKind.Local).AddTicks(6579), null, 29, (byte)17, 75, 1.2151703625988075m },
                    { 66L, true, null, new DateTime(2023, 1, 2, 17, 6, 11, 155, DateTimeKind.Local).AddTicks(2382), null, 8, (byte)14, 93, 4.441172821187030m },
                    { 67L, true, null, new DateTime(2022, 11, 6, 7, 8, 9, 184, DateTimeKind.Local).AddTicks(4500), null, 28, (byte)3, 18, 1.0997794140594915m },
                    { 68L, true, null, new DateTime(2022, 11, 16, 8, 3, 31, 674, DateTimeKind.Local).AddTicks(1774), null, 26, (byte)14, 41, 1.2886039532202315m },
                    { 69L, true, null, new DateTime(2023, 1, 10, 12, 16, 41, 477, DateTimeKind.Local).AddTicks(7350), null, 11, (byte)16, 44, 3.418961579640845m },
                    { 70L, true, null, new DateTime(2022, 11, 23, 5, 32, 12, 48, DateTimeKind.Local).AddTicks(1496), null, 26, (byte)15, 11, 1.742543337746775m },
                    { 71L, true, null, new DateTime(2022, 11, 26, 22, 8, 52, 277, DateTimeKind.Local).AddTicks(3908), null, 46, (byte)3, 57, 3.834827298221565m },
                    { 72L, true, null, new DateTime(2022, 12, 4, 18, 12, 1, 150, DateTimeKind.Local).AddTicks(4450), null, 27, (byte)2, 44, 4.755453305205075m },
                    { 73L, true, null, new DateTime(2023, 1, 7, 8, 56, 28, 681, DateTimeKind.Local).AddTicks(9430), null, 7, (byte)11, 75, 2.816218582362040m },
                    { 74L, true, null, new DateTime(2022, 12, 3, 10, 3, 1, 251, DateTimeKind.Local).AddTicks(1188), null, 39, (byte)10, 15, 5.497475109294745m },
                    { 75L, true, null, new DateTime(2022, 11, 19, 9, 48, 28, 874, DateTimeKind.Local).AddTicks(3600), null, 7, (byte)1, 29, 4.4890416955990m },
                    { 76L, true, null, new DateTime(2022, 10, 28, 23, 55, 2, 959, DateTimeKind.Local).AddTicks(5280), null, 7, (byte)9, 84, 1.01150764991133830m },
                    { 77L, true, null, new DateTime(2022, 12, 23, 19, 1, 48, 38, DateTimeKind.Local).AddTicks(5828), null, 3, (byte)12, 9, 1.823465299710380m },
                    { 78L, true, null, new DateTime(2022, 12, 30, 4, 9, 38, 720, DateTimeKind.Local).AddTicks(144), null, 49, (byte)10, 2, 3.666678506725785m },
                    { 79L, true, null, new DateTime(2022, 11, 29, 10, 16, 25, 865, DateTimeKind.Local).AddTicks(727), null, 4, (byte)2, 31, 4.247279014087880m },
                    { 80L, true, null, new DateTime(2022, 12, 18, 1, 32, 49, 604, DateTimeKind.Local).AddTicks(4440), null, 24, (byte)10, 83, 3.058347017531445m },
                    { 81L, true, null, new DateTime(2022, 12, 6, 23, 16, 7, 517, DateTimeKind.Local).AddTicks(2473), null, 19, (byte)17, 52, 4.634239290204940m },
                    { 82L, true, null, new DateTime(2022, 12, 10, 20, 8, 54, 897, DateTimeKind.Local).AddTicks(1004), null, 31, (byte)8, 55, 1.02601840301697070m },
                    { 83L, true, null, new DateTime(2022, 12, 9, 5, 54, 30, 99, DateTimeKind.Local).AddTicks(5835), null, 36, (byte)2, 96, 5.876039135212095m },
                    { 84L, true, null, new DateTime(2023, 1, 5, 0, 50, 43, 468, DateTimeKind.Local).AddTicks(6468), null, 3, (byte)8, 8, 1.726622166450425m }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 85L, true, null, new DateTime(2022, 12, 10, 8, 4, 12, 694, DateTimeKind.Local).AddTicks(7956), null, 38, (byte)1, 56, 4.726901078469540m },
                    { 86L, true, null, new DateTime(2023, 1, 5, 8, 53, 26, 461, DateTimeKind.Local).AddTicks(8396), null, 42, (byte)7, 23, 2.181203281125615m },
                    { 87L, true, null, new DateTime(2022, 12, 5, 0, 33, 12, 215, DateTimeKind.Local).AddTicks(3329), null, 16, (byte)2, 30, 4.635488529054210m },
                    { 88L, true, null, new DateTime(2022, 11, 15, 4, 58, 30, 674, DateTimeKind.Local).AddTicks(1620), null, 27, (byte)16, 14, 4.892699600613070m },
                    { 89L, true, null, new DateTime(2022, 11, 30, 8, 27, 7, 608, DateTimeKind.Local).AddTicks(6916), null, 22, (byte)10, 56, 1.929412448745880m },
                    { 90L, true, null, new DateTime(2022, 12, 4, 4, 31, 38, 33, DateTimeKind.Local).AddTicks(8282), null, 6, (byte)2, 22, 2.430345827914005m },
                    { 91L, true, null, new DateTime(2022, 11, 23, 4, 35, 45, 375, DateTimeKind.Local).AddTicks(7600), null, 20, (byte)7, 38, 2.580866012527080m },
                    { 92L, true, null, new DateTime(2022, 11, 20, 0, 33, 52, 505, DateTimeKind.Local).AddTicks(72), null, 41, (byte)4, 47, 2.871809920702040m },
                    { 93L, true, null, new DateTime(2023, 1, 11, 6, 5, 54, 797, DateTimeKind.Local).AddTicks(1200), null, 20, (byte)4, 18, 5.565298389441005m },
                    { 94L, true, null, new DateTime(2022, 10, 23, 4, 59, 4, 2, DateTimeKind.Local).AddTicks(5405), null, 21, (byte)7, 89, 3.847949766017475m },
                    { 95L, true, null, new DateTime(2022, 11, 9, 8, 16, 9, 237, DateTimeKind.Local).AddTicks(1804), null, 46, (byte)12, 15, 3.707744868336125m },
                    { 96L, true, null, new DateTime(2022, 12, 21, 21, 40, 26, 494, DateTimeKind.Local).AddTicks(1035), null, 10, (byte)15, 98, 2.465267376725220m },
                    { 97L, true, null, new DateTime(2022, 11, 5, 9, 33, 11, 315, DateTimeKind.Local).AddTicks(8383), null, 31, (byte)2, 14, 5.111392742074745m },
                    { 98L, true, null, new DateTime(2022, 11, 16, 7, 26, 7, 594, DateTimeKind.Local).AddTicks(6860), null, 47, (byte)13, 90, 2.015821127694015m },
                    { 99L, true, null, new DateTime(2022, 12, 24, 3, 22, 17, 547, DateTimeKind.Local).AddTicks(8638), null, 4, (byte)10, 94, 2.114633547195530m },
                    { 100L, true, null, new DateTime(2023, 1, 9, 23, 42, 0, 355, DateTimeKind.Local).AddTicks(4590), null, 14, (byte)12, 34, 1.762875499559045m }
                });

            migrationBuilder.InsertData(
                table: "Umowy",
                columns: new[] { "Id", "CzyAktywny", "CzyZwolnionyOdPodatku", "DataRozpoczeciaPracy", "DataZakonczeniaPracy", "DataZawarciaUmowy", "EtatId", "InneWarunkiZatrudnienia", "MiejsceWykonywaniaPracy", "OkresPracy", "OpisWynagrodzenia", "PracodawcaId", "PracownikId", "PrzyczynyZawarciaUmowy", "StanowiskoId", "WymiarCzasuPracy", "WymiarGodzinowy", "WynagrodzenieBrutto" },
                values: new object[,]
                {
                    { 1, true, false, new DateTime(2021, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 1, null, (byte)5, "Inny", 40.0, 5305.73m },
                    { 2, true, false, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 2, null, (byte)5, "Inny", 24.0, 6812.92m },
                    { 3, true, false, new DateTime(2022, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas określony", null, (byte)1, 3, null, (byte)4, "Inny", 22.0, 9161.52m },
                    { 4, true, false, new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 4, null, (byte)3, "Pełen etat", 17.0, 7983.36m },
                    { 5, true, false, new DateTime(2014, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2014, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 5, null, (byte)3, "Inny", 18.0, 3588.61m },
                    { 6, true, false, new DateTime(2021, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 6, null, (byte)3, "Inny", 36.0, 7947.31m },
                    { 7, true, false, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 7, null, (byte)6, "Pełen etat", 33.0, 4266.92m },
                    { 8, true, false, new DateTime(2017, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2017, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 8, null, (byte)6, "Ćwierć etatu", 34.0, 4794.66m },
                    { 9, true, false, new DateTime(2015, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2015, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 9, null, (byte)1, "Inny", 16.0, 9905.47m },
                    { 10, true, false, new DateTime(2019, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 10, null, (byte)3, "Pełen etat", 36.0, 7127.75m },
                    { 11, true, false, new DateTime(2022, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 11, null, (byte)1, "Ćwierć etatu", 36.0, 5580.56m },
                    { 12, true, false, new DateTime(2015, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2015, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 12, null, (byte)5, "Inny", 24.0, 7707.31m },
                    { 13, true, false, new DateTime(2018, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2018, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 13, null, (byte)4, "Inny", 26.0, 9850.80m },
                    { 14, true, false, new DateTime(2017, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2017, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 14, null, (byte)5, "Ćwierć etatu", 23.0, 9244.52m },
                    { 15, true, false, new DateTime(2020, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 15, null, (byte)5, "Ćwierć etatu", 37.0, 9429.46m },
                    { 16, true, false, new DateTime(2013, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 16, null, (byte)5, "Inny", 25.0, 9620.69m },
                    { 17, true, false, new DateTime(2016, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 17, null, (byte)6, "Pełen etat", 18.0, 3453.69m },
                    { 18, true, false, new DateTime(2013, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 18, null, (byte)3, "Pół etatu", 20.0, 6371.00m },
                    { 19, true, false, new DateTime(2018, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2018, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 19, null, (byte)1, "Ćwierć etatu", 26.0, 8440.53m },
                    { 20, true, false, new DateTime(2018, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2018, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 20, null, (byte)5, "Ćwierć etatu", 32.0, 6550.47m },
                    { 21, true, false, new DateTime(2018, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2018, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 21, null, (byte)2, "Pół etatu", 16.0, 9389.72m },
                    { 22, true, false, new DateTime(2015, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2015, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 22, null, (byte)2, "Inny", 16.0, 9352.62m },
                    { 23, true, false, new DateTime(2016, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 23, null, (byte)3, "Pół etatu", 33.0, 3026.44m },
                    { 24, true, false, new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas określony", null, (byte)1, 24, null, (byte)6, "Pełen etat", 15.0, 8449.40m },
                    { 25, true, false, new DateTime(2018, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2018, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 25, null, (byte)1, "Ćwierć etatu", 22.0, 7635.00m },
                    { 26, true, false, new DateTime(2021, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 26, null, (byte)3, "Pół etatu", 36.0, 4936.91m }
                });

            migrationBuilder.InsertData(
                table: "Umowy",
                columns: new[] { "Id", "CzyAktywny", "CzyZwolnionyOdPodatku", "DataRozpoczeciaPracy", "DataZakonczeniaPracy", "DataZawarciaUmowy", "EtatId", "InneWarunkiZatrudnienia", "MiejsceWykonywaniaPracy", "OkresPracy", "OpisWynagrodzenia", "PracodawcaId", "PracownikId", "PrzyczynyZawarciaUmowy", "StanowiskoId", "WymiarCzasuPracy", "WymiarGodzinowy", "WynagrodzenieBrutto" },
                values: new object[,]
                {
                    { 27, true, false, new DateTime(2014, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2014, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 27, null, (byte)3, "Inny", 31.0, 6886.85m },
                    { 28, true, false, new DateTime(2020, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 28, null, (byte)5, "Inny", 32.0, 9614.16m },
                    { 29, true, false, new DateTime(2013, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 29, null, (byte)3, "Ćwierć etatu", 37.0, 5567.90m },
                    { 30, true, false, new DateTime(2017, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 30, null, (byte)1, "Pół etatu", 40.0, 8542.03m },
                    { 31, true, false, new DateTime(2013, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 31, null, (byte)2, "Ćwierć etatu", 36.0, 8997.13m },
                    { 32, true, false, new DateTime(2015, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2015, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 32, null, (byte)6, "Ćwierć etatu", 17.0, 3226.19m },
                    { 33, true, false, new DateTime(2017, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2017, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 33, null, (byte)1, "Pełen etat", 38.0, 5697.87m },
                    { 34, true, false, new DateTime(2016, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 34, null, (byte)3, "Pół etatu", 22.0, 3702.63m },
                    { 35, true, false, new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 35, null, (byte)2, "Pełen etat", 31.0, 7236.20m },
                    { 36, true, false, new DateTime(2016, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 36, null, (byte)2, "Pełen etat", 28.0, 9301.31m },
                    { 37, true, false, new DateTime(2021, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas określony", null, (byte)1, 37, null, (byte)6, "Ćwierć etatu", 18.0, 8720.00m },
                    { 38, true, false, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 38, null, (byte)5, "Inny", 40.0, 9856.27m },
                    { 39, true, false, new DateTime(2013, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 39, null, (byte)2, "Ćwierć etatu", 39.0, 6671.97m },
                    { 40, true, false, new DateTime(2013, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 40, null, (byte)4, "Inny", 31.0, 7509.89m },
                    { 41, true, false, new DateTime(2013, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2013, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 41, null, (byte)6, "Ćwierć etatu", 15.0, 8001.91m },
                    { 42, true, false, new DateTime(2016, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 42, null, (byte)4, "Ćwierć etatu", 16.0, 6365.54m },
                    { 43, true, false, new DateTime(2015, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2015, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 43, null, (byte)6, "Ćwierć etatu", 32.0, 5074.41m },
                    { 44, true, false, new DateTime(2016, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas określony", null, (byte)1, 44, null, (byte)6, "Pełen etat", 37.0, 4413.85m },
                    { 45, true, false, new DateTime(2020, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 45, null, (byte)5, "Pół etatu", 30.0, 9493.11m },
                    { 46, true, false, new DateTime(2020, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "czas określony", null, (byte)1, 46, null, (byte)5, "Pełen etat", 18.0, 5505.36m },
                    { 47, true, false, new DateTime(2019, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, null, " 199/177, 75-619", "czas określony", null, (byte)1, 47, null, (byte)5, "Inny", 34.0, 6082.00m },
                    { 48, true, false, new DateTime(2016, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2016, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 48, null, (byte)4, "Pół etatu", 15.0, 7031.80m },
                    { 49, true, false, new DateTime(2022, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, " 199/177, 75-619", "okres próbny", null, (byte)1, 49, null, (byte)3, "Ćwierć etatu", 27.0, 6481.78m },
                    { 50, true, false, new DateTime(2020, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, null, " 199/177, 75-619", "czas nieokreślony", null, (byte)1, 50, null, (byte)3, "Inny", 39.0, 4311.30m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresy_MiejscowoscId",
                table: "Adresy",
                column: "MiejscowoscId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresy_PanstwoId",
                table: "Adresy",
                column: "PanstwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresy_UlicaId",
                table: "Adresy",
                column: "UlicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Migawki_UzytkownikId",
                table: "Migawki",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_PracownikId",
                table: "Oceny",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_PrzedmiotId",
                table: "Oceny",
                column: "PrzedmiotId");

            migrationBuilder.CreateIndex(
                name: "IX_Oceny_UczenId",
                table: "Oceny",
                column: "UczenId");

            migrationBuilder.CreateIndex(
                name: "IX_Oddzialy_PracownikId",
                table: "Oddzialy",
                column: "PracownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pracodawcy_AdresId",
                table: "Pracodawcy",
                column: "AdresId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracownicyAdresy_AdresId",
                table: "PracownicyAdresy",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzedmiotyPracownicy_PrzedmiotId",
                table: "PrzedmiotyPracownicy",
                column: "PrzedmiotId");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_AdresId",
                table: "Uczniowie",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_OddzialId",
                table: "Uczniowie",
                column: "OddzialId");

            migrationBuilder.CreateIndex(
                name: "IX_Umowy_EtatId",
                table: "Umowy",
                column: "EtatId");

            migrationBuilder.CreateIndex(
                name: "IX_Umowy_PracodawcaId",
                table: "Umowy",
                column: "PracodawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Umowy_PracownikId",
                table: "Umowy",
                column: "PracownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Umowy_StanowiskoId",
                table: "Umowy",
                column: "StanowiskoId");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_RolaId",
                table: "Uzytkownicy",
                column: "RolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Migawki");

            migrationBuilder.DropTable(
                name: "Oceny");

            migrationBuilder.DropTable(
                name: "PracownicyAdresy");

            migrationBuilder.DropTable(
                name: "PrzedmiotyPracownicy");

            migrationBuilder.DropTable(
                name: "Umowy");

            migrationBuilder.DropTable(
                name: "Urlopy");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Przedmioty");

            migrationBuilder.DropTable(
                name: "Etaty");

            migrationBuilder.DropTable(
                name: "Pracodawcy");

            migrationBuilder.DropTable(
                name: "Stanowiska");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Oddzialy");

            migrationBuilder.DropTable(
                name: "Adresy");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Miejscowosci");

            migrationBuilder.DropTable(
                name: "Panstwa");

            migrationBuilder.DropTable(
                name: "Ulice");
        }
    }
}
