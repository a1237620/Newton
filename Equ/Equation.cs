using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Newton.Equ
{
    /// <summary>
    /// Выражение и его производные
    /// </summary>
    internal class Equation : IDisposable
    {
        /// <summary>
        /// Функция
        /// </summary>
        internal ObservableCollection<EquationItem> Equations;
        /// <summary>
        /// Признак очистки
        /// </summary>
        private bool isDisposed;
        /// <summary>
        /// Производная функции
        /// </summary>
        private IList<EquationItem> derivatives;

        public Equation()
        {
            Equations = new();
            derivatives = new List<EquationItem>();
            Equations.CollectionChanged += EquationItems_CollectionChanged;
        }

        public IList<EquationItem> Derivatives
        {
            get => derivatives.AsReadOnly<EquationItem>();
            private set => derivatives = value;
        }

        private void EquationItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Equations == null) return;

            //очистить список производных
            if (derivatives == null)
            {
                derivatives = new List<EquationItem>();
            }
            else
            {
                derivatives.Clear();
            }



            //наполнить список производных
            foreach (var item in Equations)
            {
                if (item.Power > 0)
                {
                    //найти производную
                    derivatives.Add(new EquationItem(factor:item.Factor * item.Power, power: item.Power - 1));
                }
            }
        }


        public override string ToString()
        {
            /// Выражение в виде строки
            if (Equations.Count == 0) return "";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"f(x)=");

            string plus = null;

            for (int i = 0; i < Equations.Count; i++)
            {
                var item = Equations[i];
                if (i > 0 && item.Factor > 0)
                {
                    plus = "+";
                }
                stringBuilder.Append($"{plus}{item}");
            }

            return stringBuilder.ToString();
        }




        public void Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;
            if (Equations != null)
            {
                Equations.CollectionChanged -= EquationItems_CollectionChanged;
                Equations.Clear();
            }
            derivatives?.Clear();

            Equations = null;
            derivatives = null;
        }
    }
}

