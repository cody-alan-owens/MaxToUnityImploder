﻿using System;
namespace PlanImploder
{
    public class Optionpoint
    {
        public Point Point { get; private set; }
        public Zone Zone;
        public Optionpoint(Point point)
        {
            this.Point = point;
        }
    }
}