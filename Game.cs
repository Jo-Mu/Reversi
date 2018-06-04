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

            Console.WriteLine("Welcome to Othello (Reversi)!\n");

            board.DrawBoard();

            Console.WriteLine("\n--Press any key to start--");
            Console.ReadKey();

            GameMatch match = new GameMatch(board);
            bool playingGame = true;
            int player1Wins = 0;
            int player2Wins = 0;

            while (playingGame)
            {
                State gameResult = match.Play();

                switch (gameResult)
                {
                    case State.Cross:
                        {
                            Console.WriteLine("\nPlayer 1 Wins!");
                            player1Wins++;
                            break;
                        }
                    case State.Circle:
                        {
                            Console.WriteLine("\nPlayer 2 Wins!");
                            player2Wins++;
                            break;
                        }
                    case State.Empty:
                        {
                            Console.WriteLine("\nIt's a tie!");
                            player1Wins++;
                            player2Wins++;
                            break;
                        }
                }

                Console.WriteLine("\nScore Totals");
                Console.WriteLine("============================");
                Console.WriteLine("Player 1: " + player1Wins);
                Console.WriteLine("Player 2: " + player2Wins);
                Console.Write("\nPress 'Enter' to play another game or any other key to quit the game");
                
                if(Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    match.Reset();
                }
                else
                {
                    playingGame = false;
                    Console.CursorLeft = 0;
                    ClearCurrentConsoleLine();

                    if (player1Wins > player2Wins)
                    {
                        Console.WriteLine("And the grand winner is: PLAYER 1!");
                    }
                    else if (player2Wins > player1Wins)
                    {
                        Console.WriteLine("And the grand winner is: PLAYER 2!");
                    }
                    else
                    {
                        Console.WriteLine("It's an overall TIE!");
                    }
                }
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
