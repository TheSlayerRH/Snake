using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Snake
    {
        private List<Circle> snake;

        public int directionX { get; set; } //1 = East; -1 = west; 0 = nothing;
        public int directionY { get; set; } //-1 = North; 1 = South; 0 = nothing;
        public Circle head { get; }

        private Size circleSize;
        private Brush color;
        private int speed;

        public Snake(int directionX, int directionY, int speed, Point startPoint, Brush color, Size circleSize) {
            snake = new List<Circle>();

            this.directionX = directionX;
            this.directionY = directionY;
            this.speed = speed;
            this.color = color;
            this.circleSize = circleSize;
            Circle firstCircle = new Circle(startPoint, circleSize);
            head = firstCircle;
            snake.Add(firstCircle);
        }

        public void addCircle()
        {
            Circle circle = new Circle(snake.ElementAt(snake.Count - 1).location, circleSize);
            snake.Add(circle);
        }

        public void move()
        {
            Point beforeLastCircleLocationBeforeMove = new Point(-1, -1);
            if (snake.Count >= 2)
                beforeLastCircleLocationBeforeMove = snake.ElementAt(snake.Count - 2).location;

            for (int i = snake.Count - 2; i > 0 ; i--)
            {
                Circle currentCircle = snake.ElementAt(i);
                Circle previousCircle = snake.ElementAt(i - 1);
                currentCircle.location = new Point(previousCircle.location.X, previousCircle.location.Y);
            }
            //First circle:
            Circle firstCircle = snake.ElementAt(0);
            firstCircle.location = new Point(firstCircle.location.X + speed * directionX, firstCircle.location.Y + speed * directionY);

            //Last circle:
            if (snake.Count >= 2)
            {
                Circle lastCircle = snake.ElementAt(snake.Count - 1);
                Circle beforeLastCircle = snake.ElementAt(snake.Count - 2);

                if ((Math.Abs(beforeLastCircle.location.X - lastCircle.location.X) <= circleSize.Width && beforeLastCircle.location.Y != lastCircle.location.Y/* && beforeLastCircle.location.X != lastCircle.location.X*/) ||
                    (Math.Abs(beforeLastCircle.location.Y - lastCircle.location.Y) <= circleSize.Height && beforeLastCircle.location.X != lastCircle.location.X/* && beforeLastCircle.location.Y != lastCircle.location.Y*/))
                {
                    lastCircle.location = new Point(beforeLastCircleLocationBeforeMove.X, beforeLastCircleLocationBeforeMove.Y);
                }
                else
                {
                    if (beforeLastCircle.location.X < lastCircle.location.X - circleSize.Width)
                    {
                        lastCircle.location = new Point(beforeLastCircle.location.X + circleSize.Width, beforeLastCircle.location.Y);
                    }
                    else if (beforeLastCircle.location.X > lastCircle.location.X + circleSize.Width)
                    {
                        lastCircle.location = new Point(beforeLastCircle.location.X - circleSize.Width, beforeLastCircle.location.Y);
                    }
                    else if (beforeLastCircle.location.Y < lastCircle.location.Y - circleSize.Height)
                    {
                        lastCircle.location = new Point(beforeLastCircle.location.X, beforeLastCircle.location.Y + circleSize.Height);
                    }
                    else if (beforeLastCircle.location.Y > lastCircle.location.Y + circleSize.Height)
                    {
                        lastCircle.location = new Point(beforeLastCircle.location.X, beforeLastCircle.location.Y - circleSize.Height);
                    }
                }
            }
        }

        public void draw(Graphics gr, Color backgroundColor)
        {
            gr.Clear(backgroundColor);
            for (int i = 0; i < snake.Count; i++) 
            {
                snake.ElementAt(i).draw(gr, color);
            }
        }
    }
}
