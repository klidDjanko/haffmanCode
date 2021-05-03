using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HaffmanCode
{
    class Haffman
    {
        public void Compress(string input)
        {
            //Сохраняем список символов их частот и весов 
            List<Nodes> charsMap = new List<Nodes>();
            //Сохраняем список частот символов и их веса (01)
            List<Nodes> haffmanTree = new List<Nodes>();
            //Считаем частоты в ведённом тексте, помещаем их в charMap
            CountFrequency(input, charsMap);

            Console.WriteLine("Done!");

            //выполняем сортировку charMap по убыванию частот linq
            charsMap = charsMap.OrderByDescending(node => node.Frequency).ToList();

            while(charsMap.Count > 1)
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

            for (int i = 1; i < charsMap.Count; i += 2)
            {
                Nodes obj = (Nodes)charsMap[i];
            }
        }

        private static void CountFrequency(string input, List<Nodes> charsMap)
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
        }
    }
}
