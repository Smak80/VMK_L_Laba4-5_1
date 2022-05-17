namespace VMK_L_Laba4_5_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _animator = new Animator(aPanel.CreateGraphics());
            _animator.Start();
        }

        private Animator _animator;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _animator.Stop();
        }

        private void aPanel_Resize(object sender, EventArgs e)
        {
            _animator.MainGraphics = aPanel.CreateGraphics();
        }

        private void aPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _animator.AddCircle();
            else
                _animator.ChangePausedState();
        }
    }
}