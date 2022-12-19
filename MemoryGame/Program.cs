using MemoryGame.API;
using MemoryGame.Game;
using MemoryGame.Models;

namespace MemoryGame
{
    public class MemoryGame
    {
        public static async Task Main(string[] args)
        {
            int boardSize = 1;
            int numberOfSquares; 
            int difficultyLevel;
            int guess1;
            int guess2;
            int attempts = 0; 
            int remainingPairs; 
            string[] guessCollection;

            VisualHelpers.DisplayTitle(); 

            // Catching any issues with the board size being an odd number or a string
            while (boardSize % 2 != 0 || boardSize == 0)
            {
                Console.WriteLine("Please select your board size (an even number): ");

                if (!int.TryParse(Console.ReadLine(), out boardSize))
                {
                    Console.WriteLine("You have not provided a valid number, please try again.");
                }

                //boardSize = int.TryParse(Console.ReadLine(), out isParsed);

                if (boardSize % 2 != 0)
                {
                    Console.WriteLine("Must be an even number, please try again. \n");
                }
            }

            // Set difficulty level and catch an issues with converting to integer
            Console.WriteLine("Please select your difficulty level (1: 6 seconds, 2: 4 seconds, 3: 2 seconds, any other number: 1 second):");

            while (!int.TryParse(Console.ReadLine(), out difficultyLevel))
            {
                Console.WriteLine("You have not entered a number, try again.\n");
            }

            // Initialise the new board and complete the set up
            Board board = new Board(boardSize);
            await board.SetUpBoard();

            // Sets remaining pairs prior to game starting
            remainingPairs = (boardSize * boardSize) / 2;

            // Set number of squares for the game 
            numberOfSquares = boardSize * boardSize;

            while (true)
            {
                guess1 = 0;  
                guess2 = 0;

                board.PrintBoard();

                Console.WriteLine($"\n\nAttempts: {attempts}");
                Console.WriteLine($"Remaining pairs: {remainingPairs}");

                while (!GameplayHelpers.GuessValidation(guess1, guess2, numberOfSquares))
                {
                    Console.WriteLine("\nWhich cards to reveal?");
                    guessCollection = Console.ReadLine().Split(',');
                    guess1 = int.Parse(guessCollection[0]);
                    guess2 = int.Parse(guessCollection[1]);
                    
                    if (!GameplayHelpers.GuessValidation(guess1, guess2, numberOfSquares))
                    {
                        Console.WriteLine("Invalid input, please check and try again. \n");
                    }
                }

                // Getting WordCard for each guess
                WordCard boardIndex1 = board.GetIndex(guess1-1);
                WordCard boardIndex2 = board.GetIndex(guess2-1);

                // Writing each card number and word guessed
                Console.WriteLine($"\n{boardIndex1.CardNumber}: {boardIndex1.Word}\n{boardIndex2.CardNumber}: {boardIndex2.Word}\n");

                attempts++;

                if (board.AreWordsEqual(guess1, guess2))
                {
                    remainingPairs--; 

                    boardIndex1.WordFound();
                    boardIndex2.WordFound();

                    Console.WriteLine("Correct!\n");
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }

                if (remainingPairs == 0)
                {
                    Console.WriteLine($"Game completed in {attempts} attempts! Well done!");
                    break; 
                }

                GameplayHelpers.DifficultyWaitTime(difficultyLevel);
                Console.Clear();

                VisualHelpers.DisplayTitle();
            }
            
        }
    }
}