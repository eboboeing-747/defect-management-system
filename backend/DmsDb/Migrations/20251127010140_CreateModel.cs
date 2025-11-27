using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmsDb.Migrations
{
    /// <inheritdoc />
    public partial class CreateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefectComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Contents = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PfpPath = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EstateObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateObjectImages_EstateObjects_EstateObjectId",
                        column: x => x.EstateObjectId,
                        principalTable: "EstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalPosterId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstateObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defects_EstateObjects_EstateObjectId",
                        column: x => x.EstateObjectId,
                        principalTable: "EstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Defects_Users_OriginalPosterId",
                        column: x => x.OriginalPosterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectEntityUserEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstateObjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectEntityUserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateObjectEntityUserEntity_EstateObjects_EstateObjectId",
                        column: x => x.EstateObjectId,
                        principalTable: "EstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateObjectEntityUserEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectCommentEntityDefectEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectCommentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectCommentEntityDefectEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefectCommentEntityDefectEntity_DefectComments_DefectCommen~",
                        column: x => x.DefectCommentId,
                        principalTable: "DefectComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefectCommentEntityDefectEntity_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefectImages_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefectCommentEntityDefectEntity_DefectCommentId",
                table: "DefectCommentEntityDefectEntity",
                column: "DefectCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectCommentEntityDefectEntity_DefectId",
                table: "DefectCommentEntityDefectEntity",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectImages_DefectId",
                table: "DefectImages",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_EstateObjectId",
                table: "Defects",
                column: "EstateObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_OriginalPosterId",
                table: "Defects",
                column: "OriginalPosterId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateObjectEntityUserEntity_EstateObjectId",
                table: "EstateObjectEntityUserEntity",
                column: "EstateObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateObjectEntityUserEntity_UserId",
                table: "EstateObjectEntityUserEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateObjectImages_EstateObjectId",
                table: "EstateObjectImages",
                column: "EstateObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefectCommentEntityDefectEntity");

            migrationBuilder.DropTable(
                name: "DefectImages");

            migrationBuilder.DropTable(
                name: "EstateObjectEntityUserEntity");

            migrationBuilder.DropTable(
                name: "EstateObjectImages");

            migrationBuilder.DropTable(
                name: "DefectComments");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "EstateObjects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
