using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Service.UserProfile.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "education");

            migrationBuilder.CreateTable(
                name: "userprofile_account",
                schema: "education",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userprofile_account", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "userprofile_question",
                schema: "education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AnswerType = table.Column<string>(type: "text", nullable: false),
                    AnswerName = table.Column<string>(type: "text", nullable: true),
                    AdditionalAnswer = table.Column<bool>(type: "boolean", nullable: true),
                    AnswerData = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userprofile_question", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userprofile_question_Enabled",
                schema: "education",
                table: "userprofile_question",
                column: "Enabled");

            migrationBuilder.CreateIndex(
                name: "IX_userprofile_question_Id",
                schema: "education",
                table: "userprofile_question",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userprofile_account",
                schema: "education");

            migrationBuilder.DropTable(
                name: "userprofile_question",
                schema: "education");
        }
    }
}
