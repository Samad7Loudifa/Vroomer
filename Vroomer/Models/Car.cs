using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vroomer.Models
{
    

    public class Car
    {
        [Key]
        //[Range(1,100)]
        public int id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Price per day is RequiredAttribute ")]
        public decimal PricePerDAY { get; set; }
    }

}
