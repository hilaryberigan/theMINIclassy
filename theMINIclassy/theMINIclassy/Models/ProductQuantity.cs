using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace theMINIclassy.Models
{
    public class ProductQuantity
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Fabric")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int QtyProductOnOrder { get; set; }

    }
}