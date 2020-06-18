using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Com.Bing.Element;
using Com.Bing.Role;
using Com.Bing.Interface;

namespace Com.Bing.Environment
{
    public class Map
    {
        private Size size;
        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        private Cell[,] cells;
        public Map(Size size)
            : this(size.Width, size.Height)
        {
        }
        public Map(int width, int height)
        {
            this.size = new Size(width, height);
            cells = new Cell[width, height];
        }
        public Cell this[int x, int y]
        {
            get
            {
                return cells[x, y];
            }
        }
        public Cell this[Point p]
        {
            get
            {
                return this[p.X, p.Y];
            }
        }
        internal void AddCell(Cell cell)
        {
            cells[cell.Pos.X, cell.Pos.Y] = cell;
        }
        internal void RemoveCell(Cell cell)
        {
            cells[cell.Pos.X, cell.Pos.Y] = null;
        }
        internal Point GetRandomEmptyPoint(Snake snake)
        {
            List<Point> emptyPoints = new List<Point>();
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    Cell cell = cells[x, y];
                    if (cell == null)
                    {
                        emptyPoints.Add(new Point(x, y));
                    }
                }
            }
            foreach (Point pos in snake.Points)
            {
                emptyPoints.Remove(pos);
            }
            if (emptyPoints.Count == 0) return new Point(-1, -1);
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            return emptyPoints[rnd.Next(0, emptyPoints.Count)];
        }
        public ICell[,] MapCell
        {
            get
            {
                ICell[,] mapCell = new ICell[size.Width, size.Height];
                for (int y = this.Size.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < this.Size.Width; x++)
                    {
                        mapCell[x, y] = this[x, y];
                    }
                }
                return mapCell;
            }
        }

    }
}
