using System;
namespace PlanImploder
{
    public class Optionpoint : Point, IPoint
    {
        public Point Point { get; private set; }
        public HierarchyZone Zone;
        public Optionpoint(Point point) :base(point.X, point.Y, point.Z)
        {

        }
        public Optionpoint(float x, float y, float z) : base(x, y, z)
        {

        }

        public Point GetPoint()
        {
            return this.Point;
        }
    }
}