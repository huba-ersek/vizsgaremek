namespace AdminPanel
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            using (var ctx = new FitnessContext())
            {
                MessageBox.Show(string.Join("\n", ctx.Allergenes.Select(a => $"{a.Id},{a.Name}")));
            }
        }
    }
}
