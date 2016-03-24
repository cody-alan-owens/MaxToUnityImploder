using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class HierarchyZone : Rectangle {
        public HierarchyZone Parent;
        public List<HierarchyZone> Children;
        public Assignment Assignment;

        public HierarchyZone(List<Point> points, HierarchyZone parent, List<HierarchyZone> children) :base(points)
        {
            this.Parent = parent;
            this.Children = children;
        }

        public HierarchyZone(List<Point> points, HierarchyZone parent) : base(points)
        {
            this.Parent = parent;
            this.Children = new List<HierarchyZone>();
        }

        public HierarchyZone (List<Point> points, List<HierarchyZone> children) : base(points)
        {
            this.Children = children;
        }

        public HierarchyZone(List<Point> points, HierarchyZone parent, HierarchyZone child) : base(points)
        {
            this.Parent = parent;
            this.Children = new List<HierarchyZone>();
            this.Children.Add(child);
        }

        public HierarchyZone(List<Point> points) : base(points)
        {
            this.Children = new List<HierarchyZone>();
        }
        public HierarchyZone() : base(new List<Point>() {
            new Point(float.PositiveInfinity, float.PositiveInfinity, 0),
            new Point(float.PositiveInfinity,float.NegativeInfinity, 0),
            new Point(float.NegativeInfinity,float.NegativeInfinity, 0),
            new Point(float.NegativeInfinity,float.PositiveInfinity,0)
            })
        {
            this.Children = new List<HierarchyZone>();
        }
    }
}