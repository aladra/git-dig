using System.Collections.Generic;
using System.Windows.Forms;

namespace gitdig
{
    public partial class FormCommits : Form
    {
        private ListViewItem _selected;

        public string SelectedSha => _selected?.Text;

        public FormCommits(List<CommNode> commits)
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.HideSelection = false;
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"SHA" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Comment" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Parent" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"First line" });
            foreach (var c in commits)
            {
                ListViewItem item = listView1.Items.Add(c.FileSha);
                item.SubItems.Add(c.Comment);
                item.SubItems.Add(c.Parents.Count.ToString( )) ;
                item.SubItems.Add(c.FirstLine);
                item.Tag = c;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _selected = listView1.SelectedItems.Count == 1 ? listView1.SelectedItems[0] : null;
        }

        private void linkLabelParent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selected == null)
                return;
            CommNode selNode = (CommNode) _selected.Tag;
            if (selNode.Parents.Count == 0)
                return;
            string parSha = selNode.Parents[0];
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == parSha)
                {
                    item.EnsureVisible();
                    item.Selected = true;
                    break;
                }
            }
        }
    }
}
