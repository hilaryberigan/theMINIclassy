using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace theMINIclassy.Models
{
    public class ProductFabricQuantity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Fabric")]
        [ForeignKey("Fabric")]
        public int FabricId { get; set; }

        public Fabric Fabric { get; set; }

        [Display(Name = "Qty Fabric on Product")]
        public decimal QtyFabricOnProduct { get; set; }

    }
}