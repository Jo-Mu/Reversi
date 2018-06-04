using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reversi.SpaceState;

namespace Reversi
{
    class GameMatch
    {
        private const State _player1 = State.Cross;
        private const State _player2 = State.Circle;
        private Board _board;

        public GameMatch(Board board)
        {
            _board = board;
        }
    }
}
