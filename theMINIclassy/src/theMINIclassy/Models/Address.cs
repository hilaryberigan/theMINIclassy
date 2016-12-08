using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;



namespace theMINIclassy.Models
{
    public class Address
        
    {
        [Key]
        public int? Id { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string ApartmentNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

    }


}