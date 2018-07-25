using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PegePlayer.Common;
using PegePlayer.Common.Utils;

namespace PegPlayer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintGretings();
            try
            {
                StartGame();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nUps and error has occurred\n");
                Console.WriteLine(e);
            }
                       
            Console.WriteLine("\nThanks for play!");
            Console.ReadKey();
        }

        private static void StartGame()
        {
            while (true)
            {
                var filePath = ReadFilePath();
                var pegBoard = PegBoard.FromSource(
                    PegBoardFileSource.ParseFile(filePath)
                );

                var pegsBoardSolution = PoorManMemoizingPegsSolution.Create(pegBoard);
                pegsBoardSolution.Resolve();
                PrintSolution(pegBoard, pegsBoardSolution.GetBestPositions());

                Console.WriteLine("Would you like to play one more time? yes/no");
                var answer = Console.ReadLine();
                if (answer == "no" || answer == "n")
                {
                    break;
                }
            }
        }

        private static void PrintSolution(PegBoard pegBoard, IEnumerable<Peg> pegBestInitialPositions)
        {
            Console.WriteLine("Process done\n\n");
            var solution = pegBestInitialPositions.First();
            Console.WriteLine($"El espacio inicial que presenta mayor probabilidad de llegar al espacio meta {pegBoard.Goal}, es el {solution.Column} con un probabilidad de {solution.Probability * 100}%");
            Console.WriteLine("\n\n");
        }

        private static string ReadFilePath()
        {
            Console.WriteLine("Please specify the file with Peg game parameters");
            var path = Console.ReadLine();
            while (!File.Exists(path))
            {
                Console.WriteLine("\nFile not found, please try again\n");
                path = Console.ReadLine();
            }

            return path;
        }

        private static void PrintGretings()
        {
            Console.WriteLine("\n\nWelcome to this poors man Peg Player simulator\n\n");
        }
    }
}
