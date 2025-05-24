using System;
using System.IO;
using System.Collections.Generic;

namespace SpellCheckerTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load dictionary words into an array
            string[] dictionary = File.ReadAllLines("WordsFile.txt");

            // 1. Check one word
            Console.Write("Enter a word: ");
            string word = Console.ReadLine();
            if (IsWordCorrect(word, dictionary))
            {
                Console.WriteLine("The word is spelled correctly.");
            }
            else
            {
                Console.WriteLine("The word is spelled incorrectly.");
            }

            // 2. Check multiple words
            Console.WriteLine("\nEnter a sentence:");
            string sentence = Console.ReadLine();
            string[] words = sentence.Split(' ');
            List<string> wrongWords = new List<string>();
            int correctCount = 0;

            foreach (string w in words)
            {
                string cleanWord = w.Trim('.', ',', ';', ':', '?', '!');
                if (IsWordCorrect(cleanWord, dictionary))
                {
                    correctCount++;
                }
                else
                {
                    wrongWords.Add(cleanWord);
                }
            }

            // 3. Calculate spelling score
            double score = (double)correctCount / words.Length * 100;
            Console.WriteLine($"Spelling score: {score:F2}%");

            // 4. Save incorrect words
            if (wrongWords.Count > 0)
            {
                File.WriteAllLines("MisspelledWords.txt", wrongWords);
                Console.WriteLine("Incorrect words saved to MisspelledWords.txt");
            }
            else
            {
                Console.WriteLine("No misspelled words found!");
            }
        }

        static bool IsWordCorrect(string word, string[] dictionary)
        {
            foreach (string dictWord in dictionary)
            {
                if (string.Equals(word, dictWord, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

