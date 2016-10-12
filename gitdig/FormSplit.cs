using System;
using System.Windows.Forms;

namespace gitdig
{
    public partial class FormSplit : Form
    {
        public string SelectedSha => _selected?.Text;

        private ListViewItem _selected;

        public FormSplit( TreeNode tree )
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.HideSelection = false;
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"SHA" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Name" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Id" });
            foreach (var e in tree.Entries )
            {
                ListViewItem item = listView1.Items.Add(e.Sha );
                item.SubItems.Add(e.Name );
                item.SubItems.Add(e.Id );
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selected = listView1.SelectedItems.Count == 1 ? listView1.SelectedItems[0] : null;
        }
    }
}
