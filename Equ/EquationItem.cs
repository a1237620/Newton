namespace Newton.Equ
{
    /// <summary>
    /// Член выражения
    /// </summary>
    /// <param name="Factor">число</param>
    /// <param name="Power">степень</param>
    internal record class EquationItem(double Factor, int Power)
    {
        public override string ToString()
        {
            if (Factor == 0)
            {
                return "";
            }

            string pow = "";
            string arg = "";
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

