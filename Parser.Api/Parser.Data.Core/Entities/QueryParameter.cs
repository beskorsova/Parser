namespace Parser.Data.Core.Entities
{
    public class QueryParameter : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public long LogLineId { get; set; }
    }
}
