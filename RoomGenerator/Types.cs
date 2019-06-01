using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGenerator
{
    class Types
    {
        public delegate List<Corner> CornerListGetter(int corridorWidth, int corridorHeight);
    }
}
