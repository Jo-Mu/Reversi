using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reversi.SpaceState;

namespace Reversi
{
    class Board
    {
        private State[,] _board;
        private readonly int _sideDimensions;
        private int _emptySpacesRemaining;
        public int SideDimensions { get { return _sideDimensions; } }

        public Board(int sideDimensions)
        {
            if(sideDimensions < 4)
            {
                throw new InvalidBoardSizeException("Dimensions are too small");
            }

            if(sideDimensions % 2 != 0)
            {
                throw new InvalidBoardSizeException("Dimensions must be even");
            }

            _board = new State[sideDimensions, sideDimensions];
            _sideDimensions = sideDimensions;
            _emptySpacesRemaining = (sideDimensions * sideDimensions) - 4;

            for(int x = (sideDimensions / 2) - 1; x <= sideDimensions / 2; x++)
            {
                for (int y = (sideDimensions / 2) - 1; y <= sideDimensions / 2; y++)
                {
                    if((x + y) % 2 == 0)
                    {
                        _board[y, x] = State.Circle;
                    }
                    else
                    {
                        _board[y, x] = State.Cross;
                    }
                }
            }
        }

        public State GetStateAt(Position pos)
        {
            return _board[pos.y, pos.x];
        }

        public bool IsEmptyAt(Position pos)
        {
            return _board[pos.y, pos.x] == State.Empty;
        }

        public bool IsOutOfBounds(Position pos)
        {
            return pos.x < 0 || pos.x >= _sideDimensions || pos.y < 0 || pos.y >= _sideDimensions;
        }

        public bool IsAdjacentToEnemyPiece(Position pos, State playerState)
        {
            State enemyState = (playerState == State.Cross) ? State.Circle : State.Cross;
            int xStart = (pos.x - 1).LimitToRange(0, _sideDimensions - 1);
            int xEnd = (pos.x + 1).LimitToRange(0, _sideDimensions - 1);
            int yStart = (pos.y - 1).LimitToRange(0, _sideDimensions - 1);
            int yEnd = (pos.y + 1).LimitToRange(0, _sideDimensions - 1);

            for(int x = xStart; x <= xEnd; x++)
            {
                for(int y = yStart; y <= yEnd; y++)
                {
                    if(!(x == pos.x && y == pos.y))
                    {
                        if(GetStateAt(new Position(x, y)) == enemyState)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool AttemptPlacePieceAt(Position pos, State playerState)
        {
            bool validMove = false;

            if (IsEmptyAt(pos))
            {
                State enemyState = (playerState == State.Cross) ? State.Circle : State.Cross;

                for (int x = -1; x <= 1; x++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        Position posChange = new Position(x, y);

                        if(!(x == 0 && y == 0) && !IsOutOfBounds(pos + posChange))
                        {
                            if(FlipSurroundedPieces(pos, posChange, playerState, true))
                            {
                                validMove = true;
                            }
                        }
                    }
                }

                if (validMove)
                {
                    _board[pos.y, pos.x] = playerState;
                    _emptySpacesRemaining--;
                }
            }

            return validMove;
        }

        private void Flip(Position pos)
        {
            if (GetStateAt(pos) == State.Cross)
            {
                _board[pos.y, pos.x] = State.Circle;
            }

            else if (GetStateAt(pos) == State.Circle)
            {
                _board[pos.y, pos.x] = State.Cross;
            }
        }

        private bool FlipSurroundedPieces(Position pos, Position posChange, State playerState, bool isInitialPos)
        {
            Position neighborPos = pos + posChange;

            if (IsOutOfBounds(neighborPos) || IsEmptyAt(neighborPos))
            {
                return false;
            }
            else if (GetStateAt(neighborPos) == playerState)
            {
                if (isInitialPos)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (FlipSurroundedPieces(neighborPos, posChange, playerState, false))
            {
                Flip(neighborPos);
                return true;
            }

            return false;
        }

        public bool IsGameOver()
        {
            return _emptySpacesRemaining == 0;
        }

        public State TallyWinner()
        {
            if (IsGameOver())
            {
                int crossScore = 0;
                int circleScore = 0;

                foreach(State state in _board)
                {
                    if(state == State.Cross)
                    {
                        crossScore++;
                    }
                    else if(state == State.Circle)
                    {
                        circleScore++;
                    }
                }

                if(crossScore > circleScore)
                {
                    return State.Cross;
                }
                else if(circleScore > crossScore)
                {
                    return State.Circle;
                }
                else
                {
                    return State.Empty;
                }
            }

            return State.Empty;
        }

        public void DrawBoard()
        {
            for (int y = 0; y <= _sideDimensions; y++)
            {
                for (int x = 0; x <= _sideDimensions; x++)
                {
                    if (y == 0)
                    {
                        if (x == 0)
                        {
                            Console.Write("   " + x);
                        }
                        else if (x < _sideDimensions)
                        {
                            Console.Write("  " + x);
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            Console.Write((y - 1) + " ");
                        }
                        else
                        {
                            switch (_board[y - 1, x - 1])
                            {
                                case State.Empty:
                                    {
                                        Console.Write("[ ]");
                                        break;
                                    }
                                case State.Cross:
                                    {
                                        Console.Write("[X]");
                                        break;
                                    }
                                case State.Circle:
                                    {
                                        Console.Write("[O]");
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                }

                Console.Write("\n");
            }
        }
    }
}
