using System;
namespace PlanImploder
{
    public class Assignment
    {
        private Zone Parent;
        private Zone Child;
        public Assignment(Zone parent, Zone child)
        {
            this.Parent = parent;
            this.Child = child;
        }
    }
}