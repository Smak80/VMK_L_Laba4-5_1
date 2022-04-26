namespace VMK_L_Laba4_5_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _animator = new Animator(aPanel.CreateGraphics());
        }

        private Animator _animator;
        private void aPanel_Click(object sender, EventArgs e)
        {
            _animator.Start();
        }
    }
}