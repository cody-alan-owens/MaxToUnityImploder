using System;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class TextPoint : Point
    {
        public string Text;
        public TextPoint(float x, float y, float z, string text) : base(x, y, z)
        {
            Text = text;
        }     
    }
}
