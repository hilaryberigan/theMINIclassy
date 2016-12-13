using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class ProductQuantity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Order")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Display(Name = "Qty Product on Order")]
        public int QtyProductOnOrder { get; set; }

    }
}