using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using theMINIclassy.Data;

namespace theMINIclassy.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161221172649_saves")]
    partial class saves
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("theMINIclassy.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApartmentNumber");

                    b.Property<string>("City");

                    b.Property<string>("State");

                    b.Property<string>("StreetName");

                    b.Property<string>("StreetNumber");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("theMINIclassy.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("theMINIclassy.Models.ClassType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("ClassType");
                });

            modelBuilder.Entity("theMINIclassy.Models.Collaborator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Collaborator");
                });

            modelBuilder.Entity("theMINIclassy.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<int>("CollaboratorId");

                    b.Property<string>("Month");

                    b.Property<int>("SeasonId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("theMINIclassy.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("theMINIclassy.Models.Fabric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Content");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MinThreshold");

                    b.Property<decimal>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Fabric");
                });

            modelBuilder.Entity("theMINIclassy.Models.ImagePath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FilePath");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ImagePath");
                });

            modelBuilder.Entity("theMINIclassy.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MinThreshold");

                    b.Property<decimal>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("theMINIclassy.Models.Notion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MinThreshold");

                    b.Property<decimal>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Notion");
                });

            modelBuilder.Entity("theMINIclassy.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderNumber");

                    b.Property<string>("OrderStatus");

                    b.Property<string>("OriginatedFrom");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("theMINIclassy.Models.PatPieceStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PatPieceId");

                    b.Property<int>("StyleId");

                    b.HasKey("Id");

                    b.HasIndex("PatPieceId");

                    b.HasIndex("StyleId");

                    b.ToTable("PatPieceStyle");
                });

            modelBuilder.Entity("theMINIclassy.Models.PatPieceVariation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PatPieceId");

                    b.Property<int>("VariationId");

                    b.HasKey("Id");

                    b.HasIndex("PatPieceId");

                    b.HasIndex("VariationId");

                    b.ToTable("PatPieceVariation");
                });

            modelBuilder.Entity("theMINIclassy.Models.PatternPiece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("PatternPiece");
                });

            modelBuilder.Entity("theMINIclassy.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CollectionId");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<decimal>("MinThreshold");

                    b.Property<int>("Quantity");

                    b.Property<string>("SKU");

                    b.Property<int>("StyleId");

                    b.Property<string>("TechPackPath");

                    b.Property<string>("Title");

                    b.Property<int>("VariationId");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("StyleId");

                    b.HasIndex("VariationId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductFabricQuantity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FabricId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("QtyFabricOnProduct");

                    b.HasKey("Id");

                    b.HasIndex("FabricId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFabricQuantity");
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductLabelQuantity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LabelId");

                    b.Property<int>("ProductId");

                    b.Property<int>("QtyLabelOnProduct");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductLabelQuantity");
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductNotionQuantity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NotionId");

                    b.Property<int>("ProductId");

                    b.Property<int>("QtyNotionOnProduct");

                    b.HasKey("Id");

                    b.HasIndex("NotionId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductNotionQuantity");
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductQuantity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("QtyProductOnOrder");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductQuantity");
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductTagQuantity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<decimal>("QtyTagOnProduct");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTagQuantity");
                });

            modelBuilder.Entity("theMINIclassy.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("theMINIclassy.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ItemSize");

                    b.HasKey("Id");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("theMINIclassy.Models.Style", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Style");
                });

            modelBuilder.Entity("theMINIclassy.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MinThreshold");

                    b.Property<decimal>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("theMINIclassy.Models.Variation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Variation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("theMINIclassy.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("theMINIclassy.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.Collection", b =>
                {
                    b.HasOne("theMINIclassy.Models.Collaborator", "Collaborator")
                        .WithMany()
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.Customer", b =>
                {
                    b.HasOne("theMINIclassy.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ImagePath", b =>
                {
                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("theMINIclassy.Models.Order", b =>
                {
                    b.HasOne("theMINIclassy.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.PatPieceStyle", b =>
                {
                    b.HasOne("theMINIclassy.Models.PatternPiece", "PatternPiece")
                        .WithMany()
                        .HasForeignKey("PatPieceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Style", "Style")
                        .WithMany()
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.PatPieceVariation", b =>
                {
                    b.HasOne("theMINIclassy.Models.PatternPiece", "PatternPiece")
                        .WithMany()
                        .HasForeignKey("PatPieceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Variation", "Variation")
                        .WithMany()
                        .HasForeignKey("VariationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.Product", b =>
                {
                    b.HasOne("theMINIclassy.Models.Collection", "Collection")
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Style", "Style")
                        .WithMany()
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Variation", "Variation")
                        .WithMany()
                        .HasForeignKey("VariationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductFabricQuantity", b =>
                {
                    b.HasOne("theMINIclassy.Models.Fabric", "Fabric")
                        .WithMany()
                        .HasForeignKey("FabricId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductLabelQuantity", b =>
                {
                    b.HasOne("theMINIclassy.Models.Label", "Label")
                        .WithMany()
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductNotionQuantity", b =>
                {
                    b.HasOne("theMINIclassy.Models.Notion", "Notion")
                        .WithMany()
                        .HasForeignKey("NotionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductQuantity", b =>
                {
                    b.HasOne("theMINIclassy.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("theMINIclassy.Models.ProductTagQuantity", b =>
                {
                    b.HasOne("theMINIclassy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("theMINIclassy.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
