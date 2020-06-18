using System;
using System.Collections.Generic;
using System.Text;
using Com.Bing.Environment;
using Com.Bing.Role;
using Com.Bing.Element;
using System.Drawing;

namespace Com.Bing.Manager
{
    internal class CellCreator
    {
        private Map map;
        private Snake snake;
        internal CellCreator(Map map, Snake snake)
        {
            this.map = map;
            this.snake = snake;
        }
        internal Cell CreateCell()
        {
            Point pos = map.GetRandomEmptyPoint(snake);
            if (pos.X == -1 && pos.Y == -1)
            {
                return new EmptyCell();
            }
            return new FoodCell(pos);
        }
    }
}
