using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class WordCard
    {
        private int Id { get; set; }
        public string Word { get; set; }
        public string CardNumber { get; set; }
        public bool IsFound { get; set; } = false;

        public WordCard(int id, string word, string cardNumber)
        {
            Id = id;
            Word = word;
            CardNumber = cardNumber;
        }

        public void WordFound()
        {
            IsFound = true;
            CardNumber = "[ FOUND ]";
        }

        public override string ToString()
        {
            if (CardNumber.Equals("[ FOUND ]"))
            {
                return CardNumber;
            }
            else if (int.Parse(CardNumber) < 10)
            {
                return $"[   {CardNumber}   ]";
            }
            else
            {
                return $"[  {CardNumber}   ]";
            }
        }
    }
}
