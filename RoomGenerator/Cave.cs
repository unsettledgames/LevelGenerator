using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Cave
    {
        private static int idCount = 0;
        protected int id;
        private int width;
        private int height;
        private List<Corner> corners;

        public Cave(int width, int height, Corner topLeft)
        {
            Corner topRight;
            Corner bottomRight;
            Corner bottomLeft;

            this.width = width;
            this.height = height;

            topRight = new Corner(topLeft.GetX() + width, topLeft.GetY());
            bottomRight = new Corner(topLeft.GetX() + width, topLeft.GetY() - height);
            bottomLeft = new Corner(topLeft.GetX(), topLeft.GetY() - height);
            corners = new List<Corner>();

            corners.Add(topLeft);
            corners.Add(topRight);
            corners.Add(bottomRight);
            corners.Add(bottomLeft);

            // Assigns progressively higher id
            id = idCount;
            idCount++;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public List<Corner> GetCorners()
        {
            return corners;
        }

        /** Adds the entire cave to the matrix
         * 
         * @param matrix: the destination matrix
         * @param code:   the code of the cave
         */ 
        public void AddToMatrix(int[][] matrix, int code)
        {
            for (int i = corners[Consts.TOP_LEFT].GetX(); i < corners[Consts.TOP_RIGHT].GetX(); i++)
            {
                for (int j=corners[Consts.BOTTOM_LEFT].GetY(); j<corners[Consts.TOP_LEFT].GetY(); j++)
                {
                    MatrixUtility.AddElementFromCenter(i, j, code, matrix);
                }
            }
        }
    }
}
