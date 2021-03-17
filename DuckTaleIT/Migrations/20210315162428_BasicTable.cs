using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DuckTaleIT.Migrations
{
    public partial class BasicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MasterClass",
                schema: "dbo",
                columns: table => new
                {
                    mClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    mClassName = table.Column<string>(nullable: true),
                    mClassCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterClass", x => x.mClassId);
                });

            migrationBuilder.CreateTable(
                name: "MasterSubject",
                schema: "dbo",
                columns: table => new
                {
                    mSubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    mSubjectName = table.Column<string>(nullable: true),
                    mSubjectCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSubject", x => x.mSubjectId);
                });

            migrationBuilder.CreateTable(
                name: "MasterStudent",
                schema: "dbo",
                columns: table => new
                {
                    StuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    mClassName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterStudent", x => x.StuId);
                    table.ForeignKey(
                        name: "FK_MasterStudent_MasterClass_mClassName",
                        column: x => x.mClassName,
                        principalSchema: "dbo",
                        principalTable: "MasterClass",
                        principalColumn: "mClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStudentWise",
                schema: "dbo",
                columns: table => new
                {
                    sswId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sswStudentId = table.Column<int>(nullable: false),
                    sswSubjectId = table.Column<int>(nullable: false),
                    sswStudentMarks = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStudentWise", x => x.sswId);
                    table.ForeignKey(
                        name: "FK_SubjectStudentWise_MasterStudent_sswStudentId",
                        column: x => x.sswStudentId,
                        principalSchema: "dbo",
                        principalTable: "MasterStudent",
                        principalColumn: "StuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectStudentWise_MasterSubject_sswSubjectId",
                        column: x => x.sswSubjectId,
                        principalSchema: "dbo",
                        principalTable: "MasterSubject",
                        principalColumn: "mSubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterStudent_mClassName",
                schema: "dbo",
                table: "MasterStudent",
                column: "mClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudentWise_sswStudentId",
                schema: "dbo",
                table: "SubjectStudentWise",
                column: "sswStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudentWise_sswSubjectId",
                schema: "dbo",
                table: "SubjectStudentWise",
                column: "sswSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectStudentWise",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MasterStudent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MasterSubject",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MasterClass",
                schema: "dbo");
        }
    }
}
