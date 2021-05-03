namespace HaffmanCode
{
    class LetterCode
    {
        //Префинсный код буквы
        string code = "";
        //Сама буква
        string ch = "";

        /// <summary>
        /// Конструктор для добавления кода буквы
        /// </summary>
        public LetterCode(string ch, string code)
        {
            this.ch = ch;
            this.code = code;
        }

        //Свойства для получения кода буквы и самой буквы
        public string Ch
        {
            get { return ch; }
        }
        public string Code
        {
            get { return code; } 
        }
    }
}
