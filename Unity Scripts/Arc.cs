using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Arc
    {
        public List<Point> Points;
        public Basepoint Basepoint;
        public List<Optionpoint> Optionpoints;
        public Zone BaseZone;
        public Zone OptionZone;

        public Arc(List<Point> points)
        {
            this.Points = points;
            this.Optionpoints = new List<Optionpoint>();
        }
    }
}