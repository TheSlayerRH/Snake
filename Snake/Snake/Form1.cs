using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Snake snake;
        private Food food;

        private Brush snakeColor = Brushes.Aquamarine;
        private Size snakeCirclesSize = new Size(15, 15);
        private Brush foodColor = Brushes.Beige;
        private Size foodSize = new Size(15, 15);

        private Graphics gr;

        private bool started = false;
        private int speed = 5;
        private int score = 0;



        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (started)
            {
                if(timerMoveSnake.Enabled == true)
                    timerMoveSnake.Enabled = false;
                else
                    timerMoveSnake.Enabled = true;
            }
            else
            {
                //Create snake:
                gr = pictureBoxBoard.CreateGraphics();
                snake = new Snake(0, -1, speed, new Point(pictureBoxBoard.Width / 2, pictureBoxBoard.Height / 2), snakeColor, snakeCirclesSize);
                //snake.addCircle();
                //snake.addCircle();
                snake.draw(gr, pictureBoxBoard.BackColor);

                //Create food:
                addFood();

                //Start:
                started = true;
                timerMoveSnake.Enabled = true;
                
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (started)
            {
                Keys key = e.KeyCode;

                if (key == Keys.W)
                {
                    snake.directionY = -1;
                    snake.directionX = 0;
                }
                else if (key == Keys.D)
                {
                    snake.directionX = 1;
                    snake.directionY = 0;
                }
                else if (key == Keys.S)
                {
                    snake.directionY = 1;
                    snake.directionX = 0;
                }
                else if (key == Keys.A)
                {
                    snake.directionX = -1;
                    snake.directionY = 0;
                }
            }
        }

        private void timerMoveSnake_Tick(object sender, EventArgs e)
        {
            //Graphics gr = pictureBoxBoard.CreateGraphics();

            snake.move();
            snake.draw(gr, pictureBoxBoard.BackColor);
            if (food.isOverlapping(snake.head))
            {
                score += food.score;
                addFood();
                snake.addCircle();
            }
            food.draw(gr);
        }

        private void addFood()
        {
            Random rand = new Random();
            int x = rand.Next(pictureBoxBoard.Size.Width - foodSize.Width);
            int y = rand.Next(pictureBoxBoard.Size.Height - foodSize.Height);

            food = new Food(new Point(x, y), 10, foodColor, foodSize);
            food.draw(gr);
        }
    }
}
