using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace theMINIclassy.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        [Display(Name = "Originated From")]
        public string OriginatedFrom { get; set; }

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

    }
}