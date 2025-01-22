namespace Newton.Equ
{
    /// <summary>
    /// Член выражения
    /// </summary>
    internal class EquationItem
    {
        /// <summary>
        /// число
        /// </summary>
        internal double Factor { get; private set; }

        /// <summary>
        /// степень
        /// </summary>
        internal int Power { get; private set; }

        /// <summary>
        /// Конструктор. 
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="power"></param>
        public EquationItem(double factor, int power)
        {
            //Инициализация закрытых св-в через конструктор
            Factor = factor;
            Power = power;
        }

        public override string ToString()
        {
            if (Factor == 0)
            {
                return "";
            }
            string pow = null;
            string arg = null;
            string factor = $"{Factor}";

            if (Power > 0)
            {
                arg = "x";

                if (Factor == 1)
                {
                    factor = "";
                }
                if (Power > 1)
                {
                    pow = $"^{Power}";

                }
            }
            return $"{factor}{arg}{pow}";
        }
    }
}

