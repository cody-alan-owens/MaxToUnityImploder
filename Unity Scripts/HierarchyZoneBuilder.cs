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
        public List<HierarchyZone> GetZones(string pointsText, string basepointsText, string optionpointsText, string textpointsText)
        {
            List<HierarchyZone> zones = new List<HierarchyZone>();
            List<Assignment> assignments = new List<Assignment>();
            List<Point> basepoints = new List<Point>();
            List<Point> optionpoints = new List<Point>();
            Dictionary<int, Spline> splineAssignments = new Dictionary<int, Spline>();
            List<TextPoint> textPoints = new List<TextPoint>();
            zones.Add(new HierarchyZone());
            if (this.InputFormat == PointsInputFormat.MAX)
            {
                basepoints = DeserializeMAXPoints(basepointsText);
                optionpoints = DeserializeMAXPoints(optionpointsText);
                splineAssignments = DeserializeMAXSplinePoints(pointsText);
                textPoints = DeserializeAutoCADTextPoints(textpointsText);
            }

            foreach (KeyValuePair<int, Spline> assignment in splineAssignments)
            {
                if (assignment.Value.Type == Spline.SplineType.RECTANGLE)
                {
                    zones.Add(new HierarchyZone(assignment.Value.Points));
                }
                else if (assignment.Value.Type == Spline.SplineType.ARC)
                {
                    assignments.Add(new Assignment(assignment.Value.Points));
                }
            }
            
            for(int i = 0; i<zones.Count;i++)
            {
                zones[i].Parent = GetSmallestPointZone(zones[i].UpperLeft, ref zones);
                try {
                    zones[i].Label = GetNearestTextPoint(zones[i].LowerLeft, ref textPoints).Text;
                } catch (NullReferenceException e)
                {
                    
                }
            }
                
            foreach (Assignment assignment in assignments)
            {
                foreach(Point p in assignment.Optionpoints)
                {
                    Point refPoint = GetMatchingPoint(p, ref basepoints);
                    if(refPoint != null)
                    {
                        assignment.SetBasepoint(ref refPoint);
                        break;
                    }                        
                }
                    
                foreach (Point optionpoint in assignment.Optionpoints)
                {
                    HierarchyZone smallestZone = GetSmallestPointZone(optionpoint, ref zones);
                    assignment.Children.Add(smallestZone);
                }
            }
            for(int i = 0; i < zones.Count; i++)
            {
                if (zones[i].Parent != null)
                {
                    zones[i].Parent.Children.Add(zones[i]);
                }                
            }
            return zones;
        }

        public bool IsPointInsideRectangle(Point point, Rectangle rectangle)
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

        public HierarchyZone GetSmallestPointZone(Point point, ref List<HierarchyZone> zones)
        {
            zones = zones.OrderBy(o => o.Area).ToList();
            for (int i = 0; i < zones.Count; i++)
            {
                if (IsPointInsideRectangle(point, (Rectangle)zones[i]))
                {
                    return zones[i];
                }
            }
            return null;
        }        

        public Dictionary<int, Spline> DeserializeMAXSplinePoints(string pointsText)
        {
            Dictionary<int, Spline> splines = new Dictionary<int, Spline>();
            foreach (string item in pointsText.Split('\n'))
            {
                string[] pointStrArr = item.Split(',');
                if (pointStrArr.Length == 5)
                {
                    if (!splines.ContainsKey(int.Parse(pointStrArr[1])))
                    {
                        Spline.SplineType tempType;
                        if (pointStrArr[0] == "closed")
                        {
                            tempType = Spline.SplineType.RECTANGLE;
                        }
                        else
                        {
                            tempType = Spline.SplineType.ARC;
                        }
                        splines[int.Parse(pointStrArr[1])] = new Spline(new List<Point>(), tempType);
                    }
                    splines[int.Parse(pointStrArr[1])].Points.Add(new Point(float.Parse(pointStrArr[2].Substring(0,pointStrArr[2].Length-3)), float.Parse(pointStrArr[3].Substring(0, pointStrArr[3].Length - 3)), float.Parse(pointStrArr[4].Substring(0, pointStrArr[4].Length - 3))));
                }
            }
            return splines;
        }
        public List<Point> DeserializeMAXPoints (string pointsText)
        {
            /*
            Expected format example:
            open,1,-914.4cm,792.48cm,0.0cm
            open,1,304.8cm,792.48cm,0.0cm
            */
            List<Point> points = new List<Point>();
            foreach (string item in pointsText.Split('\r'))
            {
                string[] pointStrArr = item.Split(',');
                if (pointStrArr.Length == 5)
                {
                    string x = pointStrArr[2].Substring(0, pointStrArr[2].Length - 3);
                    string y = pointStrArr[3].Substring(0, pointStrArr[3].Length - 3);
                    string z = pointStrArr[4].Substring(0, pointStrArr[4].Length - 3);                
                    points.Add(new Point(float.Parse(x), float.Parse(y), float.Parse(z)));
                }
            }
            return points;        
        }
        public List<TextPoint> DeserializeAutoCADTextPoints (string autocadTextPoints)
        {
            /*
            Expected format example:
            -3992.8800,-190.5000,0.0000,{\W1;TUB IN LIEU\POF SHOWER @\PMASTER BATH}
            -3688.0800,-190.5000,0.0000,{\W1;NOT\PTUB IN LIEU\POF SHOWER @\PMASTER BATH}
            */
            List<TextPoint> points = new List<TextPoint>();
            foreach (string item in autocadTextPoints.Split('\r'))
            {
                string[] pointStrArr = item.Split(',');
                if (pointStrArr.Length == 4)
                {
                    try {
                        string x = pointStrArr[0].Substring(0, pointStrArr[0].Length - 3);
                        string y = pointStrArr[1].Substring(0, pointStrArr[1].Length - 3);
                        string z = pointStrArr[2].Substring(0, pointStrArr[2].Length - 3);
                        string text = pointStrArr[3];
                        points.Add(new TextPoint(float.Parse(x), float.Parse(y), float.Parse(z), text));
                    } catch(FormatException e)
                    {

                    }
                }
            }
            return points;
        }
        public Point GetMatchingPoint(Point p, ref List<Point> points)
        {
            List<Point> refPoints = points;
            foreach (Point point in refPoints)
            {
                if (Equals(point.X, p.X) && Equals(point.Y, p.Y))
                {
                    return point;
                }
            }
            return null;
        }

        public TextPoint GetNearestTextPoint(Point p, ref List<TextPoint> points)
        {
            List<TextPoint> refPoints = points;
            Dictionary<TextPoint,float> textpointDistances = new Dictionary<TextPoint,float>();
            foreach(TextPoint point in points)
            {
                textpointDistances.Add(point, GetDistance(point.X, p.X, point.Y, p.Y));
            }
            return textpointDistances.OrderBy(x => x.Value).First().Key;
        }

        public float GetDistance(float x2, float x1, float y2, float y1)
        {
            return (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}