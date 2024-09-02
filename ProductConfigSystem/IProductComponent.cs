
namespace ProductConfigSystem
{
    public interface IProductComponent
    {
        string Name { get; }

        decimal Price { get; }

        decimal CalculatePrice();

        bool ValidateConfiguration(ProductValidationContext context, out List<string> errors);

        void AddComponent(IProductComponent component);

        void RemoveComponent(IProductComponent component);
    }
}
