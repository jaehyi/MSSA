using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE11VectorDistanceCalculation
{
    class Point3D : IEquatable<Point3D>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Point3D(int min = 1, int max = 1001)
        {
            Random random = new Random();
            X = random.Next(min, max);
            Y = random.Next(min, max);
            Z = random.Next(min, max);
        }

        public double CalculateDistanceTo(Point3D point)
        {
            return Math.Sqrt(Math.Pow((point.X - X), 2) + Math.Pow((point.Y - Y), 2) + Math.Pow((point.Z - Z), 2));
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }

        public bool Equals(Point3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }
}
