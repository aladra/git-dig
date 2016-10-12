using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zlib;

namespace gitdig
{
    public partial class Form1 : Form
    {
        private List<CommNode> _commits = new List<CommNode>();
        private List<TreeNode> _trees = new List<TreeNode>();
        private List<BlobNode> _blobs = new List<BlobNode>();
        private List<string> _empties = new List<string>();

        private CommNode _selCommit;
        private TreeNode _selTree;
        private BlobNode _selBlob;

        private string _selKind;

        private DirectoryInfo _rootDir;
        private string _gitPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new FolderBrowserDialog()
            {
                Description = @"Find git folder"
            };
            var r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                OpenGitProj(f.SelectedPath);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenGitProj(@"C:\development\Timesheet");
        }

        private void OpenGitProj(string gitPath)
        {
            _commits = new List<CommNode>();
            _trees = new List<TreeNode>();
            _blobs = new List<BlobNode>();
            _empties = new List<string>();
            _gitPath = gitPath;
            _rootDir = new DirectoryInfo(gitPath + @"\.git\objects");

            int totFiles = ParseFiles(_rootDir);
            var report = new StringBuilder();
            report.AppendLine($"{totFiles} files checked, {_empties.Count} found");
            foreach (string sha in _empties)
            {
                TreeEntry entry = null;
                TreeNode homeTree = null;
                foreach (var t in _trees)
                {
                    entry = t.FindEntry(sha);
                    if (entry != null)
                    {
                        homeTree = t;
                        break;
                    }
                }

                report.AppendLine($"file {sha} is empty");
                if (homeTree != null)
                {
                    report.AppendLine($" referenced by {entry}");
                    report.AppendLine($" in tree {homeTree}");
                    foreach (var ent in homeTree.Entries)
                    {
                        report.AppendLine($"   {ent}");
                    }

                    var commit = _commits.FirstOrDefault(c => c.TreeId == homeTree.FileSha);
                    if (commit != null)
                    {
                        report.AppendLine($" has commit {commit.TreeId}");
                    }
                    else
                    {
                        report.AppendLine(" no commit found");
                    }
                }
                else
                {
                    report.AppendLine(" no reference found");
                }
            }
            textBox1.Text = report.ToString();
        }

        private int ParseFiles(DirectoryInfo di)
        {
            int j = 0;
            foreach (var sdir in di.GetDirectories())
            {
                if (sdir.Name == "pack" || sdir.Name == "info")
                    continue;
                foreach (var fi in sdir.GetFiles())
                {
                    string objSha = sdir.Name + fi.Name;

                    if (fi.Length == 0)
                    {
                        _empties.Add(objSha);
                    }

                    byte[] compressed = File.ReadAllBytes(fi.FullName);
                    byte[] result = ZlibStream.UncompressBuffer(compressed);

                    if (result.Length >= 4)
                    {
                        var ff = new UTF7Encoding(false);
                        string verdict = ff.GetString(result, 0, 4);
                        if (verdict == "comm")
                        {
                            string text = ff.GetString(result, 0, result.Length);
                            _commits.Add(new CommNode(text, objSha));
                        }
                        else if (verdict == "tree")
                        {
                            _trees.Add(new TreeNode(result, objSha));
                        }
                        else if (verdict == "blob")
                        {
                            _blobs.Add(new BlobNode(result, objSha, fi.LastWriteTime));
                        }
                        else
                        {
                            Console.WriteLine($@"Unknown type {verdict}");
                        }
                        j++;
                    }
                }
            }
            return j;
        }

