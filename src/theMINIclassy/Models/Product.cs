using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string SKU { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string TechPackPath { get; set; }

        public string Quantity { get; set; }

        public decimal MinThreshold { get; set; }

        [ForeignKey("Collection")]
        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        [ForeignKey("Style")]
        public int StyleId { get; set; }

        public Style Style { get; set; }

        [ForeignKey("Variation")]
        public int VariationId { get; set; }

        public Variation Variation { get; set; }
    }

}