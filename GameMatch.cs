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

        //Handles the turn of a given player use arrow keys to move cursor and select a position on the board and then 'Enter' or 'Space' to select it.
        //If the move is possible then the given player's piece will be placed on the board and the board will update.
        //'S' can be pressed to skip the player's turn if he/she so chooses.
        private void PlayerTurn(State playerState)
        {
            bool turnIsOver = false;
            string initialTurnStatus;
            string turnStatus;
            string playerSymbol;
            string enemySymbol;

            //Sets up initial variables based on the given State of the player.
            if (playerState == State.Cross)
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

            //Loops until a either a valid move is made or if the player skips his/her turn.
            while (!turnIsOver)
            {
                Console.Clear();
                _board.DrawBoard();
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

                        if (_board.GetStateAt(selectedPos) == State.Circle)
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

                                if (cTop > cursorOffsetTop * _board.SideDimensions)
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

                                if (cLeft > cursorOffsetLeft * _board.SideDimensions)
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
                    _board.DrawBoard();
                    Console.WriteLine(turnStatus);
                }
                while (!piecePlaced && !skipPressed);

                if (skipPressed)
                {
                    break;
                }
                //Move is valid if it's on an empty space, adjacent to an enemy piece, and if it surrounds(captures) an enemy piece.
                else if (!_board.IsEmptyAt(selectedPos))
                {
                    turnStatus = "\nSpace is already occupied!\n" + initialTurnStatus;
                    continue;
                }
                else if (!_board.IsAdjacentToEnemyPiece(selectedPos, playerState))
                {
                    turnStatus = "\nPiece is not adjacent to any opponent pieces!\n" + initialTurnStatus;
                    continue;
                }
                else if (_board.AttemptPlacePieceAt(selectedPos, playerState))
                {
                    turnIsOver = true;
                    Console.Clear();
                    _board.DrawBoard();
                }
                else
                {
                    turnStatus = "\nCan not capture any opponent pieces from this position!\n" + initialTurnStatus;
                    continue;
                }
            }
        }

        public State Play()
        {
            bool boardFullGameOver = false;

            //Game will continue until either the board is full, no more valid moves can be made, or if one player runs out of pieces.
            //Winner is then decided.
            while (!boardFullGameOver)
            {
                PlayerTurn(_player1);

                if (_board.IsGameOver())
                {
                    boardFullGameOver = true;
                    break;
                }
                else if (!_board.AnyPlayerPiecesRemaining(_player2))
                {
                    Console.WriteLine("\nPlayer 2 is out of pieces. Game Over!");
                    break;
                }

                PlayerTurn(_player2);

                if (_board.IsGameOver())
                {
                    boardFullGameOver = true;
                }
                else if (!_board.AnyPlayerPiecesRemaining(_player1))
                {
                    Console.WriteLine("\nPlayer 1 is out of pieces. Game Over!");
                    break;
                }
                else if(!(_board.IsAnyMovePossible(_player1) || _board.IsAnyMovePossible(_player2)))
                {
                    Console.WriteLine("\nNo more valid moves possible. Game Over!");
                    break;
                }
            }

            if (boardFullGameOver)
            {
                Console.WriteLine("\nGame Over!");
            }

            return _board.TallyWinner();
        }

        public void Reset()
        {
            _board = new Board(_board.SideDimensions);
        }
    }
}
