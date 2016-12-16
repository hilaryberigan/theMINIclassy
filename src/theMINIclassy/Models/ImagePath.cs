using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models
{
    public class ImagePath
    {
        [Key]
        public int Id { get; set; }

        public string FilePath { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

    }
}
