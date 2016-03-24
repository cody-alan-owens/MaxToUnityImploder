using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Arc
    {
        public Point Basepoint;
        public List<Point> Optionpoints;

        public Arc(List<Point> points)
        {
            this.Optionpoints = points;
        }

        public void SetBasepoint(ref Point point)
        {
            if (Basepoint != null)
            {
                throw new Exception("Base point already set! Cannot overwrite base point.");
            }
            Point refPoint = point;
            for(int i = 0; i < Optionpoints.Count-1; i++)
            {
                if (refPoint.Equals(Optionpoints))
                {
                    Basepoint = Optionpoints[i];
                    Optionpoints.RemoveAt(i);
                    break;
                }
            }
        }
    }
}