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
            string[] words = {"TIGER", "CRANES", "GLUE" };
            Random random = new Random();
            int randomNumber = random.Next(0, 3);
            string word = words[randomNumber];
            int hiddenWordLength = word.Length;

            Console.WriteLine(hiddenWordLength + word);

            //Console.WriteLine("Guess a letter");
            //char guess = Convert.ToChar(Console.Read());



            while (true)
            {
                Console.WriteLine("Guess a letter");
                char guess = Convert.ToChar(Console.Read());

                PrintWord();

                Console.Write(guess);
                
                void PrintWord()
                {
                    for (int i = 0; i < hiddenWordLength; i++)
                    {
                        if (word[i] == guess)
                        {
                            Console.Write(guess + " ");
                        }
                        else
                        {
                            Console.Write("_ ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(hiddenWordLength + word);
            }
        }
    }
}
