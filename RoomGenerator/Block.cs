using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Block : Cave
    {
        public Block(int width, int height, Corner topLeft) : base(width, height, topLeft)
        {
        }

        public void AddToMatrix(int[][] level, PerlinNoise noiseGenerator)
        {
            AddToMatrix(level, -1, noiseGenerator);
        }
    }
}
