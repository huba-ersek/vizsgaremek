using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

// TODO: newitem, edititem, deletetime
//       filter by fields
//       login form
//       experiment with more dynamic queries
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
            int width = (tableView.Width - tableView.RowHeadersWidth) / tableView.Columns.Count;
            foreach (DataGridViewColumn column in tableView.Columns)
            {
                column.Width = width;
            }
            //MessageBox.Show(tableView.RowHeadersWidth.ToString());
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
            if (selectedItem == null)
            {
                MessageBox.Show("Nincs tábla kiválasztva!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tableName = selectedItem.Text!;
            if (FitnessContext.IsTableImmutable(tableName))
            {
                MessageBox.Show("Ennek a táblának az adatai nem törölhetõek!", "Hiba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var ctx = new FitnessContext();
            /*var set = ctx.GetSetByTableName(selectedItem.Text!);
            Type entityType = set!.ElementType;
            var a = ctx.Model.GetEntityTypes().First(e => e.DisplayName() == entityType.Name);
            MessageBox.Show(string.Join('\n', a.GetKeys().Where(k => k.IsPrimaryKey()).Select(k => k)));*/
            if (tableView.SelectedRows.Count == 0)
            {
                MessageBox.Show("A törléshez válasszon ki legalább egy adatsort!", "Hiba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var selectedRows = new DataGridViewRow[tableView.SelectedRows.Count];
            tableView.SelectedRows.CopyTo(selectedRows, 0);
            var result = MessageBox.Show("Biztosan akarja törölni a kiválasztott adatokat?", "Megerõsítés",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;
            switch (tableName)
            {
                case DatabaseConstants.USERS_TABLE:
                    {
                        var selectedIds = selectedRows.Select(row => (int)row.Cells["Id"].Value!);
                        var users = ctx.Users.Where(u => selectedIds.Contains(u.Id));
                        ctx.Users.RemoveRange(users);
                        break;
                    }
                case DatabaseConstants.USER_ALLERGIES_TABLE:
                    {
                        var selectedIdPairs = selectedRows
                        .Select(row => ((int)row.Cells["UserId"].Value!, (int)row.Cells["AllergeneId"].Value!));
                        bool selector(UserAllergies ua) => selectedIdPairs.Contains((ua.UserId, ua.AllergeneId));
                        var userAllergies = ctx.UserAllergies.Where(selector);
                        ctx.UserAllergies.RemoveRange(userAllergies);
                        break;
                    }
                case DatabaseConstants.ITEMS_TABLE:
                    {
                        var selectedIds = selectedRows.Select(row => (int)row.Cells["Id"].Value!);
                        var items = ctx.Items.Where(i => selectedIds.Contains(i.Id));
                        ctx.Items.RemoveRange(items);
                        break;
                    }
                case DatabaseConstants.ITEM_ALLERGENES_TABLE:
                    {
                        var selectedIdPairs = selectedRows
                            .Select(row => ((int)row.Cells["ItemId"].Value!, (int)row.Cells["AllergeneId"].Value!));
                        bool selector(ItemAllergenes ia) => selectedIdPairs.Contains((ia.ItemId, ia.AllergeneId));
                        var itemAllergenes = ctx.ItemAllergenes.Where(selector);
                        ctx.ItemAllergenes.RemoveRange(itemAllergenes);
                        break;
                    }
                default:
                    throw new InvalidDataException("unknown table name");
            }
            ctx.SaveChanges();
            FillTableView(tableName);
            //ctx.Database.Sql
            //MessageBox.Show(string.Join('\n', entityType.GetProperties().Select(i => i.Name)));
            //tableView.SelectedRows
        }
    }
}
