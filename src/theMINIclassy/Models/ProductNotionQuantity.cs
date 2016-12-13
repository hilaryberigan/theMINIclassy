using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class ProductNotionQuantity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Notion")]
        [ForeignKey("Notion")]
        public int NotionId { get; set; }

        public Notion Notion { get; set; }

        [Display(Name = "Qty Notion on Product")]
        public int QtyNotionOnProduct { get; set; }

    }
}