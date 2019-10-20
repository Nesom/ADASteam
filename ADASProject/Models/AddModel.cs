using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class AddModel : PageModel
    {
        public static List<TypeInfo> StandartInfo
            = ReflectionHelper.GetCharacteristicInfo(typeof(Products.ProductInfo));

        public AddModel() { }

        public string Name { get; set; }
        public string[] ProductNames { get; set; }

        public string[] StandartInfoValues { get; set; }
        public string[] Values { get; set; }

        public List<TypeInfo> AdditionalInfo { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task OnPostAsync()
        {
            var file = "abc";
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
        }
    }
}
