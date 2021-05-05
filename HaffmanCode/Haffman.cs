using System;
using System.Collections.Generic;
using System.Linq;

namespace HaffmanCode
{
    class Haffman
    {
        //Сохраняем список символов их частот и весов 
        List<Nodes> charsMap = new List<Nodes>();
        //Сохраняем список частот символов и их веса (01)
        List<Nodes> haffmanTree = new List<Nodes>();
        //Сохраняем коды символов ()
        List<LetterCode> haffmanCodes = new List<LetterCode>();

        public void Compress(string input)
        {
            //Считаем частоты в ведённом тексте, помещаем их в charMap, получаем алфавит в виде строки
            string alphabet = CountFrequency(input, charsMap);

            //выполняем сортировку charMap по убыванию частот linq
            charsMap = charsMap.OrderByDescending(node => node.Frequency).ToList();

            //Выводим частоты на консоль (они нужны для дешифрования)
            Console.WriteLine();
            Console.WriteLine("Частоты алфавита:");
            foreach (Nodes obj in charsMap)
            {
                Console.WriteLine(obj.Chars + " -> " + obj.Frequency);
            }
            Console.WriteLine();

            //Основной код, который реализует код Хаффмана 
            HaffmanCoreMethod(alphabet);

            //Выводим на консоль коды букв (только для отладки)
            Console.WriteLine();
            foreach (LetterCode obj in haffmanCodes)
            {
                Console.WriteLine(obj.Ch + " -> " + obj.Code);
            }
            Console.WriteLine();

            //Теперь переводим исходную строку в код хаффмана
            string compress = "";
            for (int l = 0; l < input.Length; l++)
            {
                for (int c = 0; c < haffmanCodes.Count; c++)
                {
                    if (haffmanCodes[c].Ch == input[l].ToString()) compress += haffmanCodes[c].Code;
                }
            }
            //Выводим код хаффмана
            Console.WriteLine("Результат сжатия: " + compress);

            //Зачистка переменных
            charsMap.Clear();
            haffmanTree.Clear();
            haffmanCodes.Clear();
        }

        public void Decompress(string input)
        {
            //Используемый алфавит 
            string alphabet = "";
            //Вектор символов - их частот для формирования кодов Хаффмана и дешифрования
            Console.WriteLine("Требуется ввести символы и их частоты, при завершении ввода нажмите End");
            while (true)
            {
                Console.Write("Символ: = "); string chars = Console.ReadKey().KeyChar.ToString();
                alphabet += chars;
                Console.Write(" и его встречаемость = "); int frequency = Convert.ToInt32(Console.ReadLine());
                Nodes charNode = new Nodes(chars, frequency);
                charsMap.Add(charNode);
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.End) break;
            }

            //Формируем коды букв
            HaffmanCoreMethod(alphabet);

            //Декодируем
            string code = "";
            string decodeString = "";
            //Внешний цикл идёт по символьно для декодируемого кода 
            for (int i = 0; i < input.Length; i++)
            {
                //собирается код символа
                code += input[i].ToString();
                //Внутренний цикл идёт по известным кодам Хаффмана для символов алфавита
                for(int k = 0; k < alphabet.Length; k++)
                {
                    LetterCode knownCode = haffmanCodes[k];
                    //и если собираемый код соответсвует какому-то известному коду, то декодируем его в символ
                    if (knownCode.Code == code)
                    {
                        decodeString += knownCode.Ch;
                        code = "";
                        break;
                    }
                }
            }
            Console.WriteLine("Получили следующее: " + decodeString);
        }

        private void HaffmanCoreMethod(string alphabet)
        {
            while (charsMap.Count > 1)
            {
                //Получаем длину list charMap
                int length = charsMap.Count - 1;
                //Берём два наименьших элемента в charMap (частоты)
                int firstMin = charsMap[length].Frequency;
                int secondMin = charsMap[length - 1].Frequency;

                //Сравниваем частоты этих двух минимальных элементов
                if (firstMin > secondMin)
                {
                    //Заносим эти символы в дерево, букве с большей частотой присваиваем 1, другой 0
                    haffmanTree.Add(new Nodes(charsMap[length].Chars, charsMap[length].Frequency, 1));
                    haffmanTree.Add(new Nodes(charsMap[length - 1].Chars, charsMap[length - 1].Frequency, 0));
                }
                else
                {
                    //Заносим эти символы в дерево, букве с большей частотой присваиваем 1, другой 0
                    haffmanTree.Add(new Nodes(charsMap[length].Chars, charsMap[length].Frequency, 0));
                    haffmanTree.Add(new Nodes(charsMap[length - 1].Chars, charsMap[length - 1].Frequency, 1));
                }

                //Добавляем в list новый элемент из этих минимальных (это буквосочетание прежних и сумма их частот)
                charsMap.Add(new Nodes(charsMap[length].Chars + charsMap[length - 1].Chars, charsMap[length].Frequency + charsMap[length - 1].Frequency));
                //Удаляем прежние минимальные два
                charsMap.Remove(charsMap[length]);
                charsMap.Remove(charsMap[length - 1]);

                //Сортируем charMap по возрастанию
                charsMap = charsMap.OrderByDescending(node => node.Frequency).ToList();
            }

            //Формируем инвертированные коды букв (просмотр дерева весов идёт не от корня, а от потомков)
            //Внешний цикл идёт по символам алфавита
            for (int l = 0; l < alphabet.Length; l++)
            {
                //Получаем символ алфавита
                string ch = alphabet[l].ToString();
                string code = "";
                //Внутренний цикл идёт по дереву с нулями и единицами, где у каждой ноды есть ещё и буквы (или их сочетания)
                for (int t = 0; t < haffmanTree.Count; t++)
                {
                    //смотрим есть ли у ноды такая буква и если да, берём из неё вес (0 или 1)
                    if (haffmanTree[t].Chars.Contains(ch)) code += haffmanTree[t].Weight;
                }
                //Инвертируем код
                char[] temp = code.ToCharArray();
                Array.Reverse(temp);
                code = String.Concat<char>(temp);
                //Запоминаем символ и соответсвующий ему код
                haffmanCodes.Add(new LetterCode(ch, code));
            }
        }

        private static string CountFrequency(string input, List<Nodes> charsMap)
        {
            //Буфер для подсчёта частот
            string charsUsed = "";
            //Считаем частоты в заданном тексте
            for (int i = 0; i < input.Length; i++)
            {
                if (!charsUsed.Contains(input[i]))
                {
                    //Запоминаем символ
                    charsUsed += input[i];
                    int countChar = 0;
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (input[j] == input[i]) countChar++;
                    }
                    //Запоминаем пару символ-частота
                    Nodes node = new Nodes(input[i].ToString(), countChar);
                    //Помещаем эту пару в list
                    charsMap.Add(node);
                }
            }
            return charsUsed;
        }
    }
}
