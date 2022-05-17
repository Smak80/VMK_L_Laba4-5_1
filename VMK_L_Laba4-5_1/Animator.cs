namespace VMK_L_Laba4_5_1
{
    public class Animator
    {
        private object commonGraphics = new ();
        private object commonCircles = new ();
        private Graphics _mainG;
        private List<Circle> _circ = new ();
        private BufferedGraphics buf_gr;
        private Thread? t;
        private bool stop = false;
        private SizeF _containerSize;
        private SizeF ContainerSize
        {
            get => _containerSize;
            set
            {
                _containerSize = value;
                _circ.ForEach(c => { c.ContainerSize = value; });
            }
        }

        public Graphics MainGraphics
        {
            set
            {
                lock (commonGraphics)
                {
                    _mainG = value;
                    ContainerSize = _mainG.VisibleClipBounds.Size;
                    buf_gr = BufferedGraphicsManager.Current.Allocate(_mainG,
                        Rectangle.Ceiling(_mainG.VisibleClipBounds));
                }
            }
        }

        public Animator(Graphics g)
        {
            MainGraphics = g;
        }

        //Постановка шариков на паузу и снятие с паузы
        public void ChangePausedState()
        {
            if (Circle.IsPaused)
            {
                lock (commonCircles)
                {
                    Circle.Resume();
                    _circ.ForEach(c => { c.Start(); });
                }
            }
            else Circle.Pause();
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
            while (!stop)
            {
                lock (commonCircles)
                {
                    _circ = _circ.FindAll(c => c.IsAlive);
                }
                var c = new List<Circle>(_circ);
                lock (commonGraphics)
                {
                    buf_gr.Graphics.Clear(Color.White);
                    foreach (var circle in c)
                    {
                        circle.Paint(buf_gr.Graphics);
                    }
                    buf_gr.Render(_mainG);
                }
                Thread.Sleep(30);
            }
        }

        public void Start()
        {
            if (t != null && t.IsAlive)
            {
                Stop();
                t.Join();
            }
            stop = false;
            t = new Thread(_Start);
            t.Start();
        }

        public void Stop()
        {
            stop = true;
            //Circle.StopAllCircles();
        }

        public void AddCircle()
        {
            var c = new Circle(ContainerSize);
            c.Start();
            _circ.Add(c);
        }
    }
}
