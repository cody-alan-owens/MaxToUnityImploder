using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace PlanImploder {
    public class ZoneBuilder {
        private PointsInputFormat InputFormat;
        public enum PointsInputFormat {MAX}
        public ZoneBuilder(PointsInputFormat format)
        {
            this.InputFormat = format;           
        }
        public List<Zone> GetZones(string pointsText, string basepointsText, string optionpointsText)
        {
            List<Zone> zones = new List<Zone>();
            List<Rectangle> rectangles = new List<Rectangle>();
            List<Arc> arcs = new List<Arc>();
            List<Basepoint> basepoints = new List<Basepoint>();
            List<Optionpoint> optionpoints = new List<Optionpoint>();
            if (this.InputFormat == PointsInputFormat.MAX)
            {
                Dictionary<int, SplineAssignment> splineAssignments = new Dictionary<int, SplineAssignment>();
                foreach (string item in pointsText.Split('\n'))
                {
                    string[] pointStrArr = item.Split(',');
                    if (pointStrArr.Length == 5)
                    {
                        if (!splineAssignments.ContainsKey(int.Parse(pointStrArr[1])))
                        {
                            SplineAssignment.SplineType tempType;
                            if (pointStrArr[0] == "closed")
                            {
                                tempType = SplineAssignment.SplineType.RECTANGLE;
                            } else
                            {
                                tempType = SplineAssignment.SplineType.ARC;
                            }
                            splineAssignments[int.Parse(pointStrArr[1])] = new SplineAssignment(new List<Point>(), tempType);
                        }
                        splineAssignments[int.Parse(pointStrArr[1])].Points.Add(new Point(float.Parse(pointStrArr[2]), float.Parse(pointStrArr[3]), float.Parse(pointStrArr[4])));
                    }
                }
                /*REPLACE WITH GAMEOBJECTS
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
                */
                foreach (KeyValuePair<int, SplineAssignment> assignment in splineAssignments)
                {
                    if (assignment.Value.Type == SplineAssignment.SplineType.RECTANGLE)
                    {
                        rectangles.Add(new Rectangle(assignment.Value.Points));
                    } else if (assignment.Value.Type == SplineAssignment.SplineType.ARC)
                    {
                        arcs.Add(new Arc(assignment.Value.Points));
                    }
                }
                rectangles = rectangles.OrderBy(o => o.Area).ToList();
                foreach(Rectangle rectangle in rectangles)
                {
                    zones.Add(new Zone(rectangle));
                }
                for(int i = rectangles.Count-1; i>0; i--)
                {
                    zones.Add(new Zone(rectangles[i]));
                    for (int j = i - 1; i > 0; i--)
                    {
                        if (IsPointInsideRectangle(rectangles[j].UpperLeft, rectangles[i])){
                            if (IsPointInsideRectangle(rectangles[j].LowerRight, rectangles[i]))
                            {
                                zones[j].Parent = zones[i];
                            }
                        }
                    }
                }
            }
            foreach (string item in basepointsText.Split('\r'))
            {
                string[] pointStrArr = item.Split(',');
                if (pointStrArr.Length == 5)
                {
                    basepoints.Add(new Basepoint(new Point(float.Parse(pointStrArr[2]), float.Parse(pointStrArr[3]), float.Parse(pointStrArr[4]))));
                }                
            }
            foreach (string item in optionpointsText.Split('\r'))
            {
                string[] pointStrArr = item.Split(',');
                if (pointStrArr.Length == 5)
                {
                    optionpoints.Add(new Optionpoint(new Point(float.Parse(pointStrArr[2]), float.Parse(pointStrArr[3]), float.Parse(pointStrArr[4]))));
                }                
            }
            foreach (Arc arc in arcs)
            {
                foreach (Point point in arc.Points)
                {
                    foreach (Basepoint basepoint in basepoints)
                    {
                        if (float.Equals(point.X, basepoint.Point.X) && float.Equals(point.Y, basepoint.Point.Y))
                        {
                            arc.Basepoint = basepoint;
                            break;
                        }
                    }
                }
                foreach (Point point in arc.Points)
                {
                    foreach (Optionpoint optionpoint in optionpoints)
                    {
                        if (float.Equals(point.X, optionpoint.Point.X) && float.Equals(point.Y, optionpoint.Point.Y))
                        {
                            arc.Optionpoints.Add(optionpoint);
                        }
                    }
                }
                if (arc.Basepoint != null)
                {
                    arc.BaseZone = GetSmallestPointZone(arc.Basepoint.Point, ref zones);
                    foreach (Optionpoint optionpoint in arc.Optionpoints)
                    {
                        optionpoint.Zone = GetSmallestPointZone(optionpoint.Point, ref zones);
                    }
                }
                foreach(Zone zone in zones)
                {
                    if (zone.Parent != null)
                    {
                        zone.Parent.Children.Add(zone);
                    }
                }
            }
            return zones;
        }

        private bool IsPointInsideRectangle(Point point, Rectangle rectangle)
        {
            if (point.X > rectangle.UpperLeft.X)
            {
                if (point.Y < rectangle.UpperLeft.Y)
                {
                    if (point.X < rectangle.LowerRight.X)
                    {
                        if(point.Y > rectangle.LowerRight.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private Zone GetSmallestPointZone(Point point, ref List<Zone> zones)
        {
            for(int i = zones.Count - 1; i > 0; i--)
            {
                if (IsPointInsideRectangle(point, zones[i].Rectangle))
                {
                    if (zones[i].Children != null && zones[i].Children.Count > 0)
                    {
                        return GetSmallestPointZone(point, ref zones[i].Children);
                    }
                    return zones[i];
                }
            }
            return null;
        }

        private class SplineAssignment
        {
            public List<Point> Points;
            public enum SplineType { RECTANGLE, ARC };
            public SplineType Type;
            public SplineAssignment(List<Point> points, SplineType type)
            {
                this.Points = points;
                this.Type = type;
            }
        }
    }
}