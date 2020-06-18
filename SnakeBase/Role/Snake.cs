using System;
using System.Collections.Generic;
using System.Text;
using Com.Bing.Common;
using Com.Bing.Element;
using Com.Bing.Interface;
using System.Drawing;

namespace Com.Bing.Role
{
    internal class Snake
    {
        #region field
        private int twineLevel = 0;
        public int TwineLevel
        {
            get { return twineLevel; }
            set { twineLevel = value; }
        }
        private int life = 3;
        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        private int attack = 1;
        public int Attck
        {
            get { return attack; }
            set { attack = value; }
        }
        #endregion
        public LinkedList<Cell> snakeList = new LinkedList<Cell>();
        internal List<Point> Points
        {
            get
            {
                List<Point> points=new List<Point>();
                foreach (Cell cell in snakeList)
                {
                    points.Add(cell.Pos);
                }
                return points;
            }
        }
        /// <summary>
        /// the snake move direction;
        /// </summary>
        private Direction curDirection = Direction.Right;
        internal Direction CurDirection
        {
            get { return curDirection; }
            set { curDirection = value; }
        }
        internal Snake(IEnumerable<Point> bodyPoints)
        {
            foreach (Point pos in bodyPoints)
            {
                snakeList.AddLast(new BodyCell(pos));
            }
        }
        internal Snake(params Point[] pos)
            : this(pos as IEnumerable<Point>)
        {
        }
        #region offsetPosition
        private static readonly Point LeftPoint = new Point(-1, 0);
        private static readonly Point UpPoint = new Point(0, 1);
        private static readonly Point RightPoint = new Point(1, 0);
        private static readonly Point DownPoint = new Point(0, -1);
        internal Point GetDirectionOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return UpPoint;
                case Direction.Down:
                    return DownPoint;
                case Direction.Left:
                    return LeftPoint;
                case Direction.Right:
                    return RightPoint;
            }
            throw new ArgumentException("error Direction!");
        }
        #endregion
        /// <summary>
        /// the snake next move target positon  
        /// </summary>
        internal Point Target
        {
            get
            {
                Point moveOffset = GetDirectionOffset(curDirection);
                return new Point(HeadPos.X + moveOffset.X, HeadPos.Y + moveOffset.Y);
            }
        }
        /// <summary>
        /// the snake's head position
        /// </summary>
        internal Point HeadPos
        {
            get
            {
                return snakeList.First.Value.Pos;
            }
        }
        internal Point SecondPos
        {
            get
            {
                return snakeList.First.Next.Value.Pos;
            }
        }
        internal void Eat(Cell cell)
        {
            snakeList.AddFirst(new BodyCell(cell.Pos));
        }

        internal void Move()
        {
            Point targetPoint = Target;
            Point tempPoint;
            foreach (Cell cell in snakeList)
            {
                tempPoint = cell.Pos;
                cell.Pos = targetPoint;
                targetPoint = tempPoint;
            }
        }
        /// <summary>
        /// ฝ๘ปฏ
        /// </summary>
        internal void Evolve()
        {

        }

    }
}
