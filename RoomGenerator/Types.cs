using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Types
    {
        public delegate List<Corner> CornerListGetterForCorridor(int corridorWidth, int corridorHeight);
        public struct NBlocksArea
        {
            int nBlocks;
            int area;
        }
    }
}
