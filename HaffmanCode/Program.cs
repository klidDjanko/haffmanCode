using System;

namespace HaffmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");

            Console.WriteLine("Ввести текст для кодирования:");
            string input = Console.ReadLine();
            Haffman haffman = new Haffman();
            haffman.Compress(input);

            Console.WriteLine();

            Console.WriteLine("Ввести код для декодирования:");
            input = Console.ReadLine();
            haffman.Decompress(input);

            Console.ReadKey();
        }


    }
}
