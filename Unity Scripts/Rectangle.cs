using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace PlanImploder
{
    public class Rectangle {
        public Point UpperLeft { get; private set; }
        public Point UpperRight { get; private set; }
        public Point LowerLeft { get; private set; }
        public Point LowerRight { get; private set; }
        public float Area;
        public Rectangle(List<Point> points)
        {
            List<Point> pointsOrderedByX = points.OrderBy(o => o.X).ToList();
            if(pointsOrderedByX[0].Y<pointsOrderedByX[1].Y)
            {
                this.UpperLeft = pointsOrderedByX[1];
                this.LowerLeft = pointsOrderedByX[0];
            } else
            {
                this.UpperLeft = pointsOrderedByX[0];
                this.LowerLeft = pointsOrderedByX[1];
            }
            if (pointsOrderedByX[2].Y > pointsOrderedByX[3].Y)
            {
                this.UpperRight = pointsOrderedByX[2];
                this.LowerRight = pointsOrderedByX[3];
            }
            else
            {
                this.UpperRight = pointsOrderedByX[3];
                this.LowerRight = pointsOrderedByX[2];
            }
            this.Area = SetArea();
        }
        private float SetArea()
        {
            return Math.Abs((this.UpperRight.X - this.UpperLeft.X) * (this.UpperRight.Y - this.LowerRight.Y));
        }
        public List<Point> GetPoints()
        {
            return new List<Point> { this.UpperLeft, this.UpperRight, this.LowerLeft, this.LowerRight };
        }

        public float GetLength()
        {
            return this.UpperRight.X - this.UpperLeft.X;
        }
        public float GetWidth()
        {
            return this.UpperLeft.Y - this.LowerLeft.Y;
        }
    }
}