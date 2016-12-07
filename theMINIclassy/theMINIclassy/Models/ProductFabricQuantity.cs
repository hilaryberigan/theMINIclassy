using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace theMINIclassy.Models
{
    public class ProductFabricQuantity
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Fabric")]
        public int FabricId { get; set; }

        public Fabric Fabric { get; set; }

        public decimal QtyFabricOnProduct { get; set; }

    }
}