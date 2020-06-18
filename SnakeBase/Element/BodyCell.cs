using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Com.Bing.Interface;

namespace Com.Bing.Element
{
    class BodyCell : Cell, IBodyCell
    {
        public BodyCell(Point pos)
        {
            this.Pos = pos;
        }
    }
}
