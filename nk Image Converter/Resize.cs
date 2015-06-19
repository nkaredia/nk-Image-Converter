using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nk_Image_Converter
{
    class Resize
    {


        public int Width { get; set; }
        public int Height { get; set; }

        public const int MAX = 8000;
        public const int MIN = 3;

        public Resize()
        {
            Width = 3;
            Height = 3;
        }


    }
}
