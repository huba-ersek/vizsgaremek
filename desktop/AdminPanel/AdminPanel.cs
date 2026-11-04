using Microsoft.EntityFrameworkCore;

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
                foreach (var name in ctx.Model.GetEntityTypes()
                    .Select(t => t.GetTableName()))
                {
                    tablesMenuItem.DropDownItems.Add(name);
                }
            }
        }
    }
}
