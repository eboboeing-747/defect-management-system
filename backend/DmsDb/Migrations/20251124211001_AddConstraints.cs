using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmsDb.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefectCommentEntities");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEstateObjectEntities",
                table: "UserEstateObjectEntities");

            migrationBuilder.RenameTable(
                name: "UserEstateObjectEntities",
                newName: "UserEstateObjects");

            migrationBuilder.RenameColumn(
                name: "ObjectId",
                table: "Defects",
                newName: "EstateObjectId");

            migrationBuilder.RenameColumn(
                name: "EstateObjectId",
                table: "UserEstateObjects",
                newName: "EsatateObjectId");

            migrationBuilder.AlterColumn<string>(
                name: "PfpPath",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEstateObjects",
                table: "UserEstateObjects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DefectImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectEntityUserEntity",
                columns: table => new
                {
                    EstateObjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectEntityUserEntity", x => new { x.EstateObjectsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EstateObjectEntityUserEntity_EstateObjects_EstateObjectsId",
                        column: x => x.EstateObjectsId,
                        principalTable: "EstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateObjectEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EstateObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    EstateObjectEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateObjectImages_EstateObjects_EstateObjectEntityId",
                        column: x => x.EstateObjectEntityId,
                        principalTable: "EstateObjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Defects_EstateObjectId",
                table: "Defects",
                column: "EstateObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_OriginalPosterId",
                table: "Defects",
                column: "OriginalPosterId");

            migrationBuilder.CreateIndex(
                name: "IX_DefectImages_DefectId",
                table: "DefectImages",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateObjectEntityUserEntity_UsersId",
                table: "EstateObjectEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateObjectImages_EstateObjectEntityId",
                table: "EstateObjectImages",
                column: "EstateObjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_EstateObjects_EstateObjectId",
                table: "Defects",
                column: "EstateObjectId",
                principalTable: "EstateObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Users_OriginalPosterId",
                table: "Defects",
                column: "OriginalPosterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_EstateObjects_EstateObjectId",
                table: "Defects");

            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Users_OriginalPosterId",
                table: "Defects");

            migrationBuilder.DropTable(
                name: "DefectImages");

            migrationBuilder.DropTable(
                name: "EstateObjectEntityUserEntity");

            migrationBuilder.DropTable(
                name: "EstateObjectImages");

            migrationBuilder.DropIndex(
                name: "IX_Defects_EstateObjectId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_OriginalPosterId",
                table: "Defects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEstateObjects",
                table: "UserEstateObjects");

            migrationBuilder.RenameTable(
                name: "UserEstateObjects",
                newName: "UserEstateObjectEntities");

            migrationBuilder.RenameColumn(
                name: "EstateObjectId",
                table: "Defects",
                newName: "ObjectId");

            migrationBuilder.RenameColumn(
                name: "EsatateObjectId",
                table: "UserEstateObjectEntities",
                newName: "EstateObjectId");

            migrationBuilder.AlterColumn<string>(
                name: "PfpPath",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEstateObjectEntities",
                table: "UserEstateObjectEntities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DefectCommentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectCommentEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Desctiption = table.Column<string>(type: "text", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }
    }
}
