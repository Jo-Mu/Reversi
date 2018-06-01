using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    struct Position
    {
        public int x;
        public int y;

        public Position(int in_x, int in_y)
        {
            x = in_x;
            y = in_y;
        }

        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1.x + pos2.x, pos1.y + pos2.y);
        }

        public static Position operator -(Position pos1, Position pos2)
        {
            return new Position(pos1.x - pos2.x, pos1.y - pos2.y);
        }
    }
}
