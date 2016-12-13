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

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Display(Name = "Tech Pack Path")]
        public string TechPackPath { get; set; }

        public string Quantity { get; set; }

        [Display(Name = "Minimum Threshold")]
        public decimal MinThreshold { get; set; }

        [Display(Name = "Collection")]
        [ForeignKey("Collection")]
        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        [Display(Name = "Style")]
        [ForeignKey("Style")]
        public int StyleId { get; set; }

        public Style Style { get; set; }

        [Display(Name = "Variation")]
        [ForeignKey("Variation")]
        public int VariationId { get; set; }

        public Variation Variation { get; set; }
    }

}