using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE11VectorDistanceCalculation
{
    class Point2D : IEquatable<Point2D>
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(int min = 1, int max = 101)
        {
            Random random = new Random();
            X = random.Next(min, max);
            Y = random.Next(min, max);
        }

        public double CalculateDistanceTo(Point2D point)
        {
            return Math.Sqrt(Math.Pow((point.X - X), 2) + Math.Pow((point.Y - Y), 2));
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public bool Equals(Point2D other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
