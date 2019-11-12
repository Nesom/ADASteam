using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject
{
    public struct Size
    {
        public Size(int height, int wigth)
        {
            Height = height;
            Wigth = wigth;
        }

        public int Height { get; }
        public int Wigth { get; }
    }
}
