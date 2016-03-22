using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace PlanImploder {
    public class ZoneBuilder {
        private PointsInputFormat InputFormat;
        public enum PointsInputFormat {MAX}
        private string PointsText;
        private string BasepointsText;
        private string OptionpointsText;
        public ZoneBuilder(string pointsText, string basepointsText, string optionpointsText, PointsInputFormat format)
        {
            this.PointsText = pointsText;
            this.BasepointsText = basepointsText;
            this.OptionpointsText = optionpointsText;
            this.InputFormat = format;
        }
        public List<Zone> GetZones()
        {
            List<Zone> zones = new List<Zone>();
            List<Rectangle> rectangles = new List<Rectangle>();
            List<Arc> arcs = new List<Arc>();
            List<Basepoint> basepoints = new List<Basepoint>();
            List<Optionpoint> optionpoints = new List<Optionpoint>();
            if (this.InputFormat == PointsInputFormat.MAX)
            {
                Dictionary<int, List<Point>> splineAssignments = new Dictionary<int, List<Point>>();
                foreach (string item in this.PointsText.Split('\n'))
                {
                    string[] pointStrArr = item.Split(',');
                    splineAssignments[int.Parse(pointStrArr[1])].Add(new Point(decimal.Parse(pointStrArr[2]), decimal.Parse(pointStrArr[3]), decimal.Parse(pointStrArr[4])));
                }
                foreach (string item in this.BasepointsText.Split('\n'))
                {
                    string[] pointStrArr = item.Split(',');
                    basepoints.Add(new Basepoint(new Point(decimal.Parse(pointStrArr[2]), decimal.Parse(pointStrArr[3]), decimal.Parse(pointStrArr[4]))));
                }
                foreach (string item in this.OptionpointsText.Split('\n'))
                {
                    string[] pointStrArr = item.Split(',');
                    optionpoints.Add(new Optionpoint(new Point(decimal.Parse(pointStrArr[2]), decimal.Parse(pointStrArr[3]), decimal.Parse(pointStrArr[4]))));
                }
                foreach (KeyValuePair<int, List<Point>> assignment in splineAssignments)
                {
                    if (assignment.Value.Count == 4)
                    {
                        rectangles.Add(new Rectangle(assignment.Value));
                    } else if (assignment.Value.Count == 3)
                    {
                        arcs.Add(new Arc(assignment.Value));
                    }
                }
                rectangles = rectangles.OrderBy(o => o.Area).ToList();
                foreach(Rectangle rectangle in rectangles)
                {
                    zones.Add(new Zone(rectangle));
                }
                for(int i = rectangles.Count; i>0; i--)
                {
                    zones.Add(new Zone(rectangles[i]));
                    for (int j = i - 1; i > 0; i--)
                    {
                        if(rectangles[j].UpperLeft.X>rectangles[i].UpperLeft.X 
                            && rectangles[j].UpperLeft.Y < rectangles[i].UpperLeft.Y
                            && rectangles[j].LowerLeft.X > rectangles[i].LowerLeft.X
                            && rectangles[j].LowerLeft.Y > rectangles[i].LowerLeft.Y
                            && rectangles[j].UpperRight.X < rectangles[i].UpperRight.X
                            && rectangles[j].UpperRight.Y < rectangles[i].UpperRight.Y
                            && rectangles[j].LowerRight.X < rectangles[i].LowerRight.X
                            && rectangles[j].LowerRight.Y > rectangles[i].LowerRight.Y)
                        {
                            zones[j].Parent=zones[i];
                        }
                    }
                }
            }
            foreach(Arc arc in arcs)
            {
                foreach(Basepoint basepoint in basepoints)
                {
                    if (decimal.Equals(basepoint.Point.X,arc.Basepoint.Point.X) && decimal.Equals(basepoint.Point.Y, arc.Basepoint.Point.Y))
                    {
                        Basepoint tempBP = new Basepoint(arc.Basepoint.Point);
                        arc.Basepoint = new Basepoint(arc.Optionpoint.Point);
                        arc.Optionpoint = new Optionpoint(tempBP.Point);                        
                    }
                }
                //Now that basepoint and optionpoint are set and zones have correct hierarchy, start assigning arcs
                //find smallest surrounding zone rectangle for each point
                foreach(Zone zone in zones)
                {
                    if()
                }
            }

            return zones;
        }

        private bool IsPointInsideRectangle(Point point, Rectangle rectangle)
        {
            if(point.X > rectangle.UpperLeft.Y 
                && point.Y < rectangle.UpperLeft.Y
                && point.X < rectangle.LowerRight.X
                && point.X < rectangle.UpperLeft.X)
            {
                return true;
            }
            return false;
        }
    }
}