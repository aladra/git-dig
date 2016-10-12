namespace gitdig
{
    public class TreeEntry
    {
        public readonly string Id;
        public readonly string Name;
        public readonly string Sha;

        public TreeEntry(string id, string name, string sha)
        {
            Id = id;
            Name = name;
            Sha = sha;
        }

        public override string ToString()
        {
            return $"{Sha} {Id} {Name}";
        }
    }
}
