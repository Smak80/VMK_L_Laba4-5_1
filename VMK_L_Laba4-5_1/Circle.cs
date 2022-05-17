namespace VMK_L_Laba4_5_1
{
    public class Circle
    {
        public Color Color { get; set; }
        public float Radius { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public SizeF ContainerSize { get; set; }
        private Thread? t;
        private static bool stop = false;
        
        // Поле и свойство для приостановки работы потока шариков
        private static bool pause = false;
        public static bool IsPaused => pause;
        
        // Чтобы шарики на паузе не удалились из массива "живых",
        // добавляем в свойство IsAlive значение паузы
        public bool IsAlive => pause || (t?.IsAlive ?? false);
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

        private bool Move()
        {
            if (X < ContainerSize.Width - Radius - _dx)
            {
                X += _dx;
                return true;
            }
            return false;
        }

        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                stop = false;
                t = new Thread(() =>
                {
                    // Добавляем проверку свойства паузы
                    while (!stop && !IsPaused && Move())
                    {
                        Thread.Sleep(30);
                    }
                });
                t.IsBackground = true;
                t.Start();
            }
        }

        public static void StopAllCircles()
        {
            stop = true;
        }

        // Метод, приостанавливающий все шарики
        public static void Pause()
        {
            pause = true;
        }
        // Метод, возобновляющий движение всех шариков
        public static void Resume()
        {
            pause = false;
        }
    }
}
