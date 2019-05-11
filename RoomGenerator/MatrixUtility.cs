using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class MatrixUtility
    {
        public static void AddElementFromCenter(int i, int j, int toAdd, int[][] matrix)
        {
            int maxWidth = Consts.MAX_LEVEL_WIDTH;
            int maxHeight = Consts.MAX_LEVEL_HEIGHT;
            int newJ;
            int newI;

            newI = i + maxWidth / 2;
            newJ = j + maxHeight / 2;

            if (newI < Consts.MAX_LEVEL_WIDTH && newJ < Consts.MAX_LEVEL_HEIGHT)
            {
                matrix[i + maxWidth / 2][j + maxHeight / 2] = toAdd;
            }
            else
            {
                Console.WriteLine("Impossibile inserire alla posizione " + i + ", " + j);
            }
        }
    }
}
