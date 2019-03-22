using System;
using System.Collections.Generic;
using System.Text;

namespace NestProbe
{
    public class Line
    {
        public string Text
        {
            get;
            private set;
        }

        public int LineNum
        {
            get;
            private set;
        }

        public Line(int lineNum, string text)
        {
            Text = text;
            LineNum = lineNum;
        }
    }
}
