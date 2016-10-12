using System;

namespace gitdig
{
    public class SearchResult
    {
        public TreeNode Tree { get; set; }
        public TreeEntry Entry { get; set; }
        public CommNode Commit { get; set; }
        public BlobNode blob { get; set; }
    }
}
