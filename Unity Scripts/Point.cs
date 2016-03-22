using System;
namespace PlanImploder
{
    public class Point
    {
        public decimal X { get; }
        public decimal Y { get; }
        public decimal Z { get; }
        public Point(decimal x, decimal y, decimal z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}