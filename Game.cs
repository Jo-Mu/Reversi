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

            board.DrawBoard();

            //All below is testing piece placement and input validation
            const int cursorOffsetLeft = 3;
            const int cursorOffsetTop = 1;
            int cLeft = cursorOffsetLeft;
            int cTop = cursorOffsetTop;
            bool enterPressed = false;
            Position selectedPos = new Position((cLeft / cursorOffsetLeft) - 1, (cTop / cursorOffsetTop) - 1);
            string turnStatus = "Player 1's turn";
            Console.WriteLine(turnStatus);

            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(cLeft, cTop);
                    Console.Write("X");

                    if(board.GetStateAt(selectedPos) == State.Circle)
                    {
                        Console.SetCursorPosition(cLeft, cTop);
                        Console.Write("O");
                    }
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            cTop -= cursorOffsetTop;
                            
                            if(cTop <= 0)
                            {
                                cTop += cursorOffsetTop;
                            }

                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            cTop += cursorOffsetTop;

                            if(cTop > cursorOffsetTop * board.SideDimensions)
                            {
                                cTop -= cursorOffsetTop;
                            }

                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            cLeft -= cursorOffsetLeft;

                            if (cLeft <= 0)
                            {
                                cLeft += cursorOffsetLeft;
                            }

                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            cLeft += cursorOffsetLeft;

                            if (cLeft > cursorOffsetLeft * board.SideDimensions)
                            {
                                cLeft -= cursorOffsetLeft;
                            }

                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            enterPressed = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                selectedPos = new Position((cLeft / cursorOffsetLeft) - 1, (cTop / cursorOffsetTop) - 1);
                Console.SetCursorPosition(0, 0);
                board.DrawBoard();
                Console.WriteLine(turnStatus);
            }
            while (!enterPressed);

            if (board.AttemptPlacePieceAt(selectedPos, player1))
            {
                Console.Clear();
                board.DrawBoard();
            }
            else
            {
                Console.Clear();
                board.DrawBoard();
                Console.WriteLine("Can't make move!");
            }
        }
    }
}
