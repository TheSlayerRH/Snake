using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Circle
    {
        public Point location { get; set; }
        public Size size { get; }

        public Circle(Point location, Size size)
        {
            this.location = location;
            this.size = size;
        }

        public void draw(Graphics gr, Brush color)
        {
            gr.FillEllipse(color, location.X, location.Y, size.Width, size.Height);
        }
    }
}
