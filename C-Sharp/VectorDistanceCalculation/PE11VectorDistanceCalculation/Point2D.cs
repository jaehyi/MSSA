using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE11VectorDistanceCalculation
{
    //Comments in this class apply for both objects
    //Why have two objects? You can accomplish everything with one object and an extra property z here
    //if you remove the methods from this class it can become a struct
    class Point2D : IEquatable<Point2D>
    {
        //I like this class being immutable but no reason for the private set
        public int X { get; private set; }
        public int Y { get; private set; }
        public Point2D()
        {
            //two problems with assigning X, Y here. 
            //1. theyre value types, what is the default for the value types?
            //2. what does this constructor accomplish?
            X = 0;
            Y = 0;
        }

        //How do I set this object?
        //Both constructors have internal assignments and no way for you to set the vector
        //but I may not understand the assignment
        public Point2D(Random random, int min = 1, int max = 101)
        {
            X = random.Next(min, max);
            Y = random.Next(min, max);
        }

        //This method doesnt belong here. This method belongs in a static utility class 
        //Helper classes can maintain state
        //Utility classes have static methods and only operate on parameters and return something
        public double CalculateDistanceTo(Point2D point)
        {
            return Math.Sqrt(Math.Pow((point.X - X), 2) + Math.Pow((point.Y - Y), 2));
        }

        //good
        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public bool Equals(Point2D other)
        {
            //idk but I feel like there is a bug here
            return X == other.X && Y == other.Y;
        }
    }
}
