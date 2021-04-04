using System;
using System.Collections.Generic;
using System.Text;

namespace VectorDistanceCalculation
{
    class PointCollection<T> where T : Point, new()
    {
        public int MinVal { get; private set; }
        public int MaxVal { get; private set; }
        public int CollectionSize { get; private set; }
        public List<T> Points { get; private set; } = new List<T>();

        public List<T> TwoClosestPoints { get; private set; } = new List<T>(2);
        public double DistanceBetweenTwoClosestPoints { get; private set; }

        private Random random = new Random();
        public PointCollection(int collectionSize, int minVal, int maxVal)
        {
            CollectionSize = collectionSize;
            MinVal = minVal;
            MaxVal = MaxVal;
            for (int i = 0; i < CollectionSize;)
            {
                T point = new T();
                if (point is Point2D point2D)
                {
                    point2D.X = random.Next(minVal, maxVal + 1);
                    point2D.Y = random.Next(minVal, maxVal + 1);
                }
                else if (point is Point3D point3D)
                {
                    point3D.X = random.Next(minVal, maxVal + 1);
                    point3D.Y = random.Next(minVal, maxVal + 1);
                    point3D.Z = random.Next(minVal, maxVal + 1);
                }
                if (Points.Contains(point)) continue; // This line ensures there are no same points in the list
                Points.Add(point);
                i++;
            }

            CalculateTwoClosestPoints();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(CollectionSize);

            for (int i = 0; i < Points.Count; i++)
                sb.AppendLine($"Point {i + 1}: ({Points[i]})");

            return sb.ToString();
        }

        private void CalculateTwoClosestPoints()
        {
            double distance = Points[0].CalculateDistanceTo(Points[1]);
            List<T> twoClosestPoints = new List<T>(2) { Points[0], Points[1] };

            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = i + 1; j < Points.Count; j++)
                {
                    double tempDistance = Points[i].CalculateDistanceTo(Points[j]);
                    List<T> tempList = new List<T>(2) { Points[i], Points[j] };

                    if (distance > tempDistance)
                    {
                        distance = tempDistance;
                        twoClosestPoints = tempList;
                    }
                }
            }

            TwoClosestPoints = twoClosestPoints;
            DistanceBetweenTwoClosestPoints = distance;
        }

        public void DisplayTwoClosestPoints()
        {
            Console.WriteLine($"\nTwo closest points are Point {Points.IndexOf(TwoClosestPoints[0]) + 1} ({TwoClosestPoints[0]}) and" +
                            $" Point {Points.IndexOf(TwoClosestPoints[1]) + 1} ({TwoClosestPoints[1]}) " +
                            $"and the distance between them is {DistanceBetweenTwoClosestPoints:N4}\n");
        }
    }
}
