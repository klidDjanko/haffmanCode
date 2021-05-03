using System;
using System.Collections;
using System.Collections.Generic;

namespace HaffmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");

            Console.WriteLine("Ввести текст для сжатия:");
            string input = Console.ReadLine();
            Haffman haffman = new Haffman();
            haffman.Compress(input);
        }


    }
}
