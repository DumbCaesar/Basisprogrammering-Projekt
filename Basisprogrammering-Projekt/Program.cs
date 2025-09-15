using System;

namespace Basisprogrammering_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hangman();
        }

        static void Hangman()
        {
            string[] wordList = {"TIGER", "CRANES", "GLUE" };
            Random random = new Random();
            int randomNumber = random.Next(wordList.Length);
            string randomWord = wordList[randomNumber];
            int randomWordLength = randomWord.Length;
            int lives = 5;

            string word = "";
            for (int i = 0; i < randomWordLength; i++)
            {
                word += "-";
            }

            while (lives > 0 && word.Contains("-"))
            {
                Console.Clear();
                Console.WriteLine(word);
                Console.WriteLine("Lives left: " + lives);
                Console.WriteLine("Guess a letter");
                string input = Console.ReadLine().ToUpper();

                char guess = Convert.ToChar(input);
                bool correct = false;

                for (int i = 0; i < randomWordLength; i++)
                {
                    if (randomWord[i] == guess)
                    {
                        word = word.Remove(i, 1).Insert(i, guess.ToString());
                        correct = true;
                    }
                }
                if (!correct)
                {
                    lives--;
                    Console.WriteLine("Incorrect");
                    Console.ReadKey();
                }
            }
            if (word.Contains("-"))
            {
                Console.Clear();
                Console.WriteLine("You Lose! The word was: " + randomWord);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You Win! The word was: " + randomWord);
            }
        }
    }
}
