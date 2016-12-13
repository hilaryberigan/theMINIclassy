using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class ProductLabelQuantity
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Label")]
        [ForeignKey("Label")]
        public int LabelId { get; set; }

        public Label Label { get; set; }

        [Display(Name = "Qty Label on Product")]
        public int QtyLabelOnProduct { get; set; }

    }
}