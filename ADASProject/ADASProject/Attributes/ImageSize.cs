using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Attributes
{
    public class ImageSize : Attribute
    {
        public Size Size { get; }

        public ImageSize(int height, int wigth)
        {
            Size = new Size(height, wigth);
        }
    }
}
