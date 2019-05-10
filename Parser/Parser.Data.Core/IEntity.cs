namespace Parser.Data.Core
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
