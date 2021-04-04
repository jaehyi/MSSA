using System;

namespace VectorDistanceCalculation
{
    class Point3D : Point
    {
        public int Z { get; set; }

        public override double CalculateDistanceTo(object point)
        {
            if (point is Point3D p)
                return Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2) + Math.Pow(p.Z - Z, 2));
            else throw new ArgumentException("The object being compared to must be of Point3D type.");
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }

        public override bool Equals(object point)
        {
            if (point is Point3D p)
                return X == p.X && Y == p.Y && Z == p.Z;
            else throw new ArgumentException("The object being compared to must be of Point3D type.");
        }

        public override int GetHashCode()
        {
            int hashCode = new { X, Y, Z }.GetHashCode();
            return hashCode;
        }
    }
}
