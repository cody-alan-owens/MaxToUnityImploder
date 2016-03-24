using System.Collections.Generic;
namespace PlanImploder
{
    public class Spline
    {
        public List<Point> Points;
        public enum SplineType { RECTANGLE, ARC };
        public SplineType Type;
        public Spline(List<Point> points, SplineType type)
        {
            this.Points = points;
            this.Type = type;
        }
    }
}