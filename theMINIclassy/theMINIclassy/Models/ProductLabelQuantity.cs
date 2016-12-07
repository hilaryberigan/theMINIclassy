using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace theMINIclassy.Models
{
    public class ProductLabelQuantity
    {

        [Key]
        public int? Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Label")]
        public int LabelId { get; set; }

        public Label Label { get; set; }

        public int QtyLabelOnProduct { get; set; }

    }
}