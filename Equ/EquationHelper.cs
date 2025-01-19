﻿namespace Newton.Equ
{
    internal class EquationHelper
    {
        /// <summary>
        /// Получить результат выражения f(x)
        /// </summary>
        /// <param name="x0">значение X</param>
        /// <param name="list">список элемента выражения</param>
        /// <returns></returns>
        internal static double GetFx(double x, IList<EquationItem> list)
        {
            if (list==null || list.Count == 0) return 0;

            double fx = 0;
            foreach (var item in list)
            {
                fx += item.Factor * Math.Pow(x, item.Power);
            }
            return fx;
        }
        /// <summary>
        /// находим коэффициенты квадратного уравнения
        /// </summary>
        /// <param name="derivativeList"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static bool KoefABC(IList<EquationItem> derivativeList, out double a, out double b, out double c)
        {
            a = 0;
            b = 0;
            c = 0;
            //исследуем выражение
            if (derivativeList==null || derivativeList.Count == 0) return false;

            var argA = derivativeList?.FirstOrDefault(a => a.Power == 2);

            if (argA == null) return false;

            a = argA.Factor;

            var argB = derivativeList?.FirstOrDefault(a => a.Power == 1);
            if (argB != null)
            {
                b = argB.Factor;
            }
            var argC = derivativeList?.FirstOrDefault(a => a.Power == 0);
            if (argC != null)
            {
                c = argC.Factor;
            }

            return true;
        }

        /// <summary>
        /// Исследуем функцию на монотонность
        /// </summary>
        /// <param name="derivativeList"> </param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="fx1"></param>
        /// <param name="fx2"></param>
        /// <param name="isZero">решение уравнения f(0)=0</param>
        /// <returns></returns>
        internal static List<PointExt> GetPointsМonotone(IList<EquationItem> derivativeList, double x1, double x2, double fx1, double fx2)
        {
            List<PointExt> list = new List<PointExt>();
            //точка x∈(−∞;x1)
            double x1d = x1 - 1;
            //точка x∈(x2;+∞)
            double x2d = x2 + 1;

            //определим знаки производной в каждом промежутке
            var fx1d = GetFx(x1d, derivativeList);
            if (fx1d > 0)
            {
                //При x∈(−∞;x1) производная y′> 0, поэтому функция возрастает на данных промежутках.
                if (fx1 > 0)
                {
                    //есть точка пересечения.
                    //т.к. точка экстремума в области отрицательных значений,
                    //отступ для первой точки касательной  делаем в −∞, т.е. Koef = -1 
                    //1-ое решение
                    list.Add(new PointExt
                    {
                        X = x1,
                        Koef = -1
                    });
                    //TODO 
                    //Для отрицательного значения функции 
                }
            }
            //TODO исследовать для других участков

            return list;
        }


        /// <summary>
        /// Получить точки экстремума (мин,макс) из производного выражения
        /// </summary>
        /// <param name="derivativeList"></param>
        internal static List<PointExt> GetPointsExtremum(Equation mathEquation)
        {
            //TODO
            //Исследуем функцию на экстремумы
            if (!KoefABC(mathEquation.Derivatives, out double a, out double b, out double c))
                return null;

            //точка перегиба х1
            double x1 = GetX(a, b, c, -1);
            //точка перегиба х2

            double x2 = GetX(a, b, c);

            if (x1 > x2)
            {
                var temp = x1;
                x1 = x2;
                x2 = temp;
            }

            //значение функции в точке перегиба f(x1)
            double fx1 = GetFx(x1, mathEquation.Equations.ToList());
            //значение функции в точке перегиба  f(x2)
            double fx2 = GetFx(x2, mathEquation.Equations.ToList());

            var list = GetPointsМonotone(mathEquation.Derivatives, x1, x2, fx1, fx2);
            return list;
        }

        /// <summary>
        /// Получить решение квадратного уравнения  х
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private static double GetX(double a, double b, double c, double koefDiscriminant = 1)
        {
            //дискриминант
            double discr = Math.Pow(b, 2) - 4 * a * c;

            double x = (-b + koefDiscriminant * Math.Sqrt(discr)) / (2 * a);

            return x;
        }

        /// <summary>
        /// Вычисление приближённого значения корня уравнения на (n+1)-ой итерации из ур-ния касательной
        /// </summary>
        /// <param name="x0">приближённое значение корня уравнения на (n)-ой итерации</param>
        internal static double GetNewtonX(Equation mathEquation, double x0)
        {
            //приближённое значение корня уравнения на (n+1)-ой итерации
            var resEqu = GetFx(x0, mathEquation.Equations.ToList());
            var resDer = GetFx(x0, mathEquation.Derivatives);

            var x1 = x0 - resEqu / resDer;

            return x1;
        }

    }

    /// <summary>
    /// Стартовая точка
    /// </summary>
    internal class PointExt
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Коэффициент
        /// </summary>
        public int Koef { get; set; }
    }
}

