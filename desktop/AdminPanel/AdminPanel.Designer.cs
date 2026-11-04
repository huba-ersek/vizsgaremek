namespace AdminPanel
{
    partial class AdminPanel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableView = new DataGridView();
            menuStrip = new MenuStrip();
            tablesMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)tableView).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableView
            // 
            tableView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableView.Location = new Point(0, 25);
            tableView.Name = "tableView";
            tableView.Size = new Size(800, 425);
            tableView.TabIndex = 0;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { tablesMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // tablesMenuItem
            // 
            tablesMenuItem.Name = "tablesMenuItem";
            tablesMenuItem.Size = new Size(53, 20);
            tablesMenuItem.Text = "Táblák";
            // 
            // AdminPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableView);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "AdminPanel";
            Text = "Admin Panel";
            Load += AdminPanel_Load;
            ((System.ComponentModel.ISupportInitialize)tableView).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView tableView;
        private MenuStrip menuStrip;
        private ToolStripMenuItem tablesMenuItem;
    }
}
