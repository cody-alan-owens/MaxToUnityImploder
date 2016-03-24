using System;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Assignment : Arc
    {
        public List<HierarchyZone> Children;
        public Assignment(List<Point> points, List<HierarchyZone> children) : base(points)
        {
            this.Children = children;
        }

        public Assignment(List<Point> points) : base(points)
        {
            this.Children = new List<HierarchyZone>();
        }
    }
}