using System;
using System.ComponentModel;
using System.Linq;

namespace TicTacToe
{
    class Program {
        private static Boolean?[,] _gameGrid;
        private static bool _player;
        private static bool _playing;

        /// <summary>
        /// Initialize three variables and start the main game loop.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            _player = true;
            _playing = false;
            _gameGrid = new Boolean?[3, 3] {{null, null, null}, {null, null, null}, {null, null, null}};
            GameLoop();
        }

        /// <summary>
        /// Main loop for starting a new game of Tic Tac Toe.
        /// Ask if a new game has to be started, check input for the answer.
        /// </summary>
        private static void GameLoop() {
            bool loop = true;
            do {
                Console.WriteLine("Do you want to play a game of TicTacToe? [y/n]");
                String response = Console.ReadLine();

                if (response == "y") {
                    ClearGrid();
                    PlayGame();
                }
                else if (response == "n") {
                    loop = false;
                }
                else {
                    Console.WriteLine("Please enter 'y' or 'n'.");
                }
            } while (loop);
        }

        /// <summary>
        /// Loop for playing a single game of Tic Tac Toe.
        /// Ask each player for the position of their next move.
        /// </summary>
        private static void PlayGame() {
            _playing = true;
            do {
                PrintGrid();
                Console.WriteLine(_player ? "Turn of player 'o': " : "Turn of player 'x': ");
                NewMove();
                if (CheckForWinner() || CheckforDraw())
                    _playing = !_playing;
                else _player = !_player;

            } while (_playing);

            PrintGrid();

            if(CheckforDraw())
                Console.WriteLine("The game has ended in a draw!");
            else Console.WriteLine(_player ? "Player 'o' has won! " : "Player 'x' has won!");
            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }

        /// <summary>
        /// Process a single move of a player.
        /// Ask for the position and check if this position is valid (empty and within range).
        /// </summary>
        private static void NewMove() {
            bool moveCompleted = false;
            do {
                Console.WriteLine("What column do you want to mark? [0,1,2]");
                String col = Console.ReadLine();
                Console.WriteLine("What row do you want to mark? [0,1,2]");
                String row = Console.ReadLine();

                if (Int32.TryParse(col, out int c) && Int32.TryParse(row, out int r)) {
                    if (_gameGrid[c, r] == null) {
                        _gameGrid[c, r] = _player;
                        moveCompleted = true;
                    }
                    else {
                        Console.WriteLine("Please choose an empty grid cell.");
                    }
                }
                else {
                    Console.WriteLine("Please enter values between 0 and 2.");
                }
            } while (!moveCompleted);
        }

        /// <summary>
        /// Print the current grid to the console.
        /// </summary>
        static void PrintGrid() {
            Console.WriteLine("");
            for (int i = 0; i < _gameGrid.GetLength(0); i++) {
                for (int j = 0; j < _gameGrid.GetLength(1); j++) {
                    if (_gameGrid[j, i] == true)
                        Console.Write("o    ");
                    else if (_gameGrid[j, i] == false)
                        Console.Write("x    ");
                    else
                        Console.Write("-    ");
                }
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// Clear the grid for when a new game has to be started.
        /// </summary>
        static void ClearGrid() {
            for (int i = 0; i < _gameGrid.GetLength(0); i++) {
                for (int j = 0; j < _gameGrid.GetLength(1); j++) {
                    _gameGrid[j, i] = null;
                }
            }
        }

        /// <summary>
        /// Check if the player that has the turn is the winner.
        /// All possible victory conditions are hardcoded, as there are only 8 of these.
        /// </summary>
        /// <returns>TRUE if current player has won. FALSE otherwise</returns>
        private static Boolean CheckForWinner() {
            Boolean?[] winner = new Boolean?[] {_player, _player, _player};
            if (new Boolean?[] {_gameGrid[0, 0], _gameGrid[1, 0], _gameGrid[2, 0]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[0, 1], _gameGrid[1, 1], _gameGrid[2, 1]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[0, 2], _gameGrid[1, 2], _gameGrid[2, 2]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[0, 0], _gameGrid[0, 1], _gameGrid[0, 2]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[1, 0], _gameGrid[1, 1], _gameGrid[1, 2]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[2, 0], _gameGrid[2, 1], _gameGrid[2, 2]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[0, 0], _gameGrid[1, 1], _gameGrid[2, 2]}.SequenceEqual(winner))
                return true;
            else if (new Boolean?[] {_gameGrid[0, 2], _gameGrid[1, 1], _gameGrid[2, 0]}.SequenceEqual(winner))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if the grid is full. As this check occurrs after the check for a winnner, a full grid means a draw.
        /// </summary>
        /// <returns>TRUE if the game is a draw. FALSE otherwise.</returns>
        private static Boolean CheckforDraw() {
            for (int i = 0; i < _gameGrid.GetLength(0); i++) {
                for (int j = 0; j < _gameGrid.GetLength(1); j++) {
                    if (_gameGrid[j, i] != null)
                        return false;
                }
            }
            return true;
        }
    }
}
