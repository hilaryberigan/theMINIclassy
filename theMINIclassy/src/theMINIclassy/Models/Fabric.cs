using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using theMINIclassy.WorkerClasses;

namespace theMINIclassy.Models
{
    public class Fabric : Supply
    {
        public string Content { get; set; }

        public string Code { get; set; }
    }
}
