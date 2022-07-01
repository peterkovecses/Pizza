namespace Pizza.Data.Interfaces
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        string? Description { get; }
        decimal Price { get; }
        int CategoryId { get; }
    }
}
