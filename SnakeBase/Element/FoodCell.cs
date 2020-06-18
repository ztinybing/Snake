using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Com.Bing.Interface;

namespace Com.Bing.Element
{
    internal class FoodCell : Cell, IFoodCell
    {
        internal FoodCell(Point pos)
        {
            this.Pos = pos;
        }
    }
}
