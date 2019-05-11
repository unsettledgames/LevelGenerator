using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/** PROTOCOLLO?
 * 
 * - La larghezza dei corridoi non può essere maggiore della larghezza minima di una stanza
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

        public const int MAX_LEVEL_WIDTH = 500;
        public const int MAX_LEVEL_HEIGHT = 500;

        public const int SIDE_COUNT = 4;
    }
}
