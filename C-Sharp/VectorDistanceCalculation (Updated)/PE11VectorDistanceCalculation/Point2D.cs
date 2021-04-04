using System;

namespace PE11VectorDistanceCalculation
{
    class Point2D : Point
    {
        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public override double CalculateDistanceTo(object point)
        {
            if (point is Point2D p)
                return Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2));
            else throw new ArgumentException("The object being compared to must be of Point2D type.");
        }

        public override bool Equals(object point)
        {
            if (point is Point2D p)
                return X == p.X && Y == p.Y;
            else throw new ArgumentException("The object being compared to must be of Point2D type.");
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }
    }
}
