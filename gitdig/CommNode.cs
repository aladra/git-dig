using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace gitdig
{
    public class CommNode
    {
        private string[] _content;
        public string TreeId;
        public List<string> Parents = new List<string>();
        public readonly string FileSha;

        public CommNode(string text, string fileSha)
        {
            FileSha = fileSha;
            _content = text.Split('\n');
            TreeId = _content[0].Substring(_content[0].Length - 40, 40);

            foreach (var s in _content)
            {
                if (s.StartsWith("parent"))
                {
                    Parents.Add(  s.Substring(_content[1].Length - 40, 40));
                }
            }
        }

        public string FirstLine => _content.Length <= 0 ? "??" : _content[0];

        public string Comment => _content.Length <= 5 ? "??" : _content[5];

        public string Show()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File: {FileSha}");
            sb.AppendLine();
            foreach (var s in _content)
            {
                sb.AppendLine(s);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{TreeId}";
        }
    }
}
