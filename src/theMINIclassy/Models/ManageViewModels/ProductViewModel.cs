using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models.ManageViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public List<Fabric> Fabrics { get; set; }

        public List<ProductFabricQuantity> PFQuantities { get; set; }

        public List<Tag> Tags { get; set; }

        public List<ProductTagQuantity> PTQuantities { get; set; }
        
        public List<Label> Labels { get; set; }

        public List<ProductLabelQuantity> PLQuantities { get; set; }

        public List<Notion> Notions { get; set; }

        public List<ProductNotionQuantity> PNQuantities { get; set; }

        public List<Collection> Collections { get; set; }

        public List<Style> Styles { get; set; }

        public List<Variation> Variations { get; set; }

        public Collection Collection { get; set; }
    }
}
