using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gitdig
{
    public class TreeNode
    {
        private readonly int _decimal;
        public readonly List<TreeEntry> Entries = new List<TreeEntry>();
        public readonly string FileSha;

        public TreeNode(byte[] raw, string fileSha)
        {
            FileSha = fileSha;
            var enc = new UTF7Encoding(false);

            int pos = 4;
            while (raw[pos++] == ' ') {}
            int j = pos-1;
            while (raw[pos++] != '\0') {}
            string dec = enc.GetString(raw, j, pos-j -1);
            _decimal = int.Parse(dec);

            while (++pos < raw.Length)
            {
                j = pos-1;
                while ( char.IsDigit((char)raw[pos++])) {}
                string oct = enc.GetString(raw, j, pos - j);
                while (raw[pos++] == ' ') { }
                j = pos-1;
                while (raw[pos++] != '\0') { }
                string name = enc.GetString(raw, j, pos - j-1);
                string sha = string.Empty;
                for (int k = 0; k < 20; k++)
                {
                    sha += $"{raw[pos++]:x2}";
                }
                Entries.Add( new TreeEntry( oct, name , sha ));
            }
        }

        public TreeEntry FindEntry(string sha)
        {
            return Entries.FirstOrDefault(e => e.Sha == sha);
        }

        public string Show()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File: {FileSha}");
            sb.AppendLine($"Dec: {_decimal}");

            foreach (var s in Entries)
            {
                sb.AppendLine(s.ToString());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{_decimal} has {Entries.Count} entries, {FileSha}";
        }
    }
}
