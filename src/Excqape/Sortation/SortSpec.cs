namespace Excqape.Sortation
{
    public class SortSpec
    {
        public string Property { get; set; }
        public SortDirection Direction { get; set; }

        public SortSpec(string property, SortDirection direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
