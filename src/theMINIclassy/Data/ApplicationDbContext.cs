using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Models;

namespace theMINIclassy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<ClassType> ClassType { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Fabric> Fabric { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<Notion> Notion { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PatPieceStyle> PatPieceStyle { get; set; }
        public DbSet<PatPieceVariation> PatPieceVariation { get; set; }
        public DbSet<PatternPiece> PatternPiece { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductFabricQuantity> ProductFabricQuantity { get; set; }
        public DbSet<ProductLabelQuantity> ProductLabelQuantity { get; set; }
        public DbSet<ProductNotionQuantity> ProductNotionQuantity { get; set; }
        public DbSet<ProductQuantity> ProductQuantity { get; set; }
        public DbSet<ProductTagQuantity> ProductTagQuantity { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Style> Style { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Variation> Variation { get; set; }
        public DbSet<ImagePath> ImagePath { get; set; }




    }
}