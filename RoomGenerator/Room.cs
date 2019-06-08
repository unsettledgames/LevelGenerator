using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Room : Cave
    {
        private List<Corridor> corridors;
        private int nCorridors;
        private int maxCorridors;
        private static Random random = new Random();

        public bool[] corridorAdjaciencies;

        public Room (int width, int height, Corner topLeft) : base(width, height, topLeft)
        {
            corridors = new List<Corridor>();
            nCorridors = 0;

            corridorAdjaciencies = new bool[Consts.SIDE_COUNT];

            corridorAdjaciencies[Consts.NORTH] = false;
            corridorAdjaciencies[Consts.EAST] = false;
            corridorAdjaciencies[Consts.SOUTH] = false;
            corridorAdjaciencies[Consts.WEST] = false;

            maxCorridors = random.Next(0, 101);

            if (maxCorridors > 90)
            {
                maxCorridors = 4;
            }
            else if (maxCorridors > 80)
            {
                maxCorridors = 3;
            }
            else if (maxCorridors > 50)
            {
                maxCorridors = 2;
            }
            else
            {
                maxCorridors = 1;
            }
        }

        public int GetMaxCorridors()
        {
            return maxCorridors;
        }
        public List<Corridor> GetCorridors()
        {
            return corridors;
        }

        public void AddCorridor(Corridor toAdd)
        {
            corridors.Add(toAdd);
            nCorridors++;
        }

        public int GetNCorridors()
        {
            return nCorridors;
        }

        public void AddToMatrix(int[][] level, PerlinNoise noiseGenerator)
        {
            AddToMatrix(level, id, noiseGenerator);

            Random random = new Random();
            int blockWidth;
            int blockHeight;
            int blockCornerX;
            int blockCornerY;
            int currentX;
            int tollerance = 2;
            int roomCornerX = corners[Consts.TOP_LEFT].GetX();
            int roomCornerY = corners[Consts.BOTTOM_LEFT].GetY();

            for (int i = 0; i < 8; i++)
            {
                blockWidth = random.Next(3, (width / 3) * 2);
                blockHeight = random.Next(3, (height / 3) * 2);
                
                blockCornerX = random.Next(roomCornerX, corners[Consts.TOP_RIGHT].GetX());
                blockCornerY = random.Next(roomCornerY, corners[Consts.TOP_RIGHT].GetY());
                
                Block toAdd = new Block(blockWidth, blockHeight, new Corner(blockCornerX, blockCornerY));
                toAdd.AddToMatrix(level, -1, noiseGenerator);
            }
            
            /*
            int minBlockX = corners[Consts.TOP_LEFT].GetX();
            int maxBlockX = corners[Consts.TOP_RIGHT].GetX();
            int minBlockY = corners[Consts.BOTTOM_RIGHT].GetY();
            int maxBlockY = corners[Consts.TOP_RIGHT].GetY();
            int nBlocks = Utility.GetBlocksByArea(width * height);

            if (corridorAdjaciencies[Consts.WEST])
            {
                minBlockX += Consts.BLOCK_TO_SUB;
            }

            if (corridorAdjaciencies[Consts.EAST])
            {
                maxBlockX -= Consts.BLOCK_TO_SUB;
            }

            if (corridorAdjaciencies[Consts.NORTH])
            {
                maxBlockY -= Consts.BLOCK_TO_SUB; 
            }

            if (corridorAdjaciencies[Consts.SOUTH])
            {
                minBlockY += Consts.BLOCK_TO_SUB;
            }

            int minBlockWidth = 0;
            int minBlockHeight = 0;
            int maxBlockWidth = maxBlockX - minBlockX;
            int maxBlockHeight = maxBlockY - minBlockY;


            for (int i=0; i<5; i++)
            {
                int cornerX = random.Next(minBlockX, maxBlockX);
                int cornerY = random.Next(minBlockY, maxBlockY);

                int blockWidth = random.Next(minBlockWidth, maxBlockWidth);
                int blockHeight = random.Next(minBlockHeight, maxBlockHeight);

                Block block = new Block(blockWidth, blockHeight, new Corner(cornerX, cornerY));

                block.AddToMatrix(level, noiseGenerator);
            }
            */
        }
    }
}
