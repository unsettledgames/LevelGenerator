using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/** Infos?
 * 
 * - The width of the corridors can't be bigger than the minimum width of a room
 * - 
 * 
 */ 

namespace RoomGenerator
{
    class Consts
    {
        public const int NORTH =   0;
        public const int EAST =    1;
        public const int SOUTH =   2;
        public const int WEST =    3;

        public const int TOP_LEFT      =   0;
        public const int TOP_RIGHT     =   1;
        public const int BOTTOM_RIGHT  =   2;
        public const int BOTTOM_LEFT   =   3;

        public const int MAX_LEVEL_WIDTH = 700;
        public const int MAX_LEVEL_HEIGHT = 700;

        public const int SIDE_COUNT = 4;

        public const int MIN_BLOCK_DIVIDER = 3;
        public const int BLOCK_TO_SUB = 3;


        public const float NOISE_INCREASE = 0.9999f;
    }
}
