namespace ProductConfigSystem
{
    public class Component : IProductComponent
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Component(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void AddComponent(IProductComponent component)
        {
            throw new NotImplementedException("Cannot add a component to a simple component.");
        }

        public void RemoveComponent(IProductComponent component)
        {
            throw new NotImplementedException("Cannot remove a component from a simple component.");
        }

        public decimal CalculatePrice()
        {
            return Price;
        }

        public bool ValidateConfiguration(ProductValidationContext context, out List<string> errors)
        {
            errors = new List<string>();

            if (!context.IsComponentValid(this))
            {
                errors.Add($"Component '{Name}' is invalid due to price constraints.");
                return false;
            }

            return true;
        }
    }
}
