using System;
namespace PlanImploder
{
    public class Basepoint
    {
        public Point Point { get; private set; }
        public Basepoint(Point point)
        {
            this.Point = point;
        }
    }
}