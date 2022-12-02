using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCasino.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lugar = table.Column<int>(type: "int", nullable: false),
                    Recompensa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RifaRefId = table.Column<int>(type: "int", nullable: false),
                    RifaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Rifas_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RifaParticipantes",
                columns: table => new
                {
                    RifaParticipanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RifaRefId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteRefId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CartaRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RifaParticipantes", x => x.RifaParticipanteId);
                    table.ForeignKey(
                        name: "FK_RifaParticipantes_AspNetUsers_ParticipanteRefId",
                        column: x => x.ParticipanteRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RifaParticipantes_Cartas_CartaRefId",
                        column: x => x.CartaRefId,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RifaParticipantes_Rifas_RifaRefId",
                        column: x => x.RifaRefId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ganadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RifaRefId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteRefId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PremioRefId = table.Column<int>(type: "int", nullable: false),
                    HelpLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ganadores_AspNetUsers_ParticipanteRefId",
                        column: x => x.ParticipanteRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ganadores_Premios_PremioRefId",
                        column: x => x.PremioRefId,
                        principalTable: "Premios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ganadores_Rifas_RifaRefId",
                        column: x => x.RifaRefId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cartas",
                columns: new[] { "Id", "CartaId", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "El Gallo " },
                    { 2, 2, "El Diablo " },
                    { 3, 3, "La Dama " },
                    { 4, 4, "El Catrin " },
                    { 5, 5, "El Paraguas " },
                    { 6, 6, "La Sirena " },
                    { 7, 7, "La Escalera " },
                    { 8, 8, "La Botella " },
                    { 9, 9, "El Barril " },
                    { 10, 10, "El Arbol " },
                    { 11, 11, "El Melon " },
                    { 12, 12, "El Valiente " },
                    { 13, 13, "El Gorrito " },
                    { 14, 14, "La Muerte " },
                    { 15, 15, "La Pera " },
                    { 16, 16, "La Bandera " },
                    { 17, 17, "El Bandolon " },
                    { 18, 18, "El Violoncello " },
                    { 19, 19, "La Garza " },
                    { 20, 20, "El Pajaro " },
                    { 21, 21, "La Mano " },
                    { 22, 22, "La Bota " },
                    { 23, 23, "La Luna " },
                    { 24, 24, "El Cotorro " },
                    { 25, 25, "El Borracho " },
                    { 26, 26, "El Negrito " },
                    { 27, 27, "El Corazon " },
                    { 28, 28, "La Sandia " },
                    { 29, 29, "El Tambor " },
                    { 30, 30, "El Camaron " },
                    { 31, 31, "Las Jaras " },
                    { 32, 32, "El Musico " },
                    { 33, 33, "La Araña " },
                    { 34, 34, "El Soldado " },
                    { 35, 35, "La Estrella " },
                    { 36, 36, "El Cazo " },
                    { 37, 37, "El Mundo " },
                    { 38, 38, "El Apache " },
                    { 39, 39, "El Nopal " },
                    { 40, 40, "El Alacran " },
                    { 41, 41, "La Rosa " },
                    { 42, 42, "La Calavera " }
                });

            migrationBuilder.InsertData(
                table: "Cartas",
                columns: new[] { "Id", "CartaId", "Nombre" },
                values: new object[,]
                {
                    { 43, 43, "La Campana " },
                    { 44, 44, "El Cantarito " },
                    { 45, 45, "El Venado " },
                    { 46, 46, "El Sol " },
                    { 47, 47, "La Corona " },
                    { 48, 48, "La Chalupa " },
                    { 49, 49, "El Pino " },
                    { 50, 50, "El Pescado " },
                    { 51, 51, "La Palma " },
                    { 52, 52, "La Maceta " },
                    { 53, 53, "El Arpa " },
                    { 54, 54, "La Rana " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ganadores_ParticipanteRefId",
                table: "Ganadores",
                column: "ParticipanteRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganadores_PremioRefId",
                table: "Ganadores",
                column: "PremioRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganadores_RifaRefId",
                table: "Ganadores",
                column: "RifaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Premios_RifaId",
                table: "Premios",
                column: "RifaId");

            migrationBuilder.CreateIndex(
                name: "IX_RifaParticipantes_CartaRefId",
                table: "RifaParticipantes",
                column: "CartaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_RifaParticipantes_ParticipanteRefId",
                table: "RifaParticipantes",
                column: "ParticipanteRefId");

            migrationBuilder.CreateIndex(
                name: "IX_RifaParticipantes_RifaRefId",
                table: "RifaParticipantes",
                column: "RifaRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ganadores");

            migrationBuilder.DropTable(
                name: "RifaParticipantes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cartas");

            migrationBuilder.DropTable(
                name: "Rifas");
        }
    }
}
