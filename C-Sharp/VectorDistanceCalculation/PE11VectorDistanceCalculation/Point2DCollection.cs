using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE11VectorDistanceCalculation
{
    class Point2DCollection
    {
        private const int collectionSize = 100;
        public List<Point2D> Points { get; private set; } = new List<Point2D>(collectionSize);

        public List<Point2D> TwoClosestPoints { get; private set; } = new List<Point2D>(2);
        public double DistanceBetweenTwoClosestPoints { get; private set; }

        private Random random = new Random();
        public Point2DCollection()
        {
            for (int i = 0; i < collectionSize;)
            {
                Point2D point = new Point2D(random);
                if (Points.Contains(point)) continue; // This line ensures there are no same points in the list
                Points.Add(point);
                i++;
            }

            calculateTwoClosestPoints();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(collectionSize);

            for (int i = 0; i < Points.Count; i++)
                sb.AppendLine($"Point {i + 1}: ({Points[i]})");

            return sb.ToString();
        }

        private void calculateTwoClosestPoints()
        {
            double distance = Points[0].CalculateDistanceTo(Points[1]);
            List<Point2D> twoClosestPoints = new List<Point2D>(2) { Points[0], Points[1] };

            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = i + 1; j < Points.Count; j++)
                {
                    double tempDistance = Points[i].CalculateDistanceTo(Points[j]);
                    List<Point2D> tempList = new List<Point2D>(2) { Points[i], Points[j] };
                    
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
    }
}
