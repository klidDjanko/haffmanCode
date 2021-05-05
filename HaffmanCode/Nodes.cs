namespace HaffmanCode
{
    class Nodes
    {
        //Символ или группа символов
        string chars;
        //Частота символа или суммарная частота
        int frequency;
        //Вес - ноль или единица
        int weight;

        /// <summary>
        /// Конструктор для создания пары символ - его частота
        /// </summary>
        public Nodes(string chars, int frequency):this(chars, frequency, 0) {}

        /// <summary>
        /// Конструктор для создания пары символ - его частота и вес (01)
        /// </summary>
        public Nodes(string chars, int frequency, int weight)
        {
            this.chars = chars;
            this.frequency = frequency;
            this.weight = weight;
        }

        //Свойства для доступа к значению частоты, символам, весам
        public int Frequency
        {
            get { return frequency; }
        }
        public string Chars
        {
            get { return chars; } 
        }
        public int Weight
        {
            get { return weight; }
        }
    }
}
