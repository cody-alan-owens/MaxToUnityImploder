using System;
namespace PlanImploder
{
    public class Basepoint : Point, IPoint
    {
        public Point Point { get; private set; }
        public HierarchyZone Zone;
        public Basepoint(Point point) :base(point.X, point.Y, point.Z)
        {

        }
        public Basepoint(float x, float y, float z) : base(x, y, z)
        {

        }

        public Point GetPoint()
        {
            return this.Point;
        }
    }
}