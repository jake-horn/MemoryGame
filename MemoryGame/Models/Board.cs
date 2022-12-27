using MemoryGame.API;

namespace MemoryGame.Models
{
    public class Board
    {
        private WordCard[] _wordCardArray;
        private readonly int _boardColumnSize;
        private readonly int _totalSquares;
        private Dictionary<int, string> _wordDictionary = new();
        private WordApiCaller? _wordApiCaller;

        public Board(int boardSize)
        {
            _boardColumnSize = boardSize;
            _wordCardArray = new WordCard[boardSize * boardSize];
            _totalSquares = boardSize * boardSize;
        }

        /// <summary>
        /// Sets up the board for use with the game, primary method that pulls everything together. 
        /// </summary>
        /// <returns></returns>
        public async Task SetUpBoard()
        {
            _wordApiCaller = new WordApiCaller(_totalSquares);
            _wordDictionary = await _wordApiCaller.GetWordDictionary();
            PopulateArray();
            AssignRandomCardNumberToWordCards();
        }

        /// <summary>
        /// Prints the board to the console. 
        /// </summary>
        public void PrintBoard()
        {
            int counter = 0; 

            for (int i = 0; i < _wordCardArray.Length; i++)
            {
                if (counter == _boardColumnSize)
                {
                    Console.WriteLine();
                    counter = 0; 
                }

                Console.Write(_wordCardArray[i].ToString() + "  ");

                counter++;
            }
        }

        /// <summary>
        /// Provides a getter for the index of the current word card array. 
        /// </summary>
        /// <param name="index">The index required from the array. </param>
        /// <returns></returns>
        public WordCard GetIndex(int index)
        {
            return _wordCardArray[index];
        }

        /// <summary>
        /// Confirms whether two words are equal in the word card array. 
        /// </summary>
        /// <param name="word1">Index position 1 for comparison. </param>
        /// <param name="word2">Index position 2 for comparison. </param>
        public bool AreWordsEqual(int word1, int word2)
        {
            return _wordCardArray[word1 -1].Word.Equals(_wordCardArray[word2 -1].Word); 
        }


        /// <summary>
        /// Populates the array with WordCards and words
        /// </summary>
        private void PopulateArray()
        {
            int dictPointer = 0;
            int counter = -1;
            Random random = new();

            for (int i = 0; i < _wordCardArray.Length; i++)
            {
                _wordCardArray[i] = new WordCard(_wordDictionary.ElementAt(dictPointer).Value, random.Next(1,1000).ToString());

                counter++;

                if (counter == 1)
                {
                    counter = -1;
                    dictPointer++;
                }
            }
        }

        /// <summary>
        /// Assigns a random number to all the WordCards in the array, this randomises the order for use in the game
        /// </summary>
        /// <param name="wordCardArray">An array of WordCards</param>
        /// <returns>A WordCard array</returns>
        private WordCard[] AssignRandomCardNumberToWordCards()
        {
            // Order the array by converting to a list, and then convert back to an array
            // This can be changed in the future to manually change the array ordering instead
            IEnumerable<WordCard> orderedList = _wordCardArray.ToList().OrderBy(x => x.CardNumber);
            _wordCardArray = orderedList.ToArray();

            // Set the WordCard CardNumber from 1 to n for use in the game
            for (int i = 1; i <= _wordCardArray.Length; i++)
            {
                _wordCardArray[i - 1].CardNumber = i.ToString();
            }

            return _wordCardArray;
        }
    }
}
