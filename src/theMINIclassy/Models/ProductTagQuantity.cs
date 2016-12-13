using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class ProductTagQuantity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Tag")]
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        public Tag Tag { get; set; }

        [Display(Name = "Qty Tag on Product")]
        public decimal QtyTagOnProduct { get; set; }

    }
}