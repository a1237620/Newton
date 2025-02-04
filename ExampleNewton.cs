using Newton.Equ;
using System.Collections;

namespace Newton
{

    /// <summary>
    /// Пример вычисления приближённого значения корня уравнения 
    /// </summary>
    internal class ExampleNewton
    {
        #region Fields
        /// <summary>
        /// Макс. кол-во итераций
        /// </summary>
        public const int MAX_ITERATION = 200;

        /// <summary>
        /// Точность нахождения корня, ε 
        /// </summary>
        public const double EPSOLON = 0.02;

        /// <summary>
        /// Выражение и его производные
        /// </summary>
        private Equation mathEquation;

        #endregion

        /// <summary>
        /// Решаем и выводим на консоль
        /// </summary>
        internal void Calculate()
        {
            try
            {
                CreateEquation();

                $"Уравнение {mathEquation}".ToConsole(ConsoleColor.Green);
                new string('-', 50).ToConsole();
                //точки экстремума (мин,макс)
                var list = EquationHelper.GetPointsExtremum(mathEquation);

                if (list != null && list.Any())
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        double x = item.X + EPSOLON * item.Koef;

                        if (CalcX(x, out double result))
                        {
                            new string('-', 50).ToConsole();
                            $"Решение уравнения x{i}={result}".ToConsole(ConsoleColor.Green);
                        }
                    }
                }
                else
                {
                    "Решение уравнения не найдено.".ToConsole(ConsoleColor.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToConsole();
            }
        }

        /// <summary>
        /// Создание выражения
        /// </summary>
        private void CreateEquation()
        {
            //Math.Pow(x, 3) - 2 * x + 2;
            //f(x)=x^3-2x+2

            mathEquation = new Equation();
            mathEquation.Equations.Add(new EquationItem(Factor: 1, Power: 3));
            mathEquation.Equations.Add(new EquationItem(Factor: -2, Power: 1));
            mathEquation.Equations.Add(new EquationItem(Factor: 2, Power: 0));
        }
        /// <summary>
        /// Вычисление приближённого значения корня уравнения
        /// </summary>
        /// <param name="x0">начальное значение корня уравнения </param>
        /// <param name="x1">приближённое значение корня уравнения на (n+1)-ой итерации</param>
        private bool CalcX(double x0, out double x1)
        {
            //предпоследнее значение X
            double x0last = x0;
            x1 = x0;
            for (int i = 0; i < MAX_ITERATION; i++)
            {
                $"{i}. x0={x0} ".ToConsole();
                Task.Delay(100).Wait();

                x1 = EquationHelper.GetNewtonX(mathEquation, x0);

                //критерий остановки вычислений по приращению
                if (Math.Abs(x1 - x0) < EPSOLON)
                {
                    //сработал критерий остановки  вычислений по приращению
                    $"Сработал критерий остановки вычислений по приращению e={EPSOLON}".ToConsole();
                    return true;
                }

                //Критерий остановки вычислений на основе близости функции к нулю
                //var f1 = Math.Abs(GetEquation(x1));
                //if (f1 < epsilon)
                //{
                //    // выход
                //    "Сработал критерий остановки вычислений на основе близости функции к нулю");
                //    return false;
                //}

                //Критерий остановки вычислений на основе последних повторений
                if (x0last == x1)
                {
                    "Сработал критерий остановки вычислений на основе последних повторений".ToConsole();
                    return false;
                }
                x0last = x0;
                x0 = x1;
            }
            //сработал критерий остановки вычислений по кол-ву итераций
            "Сработал критерий остановки вычислений по превышению количества итераций".ToConsole();
            return false;
        }
    }
}

