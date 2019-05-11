namespace Parser.Data.Core.Entities
{
    public class QueryParameter : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
