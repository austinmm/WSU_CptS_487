using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeAndBake
{
    static class GameConfig
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        //Variable containing the overall speed of the game changed by player input for speed mode
        public static double GameSpeed { get; set; }
    }
}