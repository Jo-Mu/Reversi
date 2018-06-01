﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reversi.SpaceState;

namespace Reversi
{
    class Board
    {
        public State[,] _board;
        private readonly int _sideDimensions;

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

        public void PlacePieceAt(Position pos, State playerState)
        {
            if (IsEmptyAt(pos))
            {
                _board[pos.y, pos.x] = playerState;
            }
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
