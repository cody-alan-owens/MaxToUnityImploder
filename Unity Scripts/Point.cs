using System;
namespace PlanImploder
{
    public class Point
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}