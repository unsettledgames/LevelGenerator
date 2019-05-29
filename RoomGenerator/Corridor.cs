using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Corridor : Cave
    {
        public Corridor(int width, int height, Corner topLeft) : base(width, height, topLeft)
        {
        }

        public void AddToMatrix(int[][] level)
        {
            AddToMatrix(level, id);
        }
    }
}
