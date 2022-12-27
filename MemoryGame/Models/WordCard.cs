namespace MemoryGame.Models
{
    public class WordCard
    {
        public string Word { get; set; }
        public string CardNumber { get; set; }
        public bool IsFound { get; set; } = false;

        public WordCard(string word, string cardNumber)
        {
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
