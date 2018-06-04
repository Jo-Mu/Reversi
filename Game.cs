﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reversi.SpaceState;

namespace Reversi
{
    class Game
    {

        static void Main(string[] args)
        {
            int boardSideDimensions = 8;
            Board board = new Board(boardSideDimensions);

            board.DrawBoard();
            GameMatch match = new GameMatch(board);
            match.Play();
        }
    }
}
