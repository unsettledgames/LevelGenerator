using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Utility
    {
        public delegate List<Corner> CornerListGetterForCorridor(int corridorWidth, int corridorHeight);
        public static Dictionary<int, int> blocksPerArea;

        public static int GetBlocksByArea(int area)
        {
            foreach (int i in blocksPerArea.Keys)
            {
                Console.WriteLine("" + i + ", " + blocksPerArea[i]);

                if (i >= area)
                {
                    return blocksPerArea[i];
                }
            }

            return 0;
        }
    }
}
