using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

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
            using var ctx = new FitnessContext();
            var databaseCreator = ctx.GetService<IRelationalDatabaseCreator>();
            if (!databaseCreator.Exists() || !databaseCreator.HasTables())
            {
                MessageBox.Show(
                    "Az adatbázis még nem létezik, vagy üres! A migrációt az artisan végzi!\n\n" +
                    "Az OK gombra kattintva a program kilép!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(-1);
            }
            foreach (var name in ctx.Model.GetEntityTypes()
                .Select(t => t.GetTableName()))
            {
                tablesMenuItem.DropDownItems.Add(name, null, TableItem_Click!);
            }
        }

        private void FillTableView(string? tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return;
            using var ctx = new FitnessContext();
            // TODO: find a more dynamic way
            tableView.DataSource = tableName switch
            {
                DatabaseConstants.USERS_TABLE => ctx.Users.ToList(),
                DatabaseConstants.ALLERGENES_TABLE => ctx.Allergenes.ToList(),
                DatabaseConstants.USER_ALLERGIES_TABLE => ctx.UserAllergies.ToList(),
                DatabaseConstants.ITEMS_TABLE => ctx.Items.ToList(),
                DatabaseConstants.ITEM_ALLERGENES_TABLE => ctx.ItemAllergenes.ToList(),
                _ => throw new InvalidDataException("unknown table name"),
            };
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
