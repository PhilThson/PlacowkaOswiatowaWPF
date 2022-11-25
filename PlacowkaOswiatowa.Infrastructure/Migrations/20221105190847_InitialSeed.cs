using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlacowkaOswiatowa.Infrastructure.Migrations
{
    public partial class InitialSeed : Migration
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
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pensja = table.Column<decimal>(type: "money", precision: 5, scale: 2, nullable: false),
                    DniUrlopu = table.Column<int>(type: "int", nullable: true),
                    WymiarGodzinowy = table.Column<double>(type: "float", nullable: true),
                    Nadgodziny = table.Column<double>(type: "float", nullable: true),
                    NrTelefonu = table.Column<string>(type: "varchar(11)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EtatId = table.Column<byte>(type: "tinyint", nullable: false),
                    StanowiskoId = table.Column<byte>(type: "tinyint", nullable: false),
                    DataZatrudnienia = table.Column<DateTime>(type: "date", nullable: false),
                    DataKoncaZatrudnienia = table.Column<DateTime>(type: "date", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Pracownicy_Etaty_EtatId",
                        column: x => x.EtatId,
                        principalTable: "Etaty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Stanowiska_StanowiskoId",
                        column: x => x.StanowiskoId,
                        principalTable: "Stanowiska",
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
                    DataZmiany = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    WychowawcaId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Uczniowie_Pracownicy_WychowawcaId",
                        column: x => x.WychowawcaId,
                        principalTable: "Pracownicy",
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
                    { (byte)1, true, "PracownikAdministracyjny", null },
                    { (byte)2, true, "PracownikPedagogiczny", null },
                    { (byte)3, true, "PracownikObslugi", null }
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
                table: "Przedmioty",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "SkroconaNazwa" },
                values: new object[,]
                {
                    { (byte)1, true, "EdukacjaWczesnoszkolna", null, null },
                    { (byte)2, true, "Informatyka", null, null },
                    { (byte)3, true, "Matematyka", null, null },
                    { (byte)4, true, "JęzykPolski", null, null },
                    { (byte)5, true, "JęzykAngielski", null, null },
                    { (byte)6, true, "JęzykNiemiecki", null, null },
                    { (byte)7, true, "Fizyka", null, null },
                    { (byte)8, true, "Biologia", null, null },
                    { (byte)9, true, "Chemia", null, null },
                    { (byte)10, true, "Historia", null, null }
                });

            migrationBuilder.InsertData(
                table: "Przedmioty",
                columns: new[] { "Id", "CzyAktywny", "Nazwa", "Opis", "SkroconaNazwa" },
                values: new object[,]
                {
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
                    { (byte)1, true, "Obsługa", null },
                    { (byte)2, true, "Konserwator", null },
                    { (byte)3, true, "Pomoc", null },
                    { (byte)4, true, "Pedagog", null },
                    { (byte)5, true, "Kierownik", null },
                    { (byte)6, true, "Dyrektor", null }
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
                table: "Pracownicy",
                columns: new[] { "Id", "CzyAktywny", "DataKoncaZatrudnienia", "DataUrodzenia", "DataZatrudnienia", "DniUrlopu", "DrugieImie", "Email", "EtatId", "Imie", "Nadgodziny", "Nazwisko", "NrTelefonu", "Pensja", "Pesel", "StanowiskoId", "WymiarGodzinowy" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(1970, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 53, null, "Monica_Reichert@hotmail.com", (byte)1, "Monica", null, "Reichert", "060-078-495", 9061.42m, "6835611161", (byte)3, 36.0 },
                    { 2, true, null, new DateTime(1958, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 53, null, "Ismael_Medhurst88@gmail.com", (byte)1, "Ismael", null, "Medhurst", "346-687-257", 5439.30m, "3899355605", (byte)1, 32.0 },
                    { 3, true, null, new DateTime(1999, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, null, "Kristopher.Balistreri0@yahoo.com", (byte)1, "Kristopher", null, "Balistreri", "675-171-769", 6651.89m, "0457960031", (byte)5, 28.0 },
                    { 4, true, null, new DateTime(1955, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null, "Darnell_Pfannerstill@hotmail.com", (byte)3, "Darnell", null, "Pfannerstill", "260-673-498", 3495.17m, "8664682891", (byte)3, 30.0 },
                    { 5, true, null, new DateTime(1998, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, "Jean88@yahoo.com", (byte)2, "Jean", null, "McGlynn", "629-180-480", 6835.11m, "2899604104", (byte)1, 17.0 },
                    { 6, true, null, new DateTime(1961, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 77, null, "Anthony_Stanton@yahoo.com", (byte)1, "Anthony", null, "Stanton", "353-962-581", 3959.81m, "1319237163", (byte)3, 21.0 },
                    { 7, true, null, new DateTime(1965, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 58, null, "Corey_Jenkins39@gmail.com", (byte)1, "Corey", null, "Jenkins", "180-511-284", 4867.24m, "1344952667", (byte)6, 31.0 },
                    { 8, true, null, new DateTime(1956, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null, "Danielle87@gmail.com", (byte)2, "Danielle", null, "Kulas", "455-721-018", 6886.34m, "4248992256", (byte)5, 40.0 },
                    { 9, true, null, new DateTime(1945, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 91, null, "Brad_Farrell@yahoo.com", (byte)1, "Brad", null, "Farrell", "185-990-380", 5363.10m, "7551391069", (byte)1, 19.0 },
                    { 10, true, null, new DateTime(1984, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, "Sheldon.Stark@gmail.com", (byte)3, "Sheldon", null, "Stark", "299-750-705", 7797.94m, "7632172966", (byte)4, 16.0 }
                });

            migrationBuilder.InsertData(
                table: "Pracownicy",
                columns: new[] { "Id", "CzyAktywny", "DataKoncaZatrudnienia", "DataUrodzenia", "DataZatrudnienia", "DniUrlopu", "DrugieImie", "Email", "EtatId", "Imie", "Nadgodziny", "Nazwisko", "NrTelefonu", "Pensja", "Pesel", "StanowiskoId", "WymiarGodzinowy" },
                values: new object[,]
                {
                    { 11, true, null, new DateTime(1965, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, null, "Nellie.Heller@gmail.com", (byte)2, "Nellie", null, "Heller", "275-725-449", 9564.00m, "8146640264", (byte)6, 32.0 },
                    { 12, true, null, new DateTime(1943, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 99, null, "Preston.Weber42@hotmail.com", (byte)2, "Preston", null, "Weber", "220-445-934", 8790.57m, "6141675991", (byte)3, 29.0 },
                    { 13, true, null, new DateTime(1985, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, "Seth_Weber56@gmail.com", (byte)3, "Seth", null, "Weber", "795-043-535", 6109.21m, "9628527591", (byte)1, 15.0 },
                    { 14, true, null, new DateTime(1962, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null, "Joy44@hotmail.com", (byte)2, "Joy", null, "Rohan", "677-320-042", 3612.97m, "7772936882", (byte)3, 32.0 },
                    { 15, true, null, new DateTime(1952, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 61, null, "Jimmie50@yahoo.com", (byte)1, "Jimmie", null, "Crona", "502-726-042", 5471.83m, "7121798573", (byte)1, 17.0 },
                    { 16, true, null, new DateTime(1986, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null, "Pauline28@hotmail.com", (byte)1, "Pauline", null, "Bernier", "504-456-876", 3677.71m, "0337788633", (byte)6, 15.0 },
                    { 17, true, null, new DateTime(1985, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, null, "Doreen85@hotmail.com", (byte)3, "Doreen", null, "Howell", "225-811-351", 8939.81m, "9617780191", (byte)3, 20.0 },
                    { 18, true, null, new DateTime(1980, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, null, "Ken_Sanford60@gmail.com", (byte)1, "Ken", null, "Sanford", "942-898-322", 3123.75m, "6226218643", (byte)5, 26.0 },
                    { 19, true, null, new DateTime(1951, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, "Delia.Mueller@yahoo.com", (byte)1, "Delia", null, "Mueller", "623-189-970", 4349.47m, "8289488043", (byte)5, 38.0 },
                    { 20, true, null, new DateTime(1997, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null, "Roxanne70@gmail.com", (byte)2, "Roxanne", null, "Monahan", "769-754-910", 5128.09m, "0910355883", (byte)6, 26.0 },
                    { 21, true, null, new DateTime(1989, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 63, null, "Noel.Sipes44@gmail.com", (byte)2, "Noel", null, "Sipes", "141-182-257", 6014.64m, "3889124248", (byte)1, 28.0 },
                    { 22, true, null, new DateTime(1988, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 52, null, "Angel54@hotmail.com", (byte)2, "Angel", null, "D'Amore", "540-893-421", 4336.66m, "9531038012", (byte)5, 29.0 },
                    { 23, true, null, new DateTime(1969, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, "Victor62@hotmail.com", (byte)3, "Victor", null, "Schaden", "122-535-206", 3216.06m, "6733251548", (byte)5, 33.0 },
                    { 24, true, null, new DateTime(1951, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 62, null, "Irving.Lang42@hotmail.com", (byte)2, "Irving", null, "Lang", "388-925-803", 7273.30m, "4447839339", (byte)4, 19.0 },
                    { 25, true, null, new DateTime(1958, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 34, null, "Lonnie.Rippin77@gmail.com", (byte)3, "Lonnie", null, "Rippin", "604-868-283", 2860.05m, "3032390872", (byte)1, 33.0 },
                    { 26, true, null, new DateTime(1999, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null, "Jessie.Flatley@gmail.com", (byte)1, "Jessie", null, "Flatley", "988-530-130", 3223.22m, "5616611953", (byte)2, 31.0 },
                    { 27, true, null, new DateTime(1977, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 97, null, "Sherman.Hackett@yahoo.com", (byte)1, "Sherman", null, "Hackett", "769-095-016", 2852.21m, "7966972082", (byte)2, 19.0 },
                    { 28, true, null, new DateTime(1980, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, null, "Bill51@yahoo.com", (byte)1, "Bill", null, "Abbott", "088-397-434", 6616.85m, "6938729445", (byte)5, 32.0 },
                    { 29, true, null, new DateTime(1947, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, "Tonya.Dare@hotmail.com", (byte)3, "Tonya", null, "Dare", "620-859-897", 8148.26m, "0081469622", (byte)1, 35.0 },
                    { 30, true, null, new DateTime(1973, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, null, "Grady70@yahoo.com", (byte)2, "Grady", null, "Connelly", "697-610-167", 9586.12m, "8510619081", (byte)6, 28.0 },
                    { 31, true, null, new DateTime(1944, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 73, null, "Abraham55@yahoo.com", (byte)3, "Abraham", null, "Smith", "969-704-059", 8458.34m, "9490271692", (byte)2, 25.0 },
                    { 32, true, null, new DateTime(1975, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, null, "Hugo61@gmail.com", (byte)1, "Hugo", null, "Steuber", "651-713-868", 9711.71m, "9155616066", (byte)5, 33.0 },
                    { 33, true, null, new DateTime(1971, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 52, null, "Erma0@yahoo.com", (byte)1, "Erma", null, "Bahringer", "163-334-558", 3008.21m, "9560123832", (byte)6, 32.0 },
                    { 34, true, null, new DateTime(1971, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, "Erin_Treutel10@hotmail.com", (byte)2, "Erin", null, "Treutel", "952-767-108", 5206.29m, "6547600122", (byte)4, 35.0 },
                    { 35, true, null, new DateTime(1998, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, "Bernard81@gmail.com", (byte)2, "Bernard", null, "Kris", "437-630-212", 3673.70m, "4640247966", (byte)5, 28.0 },
                    { 36, true, null, new DateTime(1970, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null, "Nicolas52@yahoo.com", (byte)2, "Nicolas", null, "Hilll", "690-605-725", 7971.04m, "3966068457", (byte)2, 25.0 },
                    { 37, true, null, new DateTime(1961, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, null, "Don.Lueilwitz82@gmail.com", (byte)1, "Don", null, "Lueilwitz", "304-489-999", 9308.08m, "6511101605", (byte)5, 17.0 },
                    { 38, true, null, new DateTime(1992, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, "Antoinette_Turcotte10@gmail.com", (byte)2, "Antoinette", null, "Turcotte", "704-191-763", 3096.94m, "4859549171", (byte)5, 27.0 },
                    { 39, true, null, new DateTime(1970, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, "Elsa.Tillman86@gmail.com", (byte)1, "Elsa", null, "Tillman", "778-327-635", 6229.73m, "7944457287", (byte)6, 32.0 },
                    { 40, true, null, new DateTime(1977, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 61, null, "Katie.Metz@gmail.com", (byte)1, "Katie", null, "Metz", "569-548-451", 8730.42m, "2361567593", (byte)4, 17.0 },
                    { 41, true, null, new DateTime(1974, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, null, "Loren.Nienow60@yahoo.com", (byte)2, "Loren", null, "Nienow", "978-798-509", 8400.37m, "2978913294", (byte)5, 33.0 },
                    { 42, true, null, new DateTime(1960, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 51, null, "Mercedes63@yahoo.com", (byte)1, "Mercedes", null, "Bailey", "614-125-908", 8184.92m, "5378772727", (byte)5, 24.0 },
                    { 43, true, null, new DateTime(1943, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 75, null, "Esther25@hotmail.com", (byte)1, "Esther", null, "Trantow", "883-638-674", 3682.79m, "1668704525", (byte)1, 26.0 },
                    { 44, true, null, new DateTime(1997, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, "Jasmine_Kuhlman81@yahoo.com", (byte)2, "Jasmine", null, "Kuhlman", "468-934-827", 5604.03m, "6280329801", (byte)3, 19.0 },
                    { 45, true, null, new DateTime(1963, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, "Penny_Mitchell75@gmail.com", (byte)3, "Penny", null, "Mitchell", "740-688-646", 5662.12m, "1821040648", (byte)4, 17.0 },
                    { 46, true, null, new DateTime(1967, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 71, null, "Douglas.Nienow25@yahoo.com", (byte)1, "Douglas", null, "Nienow", "582-053-117", 8595.50m, "8973939561", (byte)5, 30.0 },
                    { 47, true, null, new DateTime(1943, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 69, null, "Nellie.Spencer@yahoo.com", (byte)2, "Nellie", null, "Spencer", "538-375-980", 6311.70m, "5922440056", (byte)4, 20.0 },
                    { 48, true, null, new DateTime(1971, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 66, null, "Robin.Wehner44@gmail.com", (byte)1, "Robin", null, "Wehner", "914-895-775", 7614.95m, "3384781844", (byte)1, 35.0 },
                    { 49, true, null, new DateTime(1959, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, "Sergio79@gmail.com", (byte)3, "Sergio", null, "Botsford", "219-672-898", 5620.86m, "1405587991", (byte)1, 24.0 },
                    { 50, true, null, new DateTime(1977, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null, "Kristopher97@yahoo.com", (byte)1, "Kristopher", null, "Jacobs", "481-933-062", 5456.63m, "2411715347", (byte)1, 33.0 }
                });

            migrationBuilder.InsertData(
                table: "Uzytkownicy",
                columns: new[] { "Id", "CzyAktywny", "Email", "HashHasla", "Imie", "Nazwisko", "RolaId" },
                values: new object[,]
                {
                    { 1, true, "jan@kowalski.mail.com", "AAA", "Jan", "Kowalski", (byte)2 },
                    { 2, true, "roman@nowak.mail.com", "BBB", "Roman", "Nowak", (byte)3 }
                });

            migrationBuilder.InsertData(
                table: "Uzytkownicy",
                columns: new[] { "Id", "CzyAktywny", "Email", "HashHasla", "Imie", "Nazwisko", "RolaId" },
                values: new object[] { 3, true, "adam@wisniewski.mail.com", "CCC", "Adam", "Wiśniewski", (byte)4 });

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
                    { (byte)10, true, "Tygryski", null, 10 },
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
                    { 119, 7 }
                });

            migrationBuilder.InsertData(
                table: "PracownicyAdresy",
                columns: new[] { "AdresId", "PracownikId" },
                values: new object[,]
                {
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
                    { 69, 40 },
                    { 132, 44 },
                    { 192, 44 },
                    { 93, 45 },
                    { 188, 45 },
                    { 199, 45 },
                    { 130, 46 },
                    { 72, 47 },
                    { 98, 48 }
                });

            migrationBuilder.InsertData(
                table: "PracownicyAdresy",
                columns: new[] { "AdresId", "PracownikId" },
                values: new object[] { 103, 49 });

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
                    { 21, (byte)10 },
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
                    { 40, (byte)16 }
                });

            migrationBuilder.InsertData(
                table: "PrzedmiotyPracownicy",
                columns: new[] { "PracownikId", "PrzedmiotId" },
                values: new object[,]
                {
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
                    { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, true, new DateTime(2022, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
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
                    { new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Urlopy",
                columns: new[] { "PoczatekUrlopu", "PracownikId", "CzyAktywny", "KoniecUrlopu" },
                values: new object[,]
                {
                    { new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, new DateTime(2022, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(2022, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, true, new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel", "WychowawcaId" },
                values: new object[,]
                {
                    { 1, 145, true, new DateTime(2014, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfred", "Gulgowski", (byte)22, "8532821970", 36 },
                    { 2, 108, true, new DateTime(2008, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Homer", "Halvorson", (byte)3, "0350930201", 33 },
                    { 3, 118, true, new DateTime(2017, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robert", "Murphy", (byte)30, "2721701066", 38 },
                    { 4, 147, true, new DateTime(2013, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Conrad", "Jakubowski", (byte)3, "9043444110", 44 },
                    { 5, 123, true, new DateTime(2015, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kenny", "VonRueden", (byte)20, "8192051985", 41 },
                    { 6, 24, true, new DateTime(2016, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Terrell", "Schimmel", (byte)5, "2772134661", 8 },
                    { 7, 161, true, new DateTime(2018, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dale", "Moore", (byte)1, "6813590869", 33 },
                    { 8, 93, true, new DateTime(2015, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Darnell", "Johnston", (byte)27, "7775058779", 20 },
                    { 9, 167, true, new DateTime(2008, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Guillermo", "Lynch", (byte)3, "6666934133", 4 },
                    { 10, 84, true, new DateTime(2007, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sara", "Wintheiser", (byte)11, "4222152467", 26 },
                    { 11, 147, true, new DateTime(2013, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lora", "Schmidt", (byte)35, "5310345063", 36 },
                    { 12, 85, true, new DateTime(2012, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shelley", "Turcotte", (byte)35, "8677405838", 23 },
                    { 13, 59, true, new DateTime(2017, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Douglas", "Steuber", (byte)19, "4598720739", 11 },
                    { 14, 151, true, new DateTime(2013, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Darlene", "Dickens", (byte)24, "7723235818", 42 },
                    { 15, 124, true, new DateTime(2013, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Traci", "Ratke", (byte)14, "2549923325", 46 },
                    { 16, 169, true, new DateTime(2018, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Doug", "Harvey", (byte)21, "2216077859", 17 },
                    { 17, 38, true, new DateTime(2013, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lora", "Jacobs", (byte)30, "5856041730", 15 },
                    { 18, 4, true, new DateTime(2016, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Milton", "Homenick", (byte)22, "0238176473", 38 },
                    { 19, 141, true, new DateTime(2010, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mathew", "Haag", (byte)21, "0455976756", 5 },
                    { 20, 163, true, new DateTime(2018, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oliver", "Nitzsche", (byte)19, "2925295632", 31 },
                    { 21, 162, true, new DateTime(2012, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Latoya", "Fritsch", (byte)26, "1899200910", 19 },
                    { 22, 180, true, new DateTime(2017, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Melinda", "Bechtelar", (byte)20, "8976605943", 27 },
                    { 23, 95, true, new DateTime(2006, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Louis", "Blanda", (byte)20, "8642186349", 37 },
                    { 24, 102, true, new DateTime(2018, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ricky", "Schulist", (byte)29, "7986632958", 35 },
                    { 25, 62, true, new DateTime(2011, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mary", "Kessler", (byte)21, "3027611794", 41 },
                    { 26, 97, true, new DateTime(2010, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mable", "Goyette", (byte)11, "0055212877", 28 },
                    { 27, 154, true, new DateTime(2012, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santos", "Thompson", (byte)9, "3375837035", 37 },
                    { 28, 71, true, new DateTime(2014, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laurence", "Leannon", (byte)17, "8527026279", 30 },
                    { 29, 115, true, new DateTime(2014, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gilberto", "Pagac", (byte)34, "3649156517", 11 },
                    { 30, 25, true, new DateTime(2019, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roman", "Tillman", (byte)30, "4391667261", 30 },
                    { 31, 76, true, new DateTime(2008, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Delbert", "Leannon", (byte)35, "9647234144", 50 },
                    { 32, 15, true, new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tom", "Predovic", (byte)23, "7246564736", 37 },
                    { 33, 165, true, new DateTime(2013, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Candace", "McLaughlin", (byte)5, "3141078077", 39 },
                    { 34, 188, true, new DateTime(2013, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mercedes", "Gerhold", (byte)19, "1779429088", 18 },
                    { 35, 155, true, new DateTime(2013, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eula", "Macejkovic", (byte)12, "5355874262", 44 },
                    { 36, 88, true, new DateTime(2015, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mike", "Braun", (byte)21, "8765943653", 18 },
                    { 37, 67, true, new DateTime(2006, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Scott", "Rau", (byte)16, "8742059777", 33 },
                    { 38, 92, true, new DateTime(2006, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sarah", "Pfannerstill", (byte)22, "9868522382", 30 },
                    { 39, 152, true, new DateTime(2018, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Doyle", "Altenwerth", (byte)22, "8171179066", 33 },
                    { 40, 33, true, new DateTime(2017, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Loren", "Haag", (byte)8, "0894514427", 21 },
                    { 41, 84, true, new DateTime(2011, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shannon", "Graham", (byte)21, "2676876558", 4 },
                    { 42, 77, true, new DateTime(2011, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lonnie", "Pouros", (byte)21, "4999722165", 33 }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel", "WychowawcaId" },
                values: new object[,]
                {
                    { 43, 80, true, new DateTime(2011, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dewey", "Hansen", (byte)18, "2528373028", 36 },
                    { 44, 171, true, new DateTime(2006, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samantha", "Ernser", (byte)1, "4468972080", 4 },
                    { 45, 40, true, new DateTime(2013, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shelly", "Purdy", (byte)6, "6140096741", 47 },
                    { 46, 65, true, new DateTime(2018, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frank", "Herman", (byte)12, "6171165432", 23 },
                    { 47, 160, true, new DateTime(2011, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mathew", "O'Keefe", (byte)34, "1095383208", 40 },
                    { 48, 121, true, new DateTime(2009, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Duane", "Klocko", (byte)13, "8707306844", 40 },
                    { 49, 155, true, new DateTime(2007, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kimberly", "Prosacco", (byte)3, "1402528648", 30 },
                    { 50, 117, true, new DateTime(2014, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Darla", "Grant", (byte)1, "7796626261", 7 },
                    { 51, 44, true, new DateTime(2016, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jacqueline", "Crooks", (byte)14, "8919969073", 45 },
                    { 52, 16, true, new DateTime(2007, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rudy", "Rosenbaum", (byte)10, "8778001572", 1 },
                    { 53, 85, true, new DateTime(2019, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robyn", "Ledner", (byte)3, "8980684549", 18 },
                    { 54, 34, true, new DateTime(2014, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nichole", "Beatty", (byte)34, "8346772944", 48 },
                    { 55, 152, true, new DateTime(2015, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Clara", "Denesik", (byte)10, "9651534658", 39 },
                    { 56, 135, true, new DateTime(2008, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jimmie", "Heidenreich", (byte)30, "5448469165", 25 },
                    { 57, 134, true, new DateTime(2011, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emily", "Barrows", (byte)31, "6599763514", 50 },
                    { 58, 65, true, new DateTime(2005, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Richard", "Grant", (byte)9, "1896234114", 45 },
                    { 59, 44, true, new DateTime(2011, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Barbara", "Crist", (byte)18, "9238183162", 17 },
                    { 60, 22, true, new DateTime(2007, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samuel", "Walter", (byte)25, "9212302302", 25 },
                    { 61, 129, true, new DateTime(2011, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eula", "Moore", (byte)32, "4454124463", 37 },
                    { 62, 79, true, new DateTime(2007, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Erik", "Prosacco", (byte)30, "2837299741", 14 },
                    { 63, 105, true, new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emily", "McLaughlin", (byte)5, "5230677971", 34 },
                    { 64, 78, true, new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Norma", "Reinger", (byte)35, "9172541213", 42 },
                    { 65, 127, true, new DateTime(2016, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Virgil", "Gleason", (byte)34, "2486519538", 18 },
                    { 66, 19, true, new DateTime(2017, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mack", "Schoen", (byte)18, "6184041747", 50 },
                    { 67, 99, true, new DateTime(2015, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Arlene", "Kozey", (byte)31, "7985908017", 20 },
                    { 68, 55, true, new DateTime(2017, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jeff", "Wilderman", (byte)17, "5330716815", 43 },
                    { 69, 49, true, new DateTime(2005, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lucia", "McCullough", (byte)12, "2605609578", 40 },
                    { 70, 72, true, new DateTime(2010, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Devin", "Rodriguez", (byte)15, "1127847361", 37 },
                    { 71, 178, true, new DateTime(2005, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Perry", "Krajcik", (byte)4, "4975185052", 33 },
                    { 72, 109, true, new DateTime(2018, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marta", "Cormier", (byte)21, "9094143252", 16 },
                    { 73, 57, true, new DateTime(2010, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sheldon", "Lakin", (byte)17, "4972831524", 46 },
                    { 74, 172, true, new DateTime(2005, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bill", "Rodriguez", (byte)21, "7808707855", 6 },
                    { 75, 187, true, new DateTime(2017, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vera", "Johns", (byte)5, "6264430624", 22 },
                    { 76, 175, true, new DateTime(2011, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mona", "Watsica", (byte)26, "1559353839", 3 },
                    { 77, 91, true, new DateTime(2011, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rhonda", "Fisher", (byte)6, "6033245543", 16 },
                    { 78, 38, true, new DateTime(2012, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elsie", "Russel", (byte)30, "8626064959", 6 },
                    { 79, 195, true, new DateTime(2018, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suzanne", "Bartell", (byte)27, "6751445784", 12 },
                    { 80, 60, true, new DateTime(2017, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mable", "West", (byte)27, "8725118312", 21 },
                    { 81, 197, true, new DateTime(2016, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tricia", "Hamill", (byte)30, "2510928488", 37 },
                    { 82, 33, true, new DateTime(2017, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kerry", "Blick", (byte)7, "3806913298", 48 },
                    { 83, 116, true, new DateTime(2018, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "George", "Wunsch", (byte)23, "8855688631", 19 },
                    { 84, 5, true, new DateTime(2009, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alyssa", "Satterfield", (byte)19, "9168379811", 14 }
                });

            migrationBuilder.InsertData(
                table: "Uczniowie",
                columns: new[] { "Id", "AdresId", "CzyAktywny", "DataUrodzenia", "DrugieImie", "Imie", "Nazwisko", "OddzialId", "Pesel", "WychowawcaId" },
                values: new object[,]
                {
                    { 85, 151, true, new DateTime(2017, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Todd", "Roberts", (byte)5, "1723101381", 5 },
                    { 86, 22, true, new DateTime(2005, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sue", "Barton", (byte)1, "0959151452", 49 },
                    { 87, 192, true, new DateTime(2010, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tabitha", "Steuber", (byte)23, "2036054157", 35 },
                    { 88, 57, true, new DateTime(2012, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ruben", "Mertz", (byte)14, "4925652767", 14 },
                    { 89, 78, true, new DateTime(2005, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Johnnie", "Hermann", (byte)24, "2355945242", 37 },
                    { 90, 7, true, new DateTime(2013, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Peter", "Koelpin", (byte)17, "4048814420", 9 },
                    { 91, 20, true, new DateTime(2019, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Priscilla", "Tillman", (byte)14, "1362497579", 10 },
                    { 92, 78, true, new DateTime(2005, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Virginia", "Lemke", (byte)11, "4988954039", 34 },
                    { 93, 137, true, new DateTime(2016, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sandy", "Roberts", (byte)17, "7310329779", 12 },
                    { 94, 138, true, new DateTime(2010, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nicholas", "Gulgowski", (byte)23, "7240997697", 23 },
                    { 95, 83, true, new DateTime(2019, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lindsay", "Roob", (byte)29, "8610098609", 20 },
                    { 96, 185, true, new DateTime(2009, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marshall", "Schiller", (byte)32, "3640992978", 11 },
                    { 97, 121, true, new DateTime(2019, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Homer", "Powlowski", (byte)12, "0795584546", 37 },
                    { 98, 88, true, new DateTime(2008, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Don", "Ryan", (byte)19, "2659222796", 50 },
                    { 99, 28, true, new DateTime(2009, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sally", "Hilll", (byte)25, "2472696622", 35 },
                    { 100, 56, true, new DateTime(2015, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bradford", "Hahn", (byte)24, "6309142792", 48 }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 1L, true, null, new DateTime(2022, 8, 23, 13, 45, 38, 660, DateTimeKind.Local).AddTicks(1543), null, 36, (byte)11, 21, 1.3224047344747955m },
                    { 2L, true, null, new DateTime(2022, 9, 18, 17, 38, 14, 129, DateTimeKind.Local).AddTicks(7735), null, 10, (byte)13, 51, 2.97421061199820m },
                    { 3L, true, null, new DateTime(2022, 9, 27, 10, 34, 20, 398, DateTimeKind.Local).AddTicks(3583), null, 28, (byte)6, 89, 5.683763424252980m },
                    { 4L, true, null, new DateTime(2022, 8, 3, 18, 7, 3, 698, DateTimeKind.Local).AddTicks(4340), null, 42, (byte)10, 23, 4.619625218501140m },
                    { 5L, true, null, new DateTime(2022, 10, 20, 14, 58, 39, 150, DateTimeKind.Local).AddTicks(3739), null, 23, (byte)6, 66, 3.289330662362895m },
                    { 6L, true, null, new DateTime(2022, 10, 4, 5, 37, 12, 648, DateTimeKind.Local).AddTicks(3873), null, 7, (byte)11, 5, 5.29666342879490m },
                    { 7L, true, null, new DateTime(2022, 8, 2, 21, 44, 59, 421, DateTimeKind.Local).AddTicks(78), null, 13, (byte)12, 12, 4.304368948240005m },
                    { 8L, true, null, new DateTime(2022, 9, 24, 9, 28, 43, 978, DateTimeKind.Local).AddTicks(10), null, 31, (byte)5, 4, 4.379544703466605m },
                    { 9L, true, null, new DateTime(2022, 10, 30, 16, 59, 15, 877, DateTimeKind.Local).AddTicks(5818), null, 43, (byte)11, 81, 1.571441878830755m },
                    { 10L, true, null, new DateTime(2022, 10, 19, 19, 58, 55, 888, DateTimeKind.Local).AddTicks(1990), null, 26, (byte)4, 74, 3.611762067587935m },
                    { 11L, true, null, new DateTime(2022, 10, 3, 17, 59, 13, 76, DateTimeKind.Local).AddTicks(7058), null, 21, (byte)3, 1, 1.0677779503482290m },
                    { 12L, true, null, new DateTime(2022, 9, 5, 23, 58, 48, 876, DateTimeKind.Local).AddTicks(6488), null, 3, (byte)3, 91, 5.511636213637720m },
                    { 13L, true, null, new DateTime(2022, 10, 13, 12, 30, 10, 30, DateTimeKind.Local).AddTicks(7015), null, 8, (byte)15, 89, 1.3439475876949485m },
                    { 14L, true, null, new DateTime(2022, 8, 19, 4, 51, 26, 743, DateTimeKind.Local).AddTicks(4473), null, 22, (byte)11, 19, 5.937103856325665m },
                    { 15L, true, null, new DateTime(2022, 10, 28, 7, 35, 22, 160, DateTimeKind.Local).AddTicks(2336), null, 40, (byte)15, 98, 2.497401158091335m },
                    { 16L, true, null, new DateTime(2022, 9, 2, 2, 9, 35, 146, DateTimeKind.Local).AddTicks(2352), null, 33, (byte)4, 3, 2.98275006235705m },
                    { 17L, true, null, new DateTime(2022, 8, 7, 0, 2, 45, 693, DateTimeKind.Local).AddTicks(3222), null, 20, (byte)2, 82, 4.446321260857545m },
                    { 18L, true, null, new DateTime(2022, 8, 7, 14, 12, 46, 36, DateTimeKind.Local).AddTicks(3138), null, 13, (byte)12, 95, 2.649870060686895m },
                    { 19L, true, null, new DateTime(2022, 10, 8, 22, 14, 43, 674, DateTimeKind.Local).AddTicks(1282), null, 36, (byte)6, 89, 1.3375318112492245m },
                    { 20L, true, null, new DateTime(2022, 9, 3, 11, 2, 40, 366, DateTimeKind.Local).AddTicks(1733), null, 49, (byte)10, 96, 5.405517617429380m },
                    { 21L, true, null, new DateTime(2022, 9, 29, 9, 20, 38, 704, DateTimeKind.Local).AddTicks(6682), null, 17, (byte)5, 10, 4.679909977912860m },
                    { 22L, true, null, new DateTime(2022, 8, 30, 1, 49, 58, 397, DateTimeKind.Local).AddTicks(918), null, 22, (byte)15, 13, 5.641617652793235m },
                    { 23L, true, null, new DateTime(2022, 10, 15, 16, 19, 55, 548, DateTimeKind.Local).AddTicks(6396), null, 2, (byte)13, 73, 2.27416398668390m },
                    { 24L, true, null, new DateTime(2022, 7, 29, 23, 17, 38, 879, DateTimeKind.Local).AddTicks(4564), null, 17, (byte)3, 79, 1.802071742155620m },
                    { 25L, true, null, new DateTime(2022, 10, 28, 14, 26, 3, 994, DateTimeKind.Local).AddTicks(249), null, 25, (byte)6, 50, 1.4247420981641590m },
                    { 26L, true, null, new DateTime(2022, 10, 25, 12, 30, 12, 88, DateTimeKind.Local).AddTicks(990), null, 46, (byte)14, 64, 1.1017741440337960m },
                    { 27L, true, null, new DateTime(2022, 10, 5, 5, 30, 24, 694, DateTimeKind.Local).AddTicks(6333), null, 35, (byte)6, 46, 4.813787800638840m },
                    { 28L, true, null, new DateTime(2022, 9, 19, 21, 30, 51, 622, DateTimeKind.Local).AddTicks(1639), null, 16, (byte)7, 57, 2.573816370951860m },
                    { 29L, true, null, new DateTime(2022, 8, 9, 21, 32, 21, 757, DateTimeKind.Local).AddTicks(5459), null, 6, (byte)13, 7, 4.261716844635885m },
                    { 30L, true, null, new DateTime(2022, 8, 3, 7, 41, 34, 660, DateTimeKind.Local).AddTicks(4482), null, 50, (byte)2, 15, 5.279585280120180m },
                    { 31L, true, null, new DateTime(2022, 10, 21, 19, 34, 7, 314, DateTimeKind.Local).AddTicks(7986), null, 10, (byte)7, 80, 5.462075328669545m },
                    { 32L, true, null, new DateTime(2022, 9, 20, 1, 47, 5, 994, DateTimeKind.Local).AddTicks(6357), null, 42, (byte)3, 78, 4.268010000823070m },
                    { 33L, true, null, new DateTime(2022, 7, 31, 0, 52, 39, 196, DateTimeKind.Local).AddTicks(7054), null, 41, (byte)15, 33, 5.075694581994645m },
                    { 34L, true, null, new DateTime(2022, 8, 7, 1, 32, 12, 632, DateTimeKind.Local).AddTicks(6790), null, 29, (byte)17, 42, 5.693664877998485m },
                    { 35L, true, null, new DateTime(2022, 8, 10, 13, 51, 40, 497, DateTimeKind.Local).AddTicks(4200), null, 39, (byte)15, 42, 4.454921549863610m },
                    { 36L, true, null, new DateTime(2022, 10, 26, 22, 47, 55, 377, DateTimeKind.Local).AddTicks(4759), null, 21, (byte)12, 74, 2.027644067549910m },
                    { 37L, true, null, new DateTime(2022, 8, 5, 16, 47, 5, 539, DateTimeKind.Local).AddTicks(3160), null, 39, (byte)17, 55, 1.375829413708220m },
                    { 38L, true, null, new DateTime(2022, 9, 18, 21, 54, 28, 944, DateTimeKind.Local).AddTicks(9018), null, 46, (byte)7, 5, 4.062584189727245m },
                    { 39L, true, null, new DateTime(2022, 8, 31, 4, 40, 38, 110, DateTimeKind.Local).AddTicks(3827), null, 27, (byte)13, 41, 5.235563384944370m },
                    { 40L, true, null, new DateTime(2022, 10, 5, 13, 13, 20, 701, DateTimeKind.Local).AddTicks(7395), null, 40, (byte)17, 22, 4.371573350565310m },
                    { 41L, true, null, new DateTime(2022, 10, 18, 21, 43, 26, 336, DateTimeKind.Local).AddTicks(9569), null, 29, (byte)3, 17, 5.740815302236385m },
                    { 42L, true, null, new DateTime(2022, 8, 24, 5, 50, 36, 133, DateTimeKind.Local).AddTicks(4484), null, 39, (byte)13, 96, 1.098981198435175m }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 43L, true, null, new DateTime(2022, 8, 27, 9, 59, 36, 215, DateTimeKind.Local).AddTicks(7294), null, 6, (byte)17, 12, 3.77353559284170m },
                    { 44L, true, null, new DateTime(2022, 10, 29, 12, 24, 6, 23, DateTimeKind.Local).AddTicks(4280), null, 13, (byte)14, 37, 5.468691332483985m },
                    { 45L, true, null, new DateTime(2022, 9, 28, 14, 22, 38, 620, DateTimeKind.Local).AddTicks(4212), null, 26, (byte)7, 1, 3.297334895607705m },
                    { 46L, true, null, new DateTime(2022, 10, 29, 2, 2, 19, 736, DateTimeKind.Local).AddTicks(1275), null, 18, (byte)17, 70, 1.122694093791160m },
                    { 47L, true, null, new DateTime(2022, 9, 13, 10, 47, 49, 678, DateTimeKind.Local).AddTicks(2609), null, 10, (byte)17, 79, 5.463219037029530m },
                    { 48L, true, null, new DateTime(2022, 10, 22, 14, 0, 10, 129, DateTimeKind.Local).AddTicks(3323), null, 31, (byte)1, 40, 5.479115977640785m },
                    { 49L, true, null, new DateTime(2022, 9, 29, 4, 16, 33, 742, DateTimeKind.Local).AddTicks(3434), null, 48, (byte)7, 34, 5.382786145146370m },
                    { 50L, true, null, new DateTime(2022, 9, 25, 16, 35, 17, 464, DateTimeKind.Local).AddTicks(4551), null, 21, (byte)3, 46, 1.714411221311620m },
                    { 51L, true, null, new DateTime(2022, 8, 18, 5, 45, 37, 15, DateTimeKind.Local).AddTicks(4815), null, 22, (byte)1, 44, 4.005438436756580m },
                    { 52L, true, null, new DateTime(2022, 8, 15, 12, 21, 53, 758, DateTimeKind.Local).AddTicks(235), null, 28, (byte)12, 80, 3.828796474649010m },
                    { 53L, true, null, new DateTime(2022, 9, 13, 4, 18, 0, 511, DateTimeKind.Local).AddTicks(2259), null, 4, (byte)12, 96, 5.711256997991005m },
                    { 54L, true, null, new DateTime(2022, 10, 26, 16, 2, 22, 506, DateTimeKind.Local).AddTicks(765), null, 16, (byte)2, 13, 1.1040578866862030m },
                    { 55L, true, null, new DateTime(2022, 10, 24, 22, 15, 14, 736, DateTimeKind.Local).AddTicks(1395), null, 43, (byte)16, 37, 4.755892128104295m },
                    { 56L, true, null, new DateTime(2022, 7, 31, 12, 0, 45, 295, DateTimeKind.Local).AddTicks(9942), null, 10, (byte)9, 62, 1.4142715783856210m },
                    { 57L, true, null, new DateTime(2022, 9, 1, 15, 27, 38, 83, DateTimeKind.Local).AddTicks(714), null, 45, (byte)15, 55, 4.065281078715475m },
                    { 58L, true, null, new DateTime(2022, 7, 31, 1, 28, 49, 512, DateTimeKind.Local).AddTicks(9054), null, 39, (byte)7, 74, 1.4832478917591495m },
                    { 59L, true, null, new DateTime(2022, 10, 30, 13, 4, 52, 192, DateTimeKind.Local).AddTicks(1781), null, 41, (byte)8, 71, 1.3629045213399940m },
                    { 60L, true, null, new DateTime(2022, 10, 30, 13, 11, 24, 694, DateTimeKind.Local).AddTicks(5781), null, 1, (byte)8, 65, 2.699773041391640m },
                    { 61L, true, null, new DateTime(2022, 9, 11, 7, 20, 29, 81, DateTimeKind.Local).AddTicks(6806), null, 15, (byte)3, 40, 1.2058552905013110m },
                    { 62L, true, null, new DateTime(2022, 9, 5, 13, 17, 20, 407, DateTimeKind.Local).AddTicks(1987), null, 3, (byte)6, 56, 3.409829398342330m },
                    { 63L, true, null, new DateTime(2022, 8, 5, 10, 22, 23, 29, DateTimeKind.Local).AddTicks(2927), null, 47, (byte)11, 30, 3.961850954667595m },
                    { 64L, true, null, new DateTime(2022, 8, 20, 12, 58, 20, 910, DateTimeKind.Local).AddTicks(3466), null, 27, (byte)2, 10, 2.451494803862410m },
                    { 65L, true, null, new DateTime(2022, 10, 7, 19, 57, 12, 523, DateTimeKind.Local).AddTicks(9553), null, 29, (byte)17, 75, 1.2151703625988075m },
                    { 66L, true, null, new DateTime(2022, 10, 24, 23, 12, 47, 664, DateTimeKind.Local).AddTicks(5360), null, 8, (byte)14, 93, 4.441172821187030m },
                    { 67L, true, null, new DateTime(2022, 8, 28, 13, 14, 45, 693, DateTimeKind.Local).AddTicks(7483), null, 28, (byte)3, 18, 1.0997794140594915m },
                    { 68L, true, null, new DateTime(2022, 9, 7, 14, 10, 8, 183, DateTimeKind.Local).AddTicks(4760), null, 26, (byte)14, 41, 1.2886039532202315m },
                    { 69L, true, null, new DateTime(2022, 11, 1, 18, 23, 17, 987, DateTimeKind.Local).AddTicks(340), null, 11, (byte)16, 44, 3.418961579640845m },
                    { 70L, true, null, new DateTime(2022, 9, 14, 11, 38, 48, 557, DateTimeKind.Local).AddTicks(4491), null, 26, (byte)15, 11, 1.742543337746775m },
                    { 71L, true, null, new DateTime(2022, 9, 18, 4, 15, 28, 786, DateTimeKind.Local).AddTicks(6907), null, 46, (byte)3, 57, 3.834827298221565m },
                    { 72L, true, null, new DateTime(2022, 9, 26, 0, 18, 37, 659, DateTimeKind.Local).AddTicks(7452), null, 27, (byte)2, 44, 4.755453305205075m },
                    { 73L, true, null, new DateTime(2022, 10, 29, 15, 3, 5, 191, DateTimeKind.Local).AddTicks(2437), null, 7, (byte)11, 75, 2.816218582362040m },
                    { 74L, true, null, new DateTime(2022, 9, 24, 16, 9, 37, 760, DateTimeKind.Local).AddTicks(4190), null, 39, (byte)10, 15, 5.497475109294745m },
                    { 75L, true, null, new DateTime(2022, 9, 10, 15, 55, 5, 383, DateTimeKind.Local).AddTicks(6605), null, 7, (byte)1, 29, 4.4890416955990m },
                    { 76L, true, null, new DateTime(2022, 8, 20, 6, 1, 39, 468, DateTimeKind.Local).AddTicks(8287), null, 7, (byte)9, 84, 1.01150764991133830m },
                    { 77L, true, null, new DateTime(2022, 10, 15, 1, 8, 24, 547, DateTimeKind.Local).AddTicks(8840), null, 3, (byte)12, 9, 1.823465299710380m },
                    { 78L, true, null, new DateTime(2022, 10, 21, 10, 16, 15, 229, DateTimeKind.Local).AddTicks(3161), null, 49, (byte)10, 2, 3.666678506725785m },
                    { 79L, true, null, new DateTime(2022, 9, 20, 16, 23, 2, 374, DateTimeKind.Local).AddTicks(3748), null, 4, (byte)2, 31, 4.247279014087880m },
                    { 80L, true, null, new DateTime(2022, 10, 9, 7, 39, 26, 113, DateTimeKind.Local).AddTicks(7465), null, 24, (byte)10, 83, 3.058347017531445m },
                    { 81L, true, null, new DateTime(2022, 9, 28, 5, 22, 44, 26, DateTimeKind.Local).AddTicks(5502), null, 19, (byte)17, 52, 4.634239290204940m },
                    { 82L, true, null, new DateTime(2022, 10, 2, 2, 15, 31, 406, DateTimeKind.Local).AddTicks(4037), null, 31, (byte)8, 55, 1.02601840301697070m },
                    { 83L, true, null, new DateTime(2022, 9, 30, 12, 1, 6, 608, DateTimeKind.Local).AddTicks(8872), null, 36, (byte)2, 96, 5.876039135212095m },
                    { 84L, true, null, new DateTime(2022, 10, 27, 6, 57, 19, 977, DateTimeKind.Local).AddTicks(9516), null, 3, (byte)8, 8, 1.726622166450425m }
                });

            migrationBuilder.InsertData(
                table: "Oceny",
                columns: new[] { "Id", "CzyAktywny", "DataPoprawienia", "DataWystawienia", "PoprzedniaOcena", "PracownikId", "PrzedmiotId", "UczenId", "WystawionaOcena" },
                values: new object[,]
                {
                    { 85L, true, null, new DateTime(2022, 10, 1, 14, 10, 49, 204, DateTimeKind.Local).AddTicks(1010), null, 38, (byte)1, 56, 4.726901078469540m },
                    { 86L, true, null, new DateTime(2022, 10, 27, 15, 0, 2, 971, DateTimeKind.Local).AddTicks(1454), null, 42, (byte)7, 23, 2.181203281125615m },
                    { 87L, true, null, new DateTime(2022, 9, 26, 6, 39, 48, 724, DateTimeKind.Local).AddTicks(6391), null, 16, (byte)2, 30, 4.635488529054210m },
                    { 88L, true, null, new DateTime(2022, 9, 6, 11, 5, 7, 183, DateTimeKind.Local).AddTicks(4687), null, 27, (byte)16, 14, 4.892699600613070m },
                    { 89L, true, null, new DateTime(2022, 9, 21, 14, 33, 44, 117, DateTimeKind.Local).AddTicks(9987), null, 22, (byte)10, 56, 1.929412448745880m },
                    { 90L, true, null, new DateTime(2022, 9, 25, 10, 38, 14, 543, DateTimeKind.Local).AddTicks(1356), null, 6, (byte)2, 22, 2.430345827914005m },
                    { 91L, true, null, new DateTime(2022, 9, 14, 10, 42, 21, 885, DateTimeKind.Local).AddTicks(678), null, 20, (byte)7, 38, 2.580866012527080m },
                    { 92L, true, null, new DateTime(2022, 9, 11, 6, 40, 29, 14, DateTimeKind.Local).AddTicks(3154), null, 41, (byte)4, 47, 2.871809920702040m },
                    { 93L, true, null, new DateTime(2022, 11, 2, 12, 12, 31, 306, DateTimeKind.Local).AddTicks(4285), null, 20, (byte)4, 18, 5.565298389441005m },
                    { 94L, true, null, new DateTime(2022, 8, 14, 11, 5, 40, 511, DateTimeKind.Local).AddTicks(8494), null, 21, (byte)7, 89, 3.847949766017475m },
                    { 95L, true, null, new DateTime(2022, 8, 31, 14, 22, 45, 746, DateTimeKind.Local).AddTicks(4898), null, 46, (byte)12, 15, 3.707744868336125m },
                    { 96L, true, null, new DateTime(2022, 10, 13, 3, 47, 3, 3, DateTimeKind.Local).AddTicks(4132), null, 10, (byte)15, 98, 2.465267376725220m },
                    { 97L, true, null, new DateTime(2022, 8, 27, 15, 39, 47, 825, DateTimeKind.Local).AddTicks(1484), null, 31, (byte)2, 14, 5.111392742074745m },
                    { 98L, true, null, new DateTime(2022, 9, 7, 13, 32, 44, 103, DateTimeKind.Local).AddTicks(9965), null, 47, (byte)13, 90, 2.015821127694015m },
                    { 99L, true, null, new DateTime(2022, 10, 15, 9, 28, 54, 57, DateTimeKind.Local).AddTicks(1748), null, 4, (byte)10, 94, 2.114633547195530m },
                    { 100L, true, null, new DateTime(2022, 11, 1, 5, 48, 36, 864, DateTimeKind.Local).AddTicks(7702), null, 14, (byte)12, 34, 1.762875499559045m }
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
                name: "IX_Pracownicy_EtatId",
                table: "Pracownicy",
                column: "EtatId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_StanowiskoId",
                table: "Pracownicy",
                column: "StanowiskoId");

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
                name: "IX_Uczniowie_WychowawcaId",
                table: "Uczniowie",
                column: "WychowawcaId");

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
                name: "Urlopy");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Przedmioty");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Adresy");

            migrationBuilder.DropTable(
                name: "Oddzialy");

            migrationBuilder.DropTable(
                name: "Miejscowosci");

            migrationBuilder.DropTable(
                name: "Panstwa");

            migrationBuilder.DropTable(
                name: "Ulice");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Etaty");

            migrationBuilder.DropTable(
                name: "Stanowiska");
        }
    }
}
