using MemoryGame.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class Board
    {
        private WordCard[] _wordCardArray;
        private int _boardColumnSize { get; }
        private int _totalSquares { get; }
        private Dictionary<int, string> _wordDictionary = new Dictionary<int, string>();
        private WordApiCaller? _wordApiCaller;

        public Board(int boardSize)
        {
            _boardColumnSize = boardSize;
            _wordCardArray = new WordCard[boardSize * boardSize];
            _totalSquares = boardSize * boardSize;
        }

        public async Task SetUpBoard()
        {
            _wordApiCaller = new WordApiCaller(_totalSquares);
            _wordDictionary = await _wordApiCaller.GetWordDictionary();
            PopulateArray();
            AssignRandomCardNumberToWordCards();
        }

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

        public WordCard[] GetWordCardArray()
        {
            return _wordCardArray;
        }

        public WordCard GetIndex(int index)
        {
            return _wordCardArray[index];
        }

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
            Random random = new Random();

            for (int i = 0; i < _wordCardArray.Length; i++)
            {
                _wordCardArray[i] = new WordCard(i, _wordDictionary.ElementAt(dictPointer).Value, random.Next(1,1000).ToString());

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
