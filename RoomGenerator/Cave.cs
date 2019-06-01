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
        public List<Types.CornerListGetterForCorridor> GetCornersForCorridor;
        private int height;
        protected List<Corner> corners;

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

            GetCornersForCorridor = new List<Types.CornerListGetterForCorridor>();

            GetCornersForCorridor.Add(GetNorthSide);
            GetCornersForCorridor.Add(GetEastSide);
            GetCornersForCorridor.Add(GetSouthSide);
            GetCornersForCorridor.Add(GetWestSide);
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public int GetID()
        {
            return id;
        }

        public List<Corner> GetCorners()
        {
            return corners;
        }

        public List<Corner> GetNorthSide(int corridorWidth, int corridorHeight)
        {
            List<Corner> ret = new List<Corner>();

            ret.Add(new Corner(corners[Consts.TOP_LEFT].GetX(), corners[Consts.TOP_LEFT].GetY() + corridorWidth));
            ret.Add(new Corner(corners[Consts.TOP_RIGHT].GetX() - corridorHeight, corners[Consts.TOP_RIGHT].GetY() + corridorWidth));

            return ret;
        }

        public List<Corner> GetEastSide(int corridorWidth, int corridorHeight)
        {
            List<Corner> ret = new List<Corner>();

            ret.Add(new Corner(corners[Consts.BOTTOM_RIGHT].GetX(), corners[Consts.BOTTOM_RIGHT].GetY() + corridorHeight));
            ret.Add(new Corner(corners[Consts.TOP_RIGHT].GetX(), corners[Consts.TOP_RIGHT].GetY()));

            return ret;
        }

        public List<Corner> GetSouthSide(int corridorWidth, int corridorHeight)
        {
            List<Corner> ret = new List<Corner>();

            ret.Add(new Corner(corners[Consts.BOTTOM_LEFT].GetX(), corners[Consts.BOTTOM_LEFT].GetY()));
            ret.Add(new Corner(corners[Consts.BOTTOM_RIGHT].GetX() - corridorHeight, corners[Consts.BOTTOM_RIGHT].GetY()));

            return ret;
        }

        public List<Corner> GetWestSide(int corridorWidth, int corridorHeight)
        {
            List<Corner> ret = new List<Corner>();

            ret.Add(new Corner(corners[Consts.BOTTOM_LEFT].GetX() - corridorWidth, corners[Consts.BOTTOM_LEFT].GetY() + corridorHeight));
            ret.Add(new Corner(corners[Consts.TOP_LEFT].GetX() - corridorWidth, corners[Consts.TOP_LEFT].GetY()));

            return ret;
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

        /** Checks if the current cave collides with another one
         * 
         * @param other: The other cave
         * 
         * @return: True if the cave collides, otherwise it returns false
         * 
         */ 
        public bool CollidesWith(Cave other)
        {
            if (!(other is Corridor))
            {
                List<Corner> otherCorners = other.GetCorners();

                if (corners[Consts.TOP_LEFT].GetX() >= otherCorners[Consts.TOP_RIGHT].GetX() ||
                    otherCorners[Consts.TOP_LEFT].GetX() >= corners[Consts.TOP_RIGHT].GetX() ||
                    corners[Consts.BOTTOM_RIGHT].GetY() >= otherCorners[Consts.TOP_RIGHT].GetY() ||
                    corners[Consts.TOP_RIGHT].GetY() <= otherCorners[Consts.BOTTOM_RIGHT].GetY())
                {
                    return false;
                }
                

                return true;
            }
            
            
            return false;
        }
    }
}
