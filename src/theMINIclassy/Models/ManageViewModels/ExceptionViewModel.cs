using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models.ManageViewModels
{
    public class ExceptionViewModel
    {
        public string[] errors { get; set; }

        public Product Product { get; set; }
    }
}
