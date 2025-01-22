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
        private const int maxIteration = 200;

        /// <summary>
        /// Точность нахождения корня, ε 
        /// </summary>
        private const double epsilon = 0.02;

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

                Console.WriteLine($"Уравнение {mathEquation}");
                //точки экстремума (мин,макс)
                var list = EquationHelper.GetPointsExtremum(mathEquation);

                if (list!=null && list.Any())
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        double x = item.X + epsilon * item.Koef;

                        if (CalcX(x, out double result))
                        {
                            Console.WriteLine($"Решение уравнения x{i}={result}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Решение уравнения не найдено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            for (int i = 0; i < maxIteration; i++)
            {
                Console.WriteLine($"{i}. x0={x0} ");
                Task.Delay(100).Wait();

                x1 = EquationHelper.GetNewtonX(mathEquation, x0);

                //критерий остановки вычислений по приращению
                if (Math.Abs(x1 - x0) < epsilon)
                {
                    //сработал критерий остановки  вычислений по приращению
                    Console.WriteLine($"Сработал критерий остановки вычислений по приращению e={epsilon}");
                    return true;
                }

                //Критерий остановки вычислений на основе близости функции к нулю
                //var f1 = Math.Abs(GetEquation(x1));
                //if (f1 < epsilon)
                //{
                //    // выход
                //    Console.WriteLine("Сработал критерий остановки вычислений на основе близости функции к нулю");
                //    return false;
                //}

                //Критерий остановки вычислений на основе последних повторений
                if (x0last == x1)
                {
                    Console.WriteLine("Сработал критерий остановки вычислений на основе последних повторений");
                    return false;
                }
                x0last = x0;
                x0 = x1;
            }
            //сработал критерий остановки вычислений по кол-ву итераций
            Console.WriteLine("Сработал критерий остановки вычислений по превышению количества итераций");
            return false;
        }
    }
}

