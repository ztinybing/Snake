using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Com.Bing.Manager;
using Com.Bing.Interface;

namespace Com.Bing.UI
{
    public partial class MainFrm : Form
    {
        private GameManager gameManager;
        public MainFrm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            gameManager = new GameManager();
            gameManager.OnGameOver += new Func(gameManager_OnGameOver);
            this.Refresh();
        }

        void gameManager_OnGameOver()
        {
            MessageBox.Show("GameOver", "提示");
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    gameManager.Left();
                    ResetTimer();
                    this.Refresh();
                    break;
                case Keys.S:
                    gameManager.Down();
                    ResetTimer();
                    this.Refresh();
                    break;
                case Keys.W:
                    gameManager.Up();
                    ResetTimer();
                    this.Refresh();
                    break;
                case Keys.D:
                    gameManager.Right();
                    ResetTimer();
                    this.Refresh();
                    break;
            }
        }
        /// <summary>
        /// 重置时间，防止按方向前进与时间造成的前进重合。
        /// </summary>
        private void ResetTimer()
        {
            timer1.Stop();
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            gameManager.MoveDefault();
            this.Refresh();
        }

        private void MainFrm_Paint(object sender, PaintEventArgs e)
        {
            gameManager.ToString();
            int x = gameManager.IMapCell.GetLength(0);
            int y = gameManager.IMapCell.GetLength(1);
            int xUnit = this.Width / x;
            int yUnit = (this.Height - 40) / y;
            int minUnit = Math.Min(xUnit, yUnit);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, x * minUnit, y * minUnit));
            for (int i = 0; i < x; i++)
            {
                for (int t = 0; t < y; t++)
                {
                    if (gameManager.IMapCell[i, t] == null || gameManager.IMapCell[i, t] is IEmptyCell)
                    {
                    }
                    else if (gameManager.IMapCell[i, t] is IFoodCell)
                    {
                        e.Graphics.FillRectangle(Brushes.YellowGreen, new Rectangle(i * minUnit, (y - 1 - t) * minUnit, minUnit, minUnit));
                    }
                    else if (gameManager.IMapCell[i, t] is IBodyCell)
                    {
                        e.Graphics.FillEllipse(Brushes.Green, new RectangleF(i * minUnit, (y - 1 - t) * minUnit, minUnit, minUnit));
                    }
                }
            }
        }
    }
}