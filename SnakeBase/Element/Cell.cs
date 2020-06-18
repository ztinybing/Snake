using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Com.Bing.Interface;

namespace Com.Bing.Element
{
    public abstract class Cell : ICell
    {
        private Point pos;
        public Point Pos
        {
            get { return pos; }
            set { pos = value; }
        }
    }
}
