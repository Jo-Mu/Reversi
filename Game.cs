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
            int boardSideDimensions = 8;
            Board board = new Board(boardSideDimensions);

            board.DrawBoard();
            GameMatch match = new GameMatch(board);
            match.Play();
        }

        void PlayerTurn(ref Board board, State playerState)
        {
            bool turnIsOver = false;
            string initialTurnStatus;
            string turnStatus;
            string playerSymbol;
            string enemySymbol;

            if(playerState == State.Cross)
            {
                initialTurnStatus = "\nPlayer 1's turn:\nPress 'Enter' or 'Space' to place piece.\nIf unable to make a move press 'S' to skip turn.";
                turnStatus = initialTurnStatus;
                playerSymbol = "X";
                enemySymbol = "O";
            }
            else
            {
                initialTurnStatus = "\nPlayer 2's turn:\nPress 'Enter' or 'Space' to place piece.\nIf unable to make a move press 'S' to skip turn.";
                turnStatus = initialTurnStatus;
                playerSymbol = "O";
                enemySymbol = "X";
            }

            while (!turnIsOver)
            {
                Console.Clear();
                board.DrawBoard();
                Console.WriteLine(turnStatus);

                const int cursorOffsetLeft = 3;
                const int cursorOffsetTop = 1;
                int cLeft = cursorOffsetLeft;
                int cTop = cursorOffsetTop;
                bool piecePlaced = false;
                bool skipPressed = false;
                Position selectedPos = new Position((cLeft / cursorOffsetLeft) - 1, (cTop / cursorOffsetTop) - 1);

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.SetCursorPosition(cLeft, cTop);
                        Console.Write(playerSymbol);

                        if (board.GetStateAt(selectedPos) == State.Circle)
                        {
                            Console.SetCursorPosition(cLeft, cTop);
                            Console.Write(enemySymbol);
                        }
                    }

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                cTop -= cursorOffsetTop;

                                if (cTop <= 0)
                                {
                                    cTop += cursorOffsetTop;
                                }

                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                cTop += cursorOffsetTop;

                                if (cTop > cursorOffsetTop * board.SideDimensions)
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
                        case ConsoleKey.Spacebar:
                            {
                                piecePlaced = true;
                                break;
                            }
                        case ConsoleKey.S:
                            {
                                skipPressed = true;
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
                while (!piecePlaced && !skipPressed);

                if (skipPressed)
                {
                    break;
                }
                else if (!board.IsEmptyAt(selectedPos))
                {
                    turnStatus = "\nSpace is already occupied!\n" + initialTurnStatus;
                    continue;
                }
                else if(!board.IsAdjacentToEnemyPiece(selectedPos, playerState))
                {
                    turnStatus = "\nPiece is not adjacent to any opponent pieces!\n" + initialTurnStatus;
                    continue;
                }
                else if(board.AttemptPlacePieceAt(selectedPos, playerState))
                {
                    turnIsOver = true;
                    Console.Clear();
                    board.DrawBoard();
                }
                else
                {
                    turnStatus = "\nCan not capture any opponent pieces from this position!\n" + initialTurnStatus;
                    continue;
                }
            }
        }
    }
}
