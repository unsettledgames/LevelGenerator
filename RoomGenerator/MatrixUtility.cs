using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class MatrixUtility
    {
        /** Adds an element to a matrix as if the indexes started from the center of the matrix
         * 
         * @param i:        normal row index
         * @param j:        normal column index
         * @param toAdd:    number to put in the matrix
         * @param matrix:   the matrix to which the number should be added 
         * 
         */ 
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
                Console.WriteLine("Couldn't insert at " + i + ", " + j);
            }
        }
    }
}
