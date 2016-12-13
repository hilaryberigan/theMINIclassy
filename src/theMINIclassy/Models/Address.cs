using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "Apartment Number")]
        public string ApartmentNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }

        public string GetFullAddr
        {
           
             get
            {
                return StreetNumber + " " + ApartmentNumber + " " + StreetName + ", " + City + ", " + State + " " + ZipCode;
            }
        }
    }
}
