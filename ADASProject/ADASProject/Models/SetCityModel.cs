using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class SetCityModel
    {
        public List<string> Cities { get; set; }
        [Required(ErrorMessage = "Minimal length is 3")]
        [MinLength(3)]
        public string City { get; set; }
    }
}
