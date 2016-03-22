using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Zone {
        public Rectangle Rectangle { get; }
        public Zone Parent;
        public List<Zone> Children;

        public Zone(Rectangle space, Zone parent, List<Zone> children)
        {
            this.Rectangle = space;
            this.Parent = parent;
            this.Children = children;
        }

        public Zone(Rectangle space, Zone parent)
        {
            this.Rectangle = space;
            this.Parent = parent;
            this.Children = null;
        }

        public Zone (Rectangle space, List<Zone> children)
        {
            this.Rectangle = space;
            this.Parent = null;
            this.Children = children;
        }

        public Zone(Rectangle space, Zone parent, Zone child)
        {
            this.Rectangle = space;
            this.Parent = parent;
            this.Children = new List<Zone>();
            this.Children.Add(child);
        }

        public Zone(Rectangle space)
        {
            this.Rectangle = space;
            this.Parent = null;
            this.Children = null;
        }
    }
}