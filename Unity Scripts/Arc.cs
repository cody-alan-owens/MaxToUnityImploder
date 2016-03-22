using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Arc
    {
        private Point P1;
        private Point P2;
        private Point Apex;
        public Basepoint Basepoint;
        public Optionpoint Optionpoint;
        public Zone BaseZone;
        public Zone OptionZone;

        public Arc(Point p1, Point p2, Point p3)
        {
            this.P1 = p1;
            this.P2 = p2;
            if (decimal.Equals(GetDistance(p1, p2), GetDistance(p2, p3))){
                this.Apex = p2;
                this.P2 = p3;
            } else if(GetDistance(p1,p2)>GetDistance(p1, p3))
            {
                this.Apex = p3;
            } else
            {
                this.Apex = p1;
                this.P1 = p3;
            }
            this.Basepoint = new Basepoint(this.P1);
            this.Optionpoint = new Optionpoint(this.P2);
        }

        public Arc(List<Point> points)
        {
            this.P1 = points[0];
            this.P2 = points[1];
            if (decimal.Equals(GetDistance(points[0], points[1]), GetDistance(points[1], points[2]))){
                this.Apex = points[1];
                this.P2 = points[2];
            }
            else if (GetDistance(points[0], points[1]) > GetDistance(points[0], points[2]))
            {
                this.Apex = points[2];
            }
            else
            {
                this.Apex = points[0];
                this.P1 = points[2];
            }
        }

        private decimal GetDistance(Point p1, Point p2)
        {
            return (decimal)Math.Sqrt(Math.Pow((double)p1.X - (double)p2.X, 2) + Math.Pow((double)p1.Y - (double)p2.Y, 2));
        }
    }
}