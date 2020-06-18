using System;
using System.Collections.Generic;
using System.Text;
using Com.Bing.Element;
using Com.Bing.Environment;
using Com.Bing.Role;
using System.Drawing;
using Com.Bing.Interface;
using Com.Bing.Common;

namespace Com.Bing.Manager
{
    public class GameManager : IMoveble
    {
        private Map map;
        private Snake snake;
        private CellCreator cellCreator;
        public GameManager()
        {
            map = new Map(20, 15);
            snake = new Snake(new Point(4, 0), new Point(3, 0), new Point(2, 0), new Point(1, 0), new Point(0, 0));
            cellCreator = new CellCreator(map, snake);

            Cell newCell = cellCreator.CreateCell();
            if (newCell is EmptyCell)
            {
                isGameOver = true;
                if (this.onGameOver != null) this.onGameOver();
                return;
            }
            else
            {
                map.AddCell(newCell);
            }
        }
        private bool innerIsGameOver = false;
        private bool isGameOver
        {
            get { return innerIsGameOver; }
            set
            {
                this.innerIsGameOver = value;
                if (value && this.onGameOver != null) this.onGameOver();
            }
        }
        public bool IsGameOver
        {
            get { return isGameOver; }
        }
        #region IMoveble ≥…‘±
        private void MoveToDirection(Direction direction)
        {
            if (isGameOver) return;
            Point offsetPoint = snake.GetDirectionOffset(direction);
            if (new Point(snake.HeadPos.X + offsetPoint.X, snake.HeadPos.Y + offsetPoint.Y) == snake.SecondPos) return;
            snake.CurDirection = direction;
            if (!IsNextInMap || IsNextInBody)
            {
                isGameOver = true;
                return;
            }
            Cell cell = map[snake.Target];
            if (cell != null)
            {
                if (cell is FoodCell)
                {
                    snake.Eat(cell);
                    map.RemoveCell(cell);
                    Cell newCell = cellCreator.CreateCell();
                    if (newCell is EmptyCell)
                    {
                        isGameOver = true;
                        return;
                    }
                    else
                    {
                        map.AddCell(newCell);
                    }
                }
                else if (cell is ObstacleCell)
                {
                    isGameOver = true;
                    return;
                }
            }
            else
            {
                snake.Move();
            }
        }
        public void Up()
        {
            MoveToDirection(Direction.Up);
        }
        public void Down()
        {
            MoveToDirection(Direction.Down);
        }
        public void Left()
        {
            MoveToDirection(Direction.Left);
        }
        public void Right()
        {
            MoveToDirection(Direction.Right);
        }
        #endregion
        public void MoveDefault()
        {
            MoveToDirection(snake.CurDirection);
        }
        private bool IsNextInMap
        {
            get
            {
                return IsInMap(snake.Target);
            }
        }
        private bool IsNextInBody
        {
            get 
            {
                //≤ªÀ„Œ≤∞Õ
                for (int i = 0; i < snake.Points.Count - 1; i++)
                {
                    if (snake.Target == snake.Points[i]) return true;
                }
                return false;
            }
        }
        private bool IsInMap(Point pos)
        {
            return pos.X >= 0 && pos.X < map.Size.Width && pos.Y >= 0 && pos.Y < map.Size.Height;
        }
        public override string ToString()
        {
            List<Point> snakePoints = snake.Points;
            StringBuilder sb = new StringBuilder();
            for (int y = map.Size.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < map.Size.Width; x++)
                {
                    sb.Append(map[x, y] != null || snakePoints.Contains(new Point(x, y)) ? "°ˆ" : "°ı");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public ICell[,] IMapCell
        {
            get
            {
                ICell[,] mapCell = this.map.MapCell;
                foreach (Cell cell in snake.snakeList)
                {
                    mapCell[cell.Pos.X, cell.Pos.Y] = cell;
                }
                return mapCell;
            }
        }
        private Func onGameOver;
        public event Func OnGameOver
        {
            add { this.onGameOver += value; }
            remove { this.onGameOver -= value; }
        }
    }
}
