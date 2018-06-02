using System;
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
            State player1 = State.Cross;
            State player2 = State.Circle;
            int boardSideDimensions = 8;
            Board board = new Board(boardSideDimensions);

            //testing piece placement and flipping
            board.PlacePieceAt(new Position(4, 5), player1);
            board.PlacePieceAt(new Position(5, 5), player2);
            board.PlacePieceAt(new Position(3, 5), player2);

            board.DrawBoard();
        }
    }
}
