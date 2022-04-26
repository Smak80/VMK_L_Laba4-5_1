using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMK_L_Laba4_5_1
{
    public class Animator
    {
        private Graphics _mainG;
        private Circle _circ;
        public Animator(Graphics g)
        {
            _mainG = g;
            _circ = new (g.VisibleClipBounds.Size);
        }

        public void Start()
        {
            _circ.X = 0;
            var buf_gr = BufferedGraphicsManager.Current.Allocate(_mainG, Rectangle.Ceiling(_mainG.VisibleClipBounds));
            buf_gr.Graphics.Clear(Color.White);
            while (true)
            {
                _circ.Paint(buf_gr.Graphics);
                buf_gr.Render(_mainG);
                Thread.Sleep(30);
                buf_gr.Graphics.Clear(Color.White);
                if (!_circ.Move()) break;
            }
            buf_gr.Dispose();
        }
    }
}
