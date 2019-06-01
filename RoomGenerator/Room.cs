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

        public void AddToMatrix(int[][] level)
        {
            AddToMatrix(level, id);
        }
    }
}
