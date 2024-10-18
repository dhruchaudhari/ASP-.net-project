using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShoppingCartMvcUI.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Book_BookId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Book_BookId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Book_BookId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Stock",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_BookId",
                table: "Stock",
                newName: "IX_Stock_FoodId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "OrderDetail",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_BookId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_FoodId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "CartDetail",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_BookId",
                table: "CartDetail",
                newName: "IX_CartDetail_FoodId");

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuisineName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RestorantName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuisineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_Cuisines_CuisineId",
                        column: x => x.CuisineId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_CuisineId",
                table: "Food",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Food_FoodId",
                table: "CartDetail",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                table: "OrderDetail",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Food_FoodId",
                table: "Stock",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Food_FoodId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Food_FoodId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "Stock",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_FoodId",
                table: "Stock",
                newName: "IX_Stock_BookId");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "OrderDetail",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_FoodId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_BookId");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "CartDetail",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_FoodId",
                table: "CartDetail",
                newName: "IX_CartDetail_BookId");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Book_BookId",
                table: "CartDetail",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Book_BookId",
                table: "OrderDetail",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Book_BookId",
                table: "Stock",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
