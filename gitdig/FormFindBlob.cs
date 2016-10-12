using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gitdig
{
    public partial class FormFindBlob : Form
    {
        private Func<string, List<SearchResult>>  _finder;
        public FormFindBlob( Func< string, List<SearchResult>> finder)
        {
            _finder = finder;
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.HideSelection = false;
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Comment" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Tree" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Blob" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Found" });
            listView1.Columns.Add(new ColumnHeader() { Width = 100, Text = @"Written" });

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var results = _finder(textBoxName.Text.Trim());
            listView1.Items.Clear();
            foreach (var r in results)
            {
                string commMess = "???";
                if (r.Commit != null)
                {
                    commMess = r.Commit.Comment;
                }
                var item = listView1.Items.Add(commMess);
                item.SubItems.Add(r.Tree.FileSha);
                if (r.blob == null)
                {
                    item.SubItems.Add("No");
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add("Yes");
                    item.SubItems.Add(r.blob.LastWrite.ToString( "yyyy-MM-dd HH:mm:ss") );
                }

            }

        }
    }
}
