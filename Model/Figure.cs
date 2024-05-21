using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ForTopSystems.Model
{
    abstract class ViewModel : INotifyPropertyChanged// базовый класс для фигур
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string PropName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropName);
            return true;
        }
    }
    abstract class Figure : ViewModel// базовый класс для фигур
    {
        public abstract double Sq(); // у каждой фигуры есть площадь
        public abstract double Length(); // у каждой фигуры есть периметр
    }
    class Square : Figure, IPainting // Релизуем интерфейс для возможности прорисовки
    {
        private double _A = 100;
        public double A
        {
            get => _A;
            set => Set(ref _A, value);
        }
        public override double Length() => 4 * A;

        public override double Sq() => A * A;

        public string Paint()
        {
            return $"{A}";
        }
    }
    class Triangle : Figure, IPainting // Релизуем интерфейс для возможности прорисовки
    {
        public double[] Arr { get; set; }
        public readonly double A;
        public readonly double B;
        public readonly double C;

        public Triangle(double[] a)
        {
            Arr = a;
            A = Math.Abs(Math.Sqrt(Math.Pow(Arr[2] - Arr[0], 2) + Math.Pow(Arr[3] - Arr[1], 2)));
            B = Math.Abs(Math.Sqrt(Math.Pow(Arr[4] - Arr[2], 2) + Math.Pow(Arr[5] - Arr[3], 2)));
            C = Math.Abs(Math.Sqrt(Math.Pow(Arr[4] - Arr[0], 2) + Math.Pow(Arr[5] - Arr[1], 2)));
        }

        public override double Length() => A + B + C;

        public override double Sq()
        {
            var halfLength = Length() / 2;
            return Math.Sqrt(Math.Abs(halfLength * (halfLength - A) * (halfLength - B) * (halfLength - C)));
        }
        // (50, 150), (150, 50) и (250, 150)
        public string Paint()
        {
            return $"{Arr[0]}, {Arr[1]}, {Arr[2]}, {Arr[3]}, {Arr[4]}, {Arr[5]}";
        }
    }
}
