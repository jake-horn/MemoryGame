using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Game
{
    public static class GameplayHelpers
    {
        public static void DifficultyWaitTime(int difficultyLevel)
        {
            switch (difficultyLevel)
            {
                case 1:
                    Thread.Sleep(6000);
                    break;
                case 2:
                    Thread.Sleep(4000);
                    break; 
                case 3:
                    Thread.Sleep(2000);
                    break; 
                default:
                    Thread.Sleep(1000);
                    break; 
            }
        }

        public static bool GuessValidation(int guess1, int guess2, int maximumNumber)
        {
            // If guesses are equal numbers
            if (guess1 == guess2)
            {
                return false; 
            }

            // If gueses are lower than 1
            if (guess1 < 1 || guess2 < 1)
            {
                return false; 
            }

            // If guesses are higher than the maximum number of the board
            if (guess1 > maximumNumber || guess2 > maximumNumber)
            {
                return false; 
            }


            return true; 
        }
    }
}
