namespace VectorDistanceCalculation
{
    abstract class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public abstract override string ToString();
        public abstract double CalculateDistanceTo(object point);
        public abstract override bool Equals(object point);
        public abstract override int GetHashCode();
    }
}