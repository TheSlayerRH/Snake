using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        public int score { get; }
        public Brush color;

        private Point location;
        private Size size;

        public Food(Point location, int score, Brush color, Size size)
        {
            this.location = location;
            this.score = score;
            this.color = color;
            this.size = size;
        }

        public bool isOverlapping(Circle snakeHead)
        {
            Point location1 = snakeHead.location;
            Point[] locations = { location1, new Point(location1.X + snakeHead.size.Width, location1.Y), 
                new Point(location1.X, location1.Y + snakeHead.size.Height), new Point(location1.X + snakeHead.size.Width, location1.Y + snakeHead.size.Height) };

            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i].X > location.X && locations[i].X <= location.X + size.Width &&
                    locations[i].Y > location.Y && locations[i].Y <= location.Y + size.Height)
                    return true;
            }

            return false;
        }

        public void draw(Graphics gr)
        {
            gr.FillEllipse(color, location.X, location.Y, size.Width, size.Height);
        }
    }
}
