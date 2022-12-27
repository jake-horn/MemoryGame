namespace MemoryGame.API
{
    public class WordApiCaller
    {
        private readonly Dictionary<int, string> _wordDictionary;
        private readonly HttpClient _httpClient;
        private IList<string> _wordList; 
        private readonly int _totalBoardSize; 

        public WordApiCaller(int totalBoardSize)
        {
            _wordDictionary = new Dictionary<int,string>();
            _totalBoardSize = totalBoardSize;
            _wordList = new List<string>();
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Returns the word dictionary. 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<int, string>> GetWordDictionary()
        {
            await PopulateWordDictionary();
            return _wordDictionary; 
        }

        /// <summary>
        /// Populates the word dictionary. 
        /// </summary>
        /// <returns></returns>
        private async Task PopulateWordDictionary()
        {
            await GetWords(_totalBoardSize);

            for (int i = 0; i < _wordList.Count; i++)
            {
                _wordDictionary.Add(i + 1, _wordList[i]);
            } 
        }

        /// <summary>
        /// Used to call the api and get the words for use in the game. 
        /// </summary>
        /// <param name="numberOfWords">Number of words required for the game</param>
        /// <returns></returns>
        private async Task GetWords(int numberOfWords)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"https://random-word-api.herokuapp.com/word?number={numberOfWords}");
                string responseBody = await response.Content.ReadAsStringAsync();

                _wordList = responseBody.Split(',').ToList();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
