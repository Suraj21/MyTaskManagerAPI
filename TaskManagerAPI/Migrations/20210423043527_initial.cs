using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerAPI.Migrations
{
    public partial class initial : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false),
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
                name: "ClientLocations",
                columns: table => new
                {
                    ClientLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientLocationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLocations", x => x.ClientLocationID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<Guid>",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<Guid>", x => new { x.UserId, x.RoleId });
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
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                    table.ForeignKey(
                        name: "FK_Skills_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamSize = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientLocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_ClientLocations_ClientLocationID",
                        column: x => x.ClientLocationID,
                        principalTable: "ClientLocations",
                        principalColumn: "ClientLocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientLocations",
                columns: new[] { "ClientLocationID", "ClientLocationName" },
                values: new object[,]
                {
                    { 1, "Boston" },
                    { 2, "New Delhi" },
                    { 3, "New Jersy" },
                    { 4, "New York" },
                    { 5, "London" },
                    { 6, "Tokyo" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { 124, "Palestine" },
                    { 125, "Croatia" },
                    { 126, "Bosnia and Herzegovina" },
                    { 127, "Kuwait" },
                    { 128, "Moldova" },
                    { 129, "Liberia" },
                    { 130, "Mauritania" },
                    { 131, "Panama" },
                    { 132, "Uruguay" },
                    { 133, "Armenia" },
                    { 136, "Oman" },
                    { 135, "Albania" },
                    { 123, "Lebanon" },
                    { 137, "Mongolia" },
                    { 138, "Jamaica" },
                    { 139, "Lesotho" },
                    { 140, "Namibia" },
                    { 141, "Macedonia" },
                    { 142, "Slovenia" },
                    { 143, "Latvia" },
                    { 134, "Lithuania" },
                    { 122, "Republic of the Congo" },
                    { 119, "Ireland" },
                    { 120, "Georgia" },
                    { 99, "Serbia" },
                    { 100, "Papua New Guinea" },
                    { 101, "Paraguay" },
                    { 102, "Laos" },
                    { 103, "Libya" },
                    { 104, "Jordan" },
                    { 105, "Sierra Leone" },
                    { 106, "Togo" },
                    { 107, "El Salvador" },
                    { 108, "Nicaragua" },
                    { 109, "Eritrea" },
                    { 110, "Denmark" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { 111, "Kyrgyzstan" },
                    { 112, "Slovakia" },
                    { 113, "Finland" },
                    { 114, "Singapore" },
                    { 115, "Turkmenistan" },
                    { 116, "Norway" },
                    { 117, "Costa Rica" },
                    { 118, "Central African Republic" },
                    { 144, "Botswana" },
                    { 121, "New Zealand" },
                    { 145, "Qatar" },
                    { 148, "Guinea-Bissau" },
                    { 147, "Gabon" },
                    { 173, "Belize" },
                    { 174, "Barbados" },
                    { 175, "Vanuatu" },
                    { 176, "Samoa" },
                    { 177, "Saint Lucia" },
                    { 178, "Kiribati" },
                    { 179, "Grenada" },
                    { 180, "Tonga" },
                    { 181, "Federated States of Micronesia" },
                    { 172, "Iceland" },
                    { 182, "Saint Vincent and the Grenadines" },
                    { 184, "Antigua and Barbuda" },
                    { 185, "Andorra" },
                    { 186, "Dominica" },
                    { 187, "Liechtenstein" },
                    { 188, "Monaco" },
                    { 189, "San Marino" },
                    { 190, "Palau" },
                    { 191, "Tuvalu" },
                    { 192, "Nauru" },
                    { 183, "Seychelles" },
                    { 171, "Maldives" },
                    { 170, "Bahamas" },
                    { 169, "Brunei" },
                    { 98, "Bulgaria" },
                    { 149, "Trinidad and Tobago" },
                    { 150, "Estonia" },
                    { 151, "Mauritius" },
                    { 152, "Swaziland" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { 153, "Bahrain" },
                    { 154, "Timor-Leste" },
                    { 155, "Cyprus" },
                    { 156, "Fiji" },
                    { 157, "Djibouti" },
                    { 158, "Guyana" },
                    { 159, "Equatorial Guinea" },
                    { 160, "Bhutan" },
                    { 161, "Comoros" },
                    { 162, "Montenegro" },
                    { 163, "Western Sahara" },
                    { 164, "Suriname" },
                    { 165, "Luxembourg" },
                    { 166, "Solomon Islands" },
                    { 167, "Cape Verde" },
                    { 168, "Malta" },
                    { 146, "Gambia" },
                    { 97, "Tajikistan" },
                    { 94, "Burundi" },
                    { 95, "Switzerland" },
                    { 26, "Spain" },
                    { 27, "Colombia" },
                    { 28, "Ukraine" },
                    { 29, "Tanzania" },
                    { 30, "Argentina" },
                    { 31, "Kenya" },
                    { 32, "Poland" },
                    { 33, "Algeria" },
                    { 34, "Canada" },
                    { 25, "Myanmar" },
                    { 35, "Uganda" },
                    { 37, "Morocco" },
                    { 38, "Sudan" },
                    { 39, "Peru" },
                    { 40, "Malaysia" },
                    { 41, "Uzbekistan" },
                    { 42, "Saudi Arabia" },
                    { 43, "Venezuela" },
                    { 44, "Nepal" },
                    { 45, "Afghanistan" },
                    { 36, "Iraq" },
                    { 24, "South Korea" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { 23, "South Africa" },
                    { 22, "Italy" },
                    { 1, "China" },
                    { 2, "United States" },
                    { 3, "Indonesia" },
                    { 4, "Brazil" },
                    { 5, "Pakistan" },
                    { 6, "Nigeria" },
                    { 7, "Bangladesh" },
                    { 8, "Russia" },
                    { 9, "Japan" },
                    { 10, "Mexico" },
                    { 11, "Philippines" },
                    { 12, "Vietnam" },
                    { 13, "Ethiopia" },
                    { 14, "Egypt" },
                    { 15, "Germany" },
                    { 16, "Iran" },
                    { 17, "Turkey" },
                    { 18, "Democratic Republic of the Congo" },
                    { 19, "Thailand" },
                    { 20, "France" },
                    { 21, "United Kingdom" },
                    { 46, "Ghana" },
                    { 96, "Israel" },
                    { 47, "Yemen" },
                    { 49, "Mozambique" },
                    { 75, "Guinea" },
                    { 76, "Greece" },
                    { 77, "Tunisia" },
                    { 78, "Portugal" },
                    { 79, "Rwanda" },
                    { 80, "Czech Republic" },
                    { 81, "Haiti" },
                    { 82, "Bolivia" },
                    { 83, "Somalia" },
                    { 74, "Belgium" },
                    { 84, "Hungary" },
                    { 86, "Sweden" },
                    { 87, "Belarus" },
                    { 88, "Dominican Republic" },
                    { 89, "Azerbaijan" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { 90, "Austria" },
                    { 91, "Honduras" },
                    { 92, "United Arab Emirates" },
                    { 93, "South Sudan" },
                    { 193, "Vatican City" },
                    { 85, "Benin" },
                    { 73, "Cuba" },
                    { 72, "Chad" },
                    { 71, "Zimbabwe" },
                    { 50, "Taiwan" },
                    { 51, "Australia" },
                    { 52, "Syria" },
                    { 53, "Ivory Coast" },
                    { 54, "Madagascar" },
                    { 55, "Angola" },
                    { 56, "Sri Lanka" },
                    { 57, "Cameroon" },
                    { 58, "Romania" },
                    { 59, "Kazakhstan" },
                    { 60, "Netherlands" },
                    { 61, "Chile" },
                    { 62, "Niger" },
                    { 63, "Burkina Faso" },
                    { 64, "Ecuador" },
                    { 65, "Guatemala" },
                    { 66, "Mali" },
                    { 67, "Malawi" },
                    { 68, "Senegal" },
                    { 69, "Cambodia" },
                    { 70, "Zambia" },
                    { 48, "North Korea" },
                    { 194, "India" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectID", "Active", "ClientLocationID", "DateOfStart", "ProjectName", "Status", "TeamSize" },
                values: new object[] { 2, true, 1, "2018-3-16", "Reporting Tool", "Support", 81 });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectID", "Active", "ClientLocationID", "DateOfStart", "ProjectName", "Status", "TeamSize" },
                values: new object[] { 1, true, 2, "2017-8-1", "Hospital Management System", "In Force", 14 });

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
                name: "IX_Projects_ClientLocationID",
                table: "Projects",
                column: "ClientLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Id",
                table: "Skills",
                column: "Id");
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
                name: "Countries");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<Guid>");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ClientLocations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