        private void buttonComm_Click(object sender, EventArgs e)
        {
            var f = new FormCommits(_commits);
            var r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                _selKind = "comm";
                _selCommit = _commits.FirstOrDefault(c => c.FileSha == f.SelectedSha);
                DrawData();
            }
        }

        private void DrawData()
        {
            string tmp = "?";
            if (_selKind == "comm")
            {
                tmp = _selCommit.Show();
            }
            else if (_selKind == "tree")
            {
                tmp = _selTree.Show();
            }
            else if (_selKind == "blob")
            {
                tmp = _selBlob.Show();
            }
            textBox1.Text = tmp.Replace("\0", "\r\n");
        }

        private void linkLabelParent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selKind == "comm")
            {
                if (_selCommit.Parents.Count == 0)
                {
                    MessageBox.Show(@"No parents");
                    return;
                }
                var par = _commits.FirstOrDefault(n => _selCommit.Parents.Contains(n.FileSha));
                if (par == null)
                {
                    MessageBox.Show(@"Parent not found");
                    return;
                }
                _selCommit = par;
                DrawData();
            }
        }

        private void linkLabelChild_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selKind == "comm")
            {
                var par = _commits.FirstOrDefault(n => n.Parents.Contains(_selCommit.FileSha));
                if (par == null)
                {
                    MessageBox.Show(@"Child not found");
                    return;
                }
                _selCommit = par;
                DrawData();
            }
        }

        private void linkLabelTree_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selKind == "comm")
            {
                var tre = _trees.FirstOrDefault(t => t.FileSha == _selCommit.TreeId);
                if (tre == null)
                {
                    MessageBox.Show(@"Child not found");
                    return;
                }
                _selKind = "tree";
                _selTree = tre;
                DrawData();
            }
        }

        private void linkLabelRoot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target;
            if (_selKind == "tree")
            {
                target = _selTree.FileSha;
            }
            else if (_selKind == "blob")
            {
                target = _selBlob.FileSha;

            }
            else
            {
                return;
            }

            var tre = _trees.FirstOrDefault(t => t.FileSha == target);
            if (tre != null)
            {
                _selKind = "tree";
                _selTree = tre;
                DrawData();
                return;
            }

            var roo = _commits.FirstOrDefault(c => c.TreeId == target);
            if (roo != null)
            {
                _selKind = "comm";
                _selCommit = roo;
                DrawData();
                return;
            }
            MessageBox.Show(@"Root not found");

        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            if (_selKind == "tree")
            {
                var f = new FormSplit(_selTree);
                var r = f.ShowDialog(this);
                if (r == DialogResult.OK)
                {
                    if (f.SelectedSha != null)
                    {
                        var tre = _trees.FirstOrDefault(t => t.FileSha == f.SelectedSha);
                        if (tre != null)
                        {
                            _selKind = "tree";
                            _selTree = tre;
                            DrawData();
                            return;
                        }

                        var blo = _blobs.FirstOrDefault(b => b.FileSha == f.SelectedSha);
                        if (blo != null)
                        {
                            _selKind = "blob";
                            _selBlob = blo;
                            DrawData();
                            return;
                        }

                        MessageBox.Show(@"Not found");
                    }
                }
            }
        }

        private void buttonFindBlob_Click(object sender, EventArgs e)
        {
            var f = new FormFindBlob(FindTreesWithEntry);
            f.ShowDialog(this);
        }

        private List<SearchResult> FindTreesWithEntry(string match)
        {
            var result = new List<SearchResult>();
            foreach (var tre in _trees)
            {
                var ent = tre.Entries.FirstOrDefault(e => e.Name == match);
                if (ent != null)
                {
                    var r = new SearchResult() { Tree = tre, Entry = ent };
                    result.Add(r);
                    // find the commit
                    string nav = tre.FileSha;
                    while (true)
                    {
                        var par = _trees.FirstOrDefault(t => t.Entries.Any(e => e.Sha == nav));
                        if (par == null)
                        {
                            break;
                        }
                        nav = par.FileSha;
                    }
                    r.Commit = _commits.FirstOrDefault(c => c.TreeId == nav);

                    // find the blob
                    r.blob = _blobs.FirstOrDefault(b => b.FileSha == ent.Sha);

                }
            }

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            string fileNameIn = @"C:\Development\Timesheet\.git\objects\53\c28b3ef813a83972121723ad0393f83b8022b4";
            string fileNameOut = fileNameIn + "t";
            byte[] compressed = File.ReadAllBytes(fileNameIn);
            byte[] result = Ionic.Zlib.ZlibStream.UncompressBuffer(compressed);

            for (int j = 0; j < 10; j++)
            {
                using (FileStream fs = File.Create($"{fileNameIn}_{j}"))
                {
                    using (Ionic.Zlib.ZlibStream x = new Ionic.Zlib.ZlibStream(fs, CompressionMode.Compress, (CompressionLevel)j))
                    {
                        x.Write(result, 0, result.Length);

                    }
                }
            }
            */

            const string commName = "V1.8.8";
            ShaValue wasLink = new ShaValue("e1aacf0fe22a0f75ad8f00e327619398fe867908");
            ShaValue nowLink = new ShaValue("4881fac144084a0825b1f9037965f6edc48914b9");

            var cWork = _commits.FirstOrDefault(c => c.Comment == commName);
            if (cWork == null)
            {
                MessageBox.Show($@"Commit {commName} not found");
                return;
            }

            var tWork = _trees.FirstOrDefault(t => t.FileSha == cWork.TreeId);
            if (tWork == null)
            {
                MessageBox.Show(@"Tree not found");
                return;
            }

            ShaValue originalTreeSha = new ShaValue(tWork.FileSha);
            byte[] treeRaw = GetFileContents(originalTreeSha);

            int treeFound = ReplaceShaBin(treeRaw, wasLink, nowLink);
            if (treeFound != 1)
            {
                File.WriteAllBytes(@"C:\Alan\fail.bin", treeRaw);
                MessageBox.Show(@"Tree link not found");
                return;
            }
            ShaValue replaceTreeSha = DumpFile(treeRaw);

            ShaValue orgCommitSha = new ShaValue(cWork.FileSha);
            byte[] commitRaw = GetFileContents(orgCommitSha);
            int r2 = ReplaceShaAscii(commitRaw, originalTreeSha, replaceTreeSha);
            if (r2 != 1)
            {
                MessageBox.Show(@"Tree link in original commit not found");
                return;
            }

            ShaValue replaceCommit = DumpFile(commitRaw);

            // now walk later commits
            ShaValue existingCommit = new ShaValue(cWork.FileSha);
            CommNode par = _commits.FirstOrDefault(n => n.Parents.Contains(existingCommit.ToString()));
            while (par != null && par.Comment != "Snapshot")
            {
                Console.WriteLine($@"Update {par.Comment} {par.FileSha}");
                byte[] raw = GetFileContents(new ShaValue(par.FileSha));
                int r = ReplaceShaAscii(raw, existingCommit, replaceCommit);
                replaceCommit = DumpFile(raw);
                existingCommit = new ShaValue(par.FileSha);
                par = _commits.FirstOrDefault(n => n.Parents.Contains(existingCommit.ToString()));
            }

            // now change the head ref
            string refsPath = Path.Combine(_gitPath, @".git\packed-refs");
            string refs = File.ReadAllText(refsPath);
            string replace = refs.Replace(existingCommit.ToString(), replaceCommit.ToString());
            File.WriteAllText(refsPath, replace);
        }


        private int ReplaceShaAscii(byte[] raw, ShaValue from, ShaValue to)
        {
            return ReplaceBytes(raw, from.AsAscii(), to.AsAscii());
        }

        private int ReplaceShaBin(byte[] raw, ShaValue from, ShaValue to)
        {
            return ReplaceBytes(raw, from.Value, to.Value);
        }

        private static int ReplaceBytes(byte[] raw, byte[] from, byte[] to)
        {
            int replaced = 0;
            for (int j = 0; j < raw.Length - from.Length; j++)
            {
                bool match = true;
                for (int k = 0; k < from.Length; k++)
                {
                    if (raw[j + k] != from[k])
                    {
                        match = false;
                        break;
                    }
                }
                if (!match)
                    continue;

                for (int k = 0; k < from.Length; k++)
                {
                    raw[j++] = to[k];
                }
                replaced++;
            }
            return replaced;
        }

        private byte[] GetFileContents(ShaValue sha, bool kill = true)
        {
            string baseDir = _rootDir.FullName;
            string shaTxt = sha.ToString();
            string readPath = Path.Combine(Path.Combine(baseDir, shaTxt.Substring(0, 2)), shaTxt.Substring(2));
            byte[] compressed = File.ReadAllBytes(readPath);

            if (kill)
            {
                File.SetAttributes(readPath, FileAttributes.Normal);
                File.Delete(readPath);
            }

            return ZlibStream.UncompressBuffer(compressed);
        }

        private ShaValue DumpFile(byte[] raw)
        {
            string baseDir = _rootDir.FullName;
            var shaBytes = ShaValue.FromContents(raw);

            string shaTxt = shaBytes.ToString();
            string tmpFolder = Path.Combine(baseDir, shaTxt.Substring(0, 2));
            if (!Directory.Exists(tmpFolder))
            {
                Directory.CreateDirectory(tmpFolder);
            }
            string tmpPath = Path.Combine(tmpFolder, shaTxt.Substring(2));
            if (File.Exists(tmpPath))
            {
                File.Delete(tmpPath);
            }

            using (FileStream fs = File.Create(tmpPath))
            {
                using (ZlibStream x = new ZlibStream(fs, CompressionMode.Compress, (CompressionLevel)1))
                {
                    x.Write(raw, 0, raw.Length);
                }
            }
            return shaBytes;
        }
    }
}
