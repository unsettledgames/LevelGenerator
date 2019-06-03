using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RoomGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Color background = Color.FromArgb(255, 0, 0, 0);
            Color foreground = Color.FromArgb(255, 255, 255, 255);

            Level level = new RoomGenerator.Level(7, 20, 7, 20, 15, 40, 3, 5, 1, 15, 40, background, foreground, 50);
            level.GenerateMap();
            string c = Console.ReadLine();

            while (c.Equals(""))
            {
                level.GenerateMap();

                c = Console.ReadLine();
            }
        }
    }
}
