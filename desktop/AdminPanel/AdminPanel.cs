using Microsoft.EntityFrameworkCore;

namespace AdminPanel
{
    public partial class AdminPanel : Form
    {
        ToolStripMenuItem? selectedItem = null;
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
                    tablesMenuItem.DropDownItems.Add(name, null, TableItem_Click!);
                }
            }
        }

        private void FillTableView(string? tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return;
            using (var ctx = new FitnessContext())
            {
                var set = ctx.GetSetForTable(tableName) as IQueryable;
                var list = set!.GetEnumerator().ToIEnumerable().Cast<object>().ToList();
                tableView.DataSource = list;
            }
        }

        private void TableItem_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem item)
                return;
            if (selectedItem != null)
                selectedItem.Checked = false;
            selectedItem = item;
            item.Checked = true;
            FillTableView(item.Text);
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
