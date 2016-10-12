using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace gitdig
{
    public class BlobNode
    {
        public readonly string FileSha;
        private readonly byte[] _raw;
        public DateTime LastWrite;

        public BlobNode(byte[] raw, string fileSha, DateTime lastWrite)
        {
            _raw = raw;
            FileSha = fileSha;
            LastWrite = lastWrite;
        }

        public string Show()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File: {FileSha}");
            sb.AppendLine();

            var enc = new UTF7Encoding(false);
            string dec = enc.GetString(_raw, 0, _raw.Length );

            sb.Append(dec);
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Blob {FileSha}";
        }
    }
}
