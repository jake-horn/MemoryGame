# MemoryGame
This is just a small project I found on a C# discord to practice coding. It is a game of "Memory Snap", where you memorise the words under the cards and match them to their partner cards. 

The game asks the user for the number of columns (board size) and the difficulty level, which is the time for how long the cards are shown. After this the user is presented with cards and they can select two of the numbers, after which the words are displayed for the difficult time set before being reset. If the user finds the correct cards, they are taken off the board. The game ends when all cards are found. 

The words are taken from a free word api (https://random-word-api.herokuapp.com/) by calling the api through the HttpClient class. 

The aims for this were: 
- To practice object oriented programming. 
- To reinforce knowledge on HttpClient/API calling. 
- To aim to use arrays for the display and storing of data. 

# To Run
Download or clone the repo, navigate the command prompt to the main MemoryGame file then type "dotnet run". You'll need to have .net 6 installed to run the program. 