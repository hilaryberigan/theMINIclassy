using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace theMINIclassy.Data.Migrations
{
    public partial class actualinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fabric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabric", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatternPiece",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternPiece", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ItemSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<string>(nullable: true),
                    OriginatedFrom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatPieceStyle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatPieceId = table.Column<int>(nullable: false),
                    StyleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatPieceStyle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatPieceStyle_PatternPiece_PatPieceId",
                        column: x => x.PatPieceId,
                        principalTable: "PatternPiece",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatPieceStyle_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatPieceVariation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatPieceId = table.Column<int>(nullable: false),
                    VariationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatPieceVariation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatPieceVariation_PatternPiece_PatPieceId",
                        column: x => x.PatPieceId,
                        principalTable: "PatternPiece",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatPieceVariation_Variation_VariationId",
                        column: x => x.VariationId,
                        principalTable: "Variation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    MinThreshold = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    StyleId = table.Column<int>(nullable: false),
                    TechPackPath = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    VariationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Variation_VariationId",
                        column: x => x.VariationId,
                        principalTable: "Variation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFabricQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FabricId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    QtyFabricOnProduct = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFabricQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFabricQuantity_Fabric_FabricId",
                        column: x => x.FabricId,
                        principalTable: "Fabric",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFabricQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLabelQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabelId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    QtyLabelOnProduct = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLabelQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLabelQuantity_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLabelQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductNotionQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotionId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    QtyNotionOnProduct = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNotionQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductNotionQuantity_Notion_NotionId",
                        column: x => x.NotionId,
                        principalTable: "Notion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductNotionQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    QtyProductOnOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTagQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    QtyTagOnProduct = table.Column<decimal>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTagQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTagQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTagQuantity_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "Collection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Collection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collection_CollaboratorId",
                table: "Collection",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_SeasonId",
                table: "Collection",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatPieceStyle_PatPieceId",
                table: "PatPieceStyle",
                column: "PatPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatPieceStyle_StyleId",
                table: "PatPieceStyle",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_PatPieceVariation_PatPieceId",
                table: "PatPieceVariation",
                column: "PatPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatPieceVariation_VariationId",
                table: "PatPieceVariation",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CollectionId",
                table: "Product",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_StyleId",
                table: "Product",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VariationId",
                table: "Product",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFabricQuantity_FabricId",
                table: "ProductFabricQuantity",
                column: "FabricId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFabricQuantity_ProductId",
                table: "ProductFabricQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLabelQuantity_LabelId",
                table: "ProductLabelQuantity",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLabelQuantity_ProductId",
                table: "ProductLabelQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNotionQuantity_NotionId",
                table: "ProductNotionQuantity",
                column: "NotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNotionQuantity_ProductId",
                table: "ProductNotionQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_OrderId",
                table: "ProductQuantity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_ProductId",
                table: "ProductQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagQuantity_ProductId",
                table: "ProductTagQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagQuantity_TagId",
                table: "ProductTagQuantity",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Collaborator_CollaboratorId",
                table: "Collection",
                column: "CollaboratorId",
                principalTable: "Collaborator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Season_SeasonId",
                table: "Collection",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Collaborator_CollaboratorId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Season_SeasonId",
                table: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Collection_CollaboratorId",
                table: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Collection_SeasonId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Collection");

            migrationBuilder.DropTable(
                name: "Collaborator");

            migrationBuilder.DropTable(
                name: "PatPieceStyle");

            migrationBuilder.DropTable(
                name: "PatPieceVariation");

            migrationBuilder.DropTable(
                name: "ProductFabricQuantity");

            migrationBuilder.DropTable(
                name: "ProductLabelQuantity");

            migrationBuilder.DropTable(
                name: "ProductNotionQuantity");

            migrationBuilder.DropTable(
                name: "ProductQuantity");

            migrationBuilder.DropTable(
                name: "ProductTagQuantity");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "PatternPiece");

            migrationBuilder.DropTable(
                name: "Fabric");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Notion");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropTable(
                name: "Variation");
        }
    }
}
