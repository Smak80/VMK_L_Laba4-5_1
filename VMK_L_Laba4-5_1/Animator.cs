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
        private List<Circle> _circ = new ();
        private BufferedGraphics buf_gr;
        private Thread? t;
        public Animator(Graphics g)
        {
            _mainG = g;
            buf_gr = BufferedGraphicsManager.Current.Allocate(_mainG, Rectangle.Ceiling(_mainG.VisibleClipBounds));
        }

        private void _Start()
        {
            /*_circ = new(_mainG.VisibleClipBounds.Size);
            _circ.X = 0;
            Monitor.Enter(buf_gr);
            buf_gr.Graphics.Clear(Color.White);
            Monitor.Exit(buf_gr);
            while (true)
            {
                Monitor.Enter(buf_gr);
                _circ.Paint(buf_gr.Graphics);
                buf_gr.Render(_mainG);
                Monitor.Exit(buf_gr);
                Thread.Sleep(30);
                lock (buf_gr)
                {
                    buf_gr.Graphics.Clear(Color.White);
                }
                if (!_circ.Move()) break;
            }*/
            while (true)
            {
                buf_gr.Graphics.Clear(Color.White);
                foreach (var circle in _circ) /// ОПАСНО!!!
                {
                    circle.Paint(buf_gr.Graphics);
                }
                buf_gr.Render(_mainG);
                Thread.Sleep(30);
            }
        }

        public void Start()
        {
            if (t != null && t.IsAlive)
            {
                Stop();
            }
            t = new Thread(_Start);
            t.Start();
        }

        public void AddCircle()
        {
            var c = new Circle(_mainG.VisibleClipBounds.Size);
            _circ.Add(c);
            c.Start();
        }
    }
}
