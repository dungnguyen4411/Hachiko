using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hachico.Migrations
{
    public partial class intit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagePet_Pets_PetId",
                table: "ImagePet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagePet",
                table: "ImagePet");

            migrationBuilder.RenameTable(
                name: "ImagePet",
                newName: "ImagePets");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ImagePets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagePets",
                table: "ImagePets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePets_PetId",
                table: "ImagePets",
                column: "PetId");
            migrationBuilder.AddForeignKey(
                name: "FK_ImagePets_Pets_PetId",
                table: "ImagePets",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceDetails_SSID",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagePets_Pets_PetId",
                table: "ImagePets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagePets",
                table: "ImagePets");

            migrationBuilder.DropIndex(
                name: "IX_ImagePets_PetId",
                table: "ImagePets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ImagePets");

            migrationBuilder.RenameTable(
                name: "ImagePets",
                newName: "ImagePet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagePet",
                table: "ImagePet",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagePet_Pets_PetId",
                table: "ImagePet",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
