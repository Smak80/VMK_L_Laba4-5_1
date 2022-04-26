using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMK_L_Laba4_5_1
{
    public class Circle
    {
        public Color Color { get; set; }
        public float Radius { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public SizeF ContainerSize { get; set; }

        private Random _r = new Random();
        private float _dx = 2;
        public Circle(SizeF containerSize)
        {
            Color = Color.FromArgb(
                _r.Next(235),
                _r.Next(235),
                _r.Next(235)
            );
            Radius = _r.Next(30, 100);
            ContainerSize = containerSize;
        }

        public void Paint(Graphics g)
        {
            var b = new SolidBrush(Color);
            g.FillEllipse(
                b, 
                X, 
                Y, 
                Radius, 
                Radius
                );
        }

        public bool Move()
        {
            if (X < ContainerSize.Width - Radius - _dx)
            {
                X += _dx;
                return true;
            }

            return false;
        }
    }
}
