
namespace ProductConfigSystem
{
    public class CompositeComponent : IProductComponent
    {
        public string Name { get; private set; }
        private readonly List<IProductComponent> _components = new ();
        public List<IProductComponent> MandatoryComponents { get; private set; }

        public CompositeComponent(string name)
        {
            Name = name;
            MandatoryComponents = new List<IProductComponent>();
        }

        public decimal Price => CalculatePrice();

        public void AddComponent(IProductComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(IProductComponent component)
        {
            _components.Remove(component);
        }

        public decimal CalculatePrice()
        {
            decimal total = 0;
            foreach (IProductComponent component in _components)
            {
                total += component.CalculatePrice();
            }
            return total;
        }

        public bool ValidateConfiguration(ProductValidationContext context, out List<string> errors)
        {
            errors = new List<string>();
            bool isValid = true;

            // Validate the composite component itself
            if (!context.IsComponentValid(this))
            {
                errors.Add($"Composite '{Name}' is invalid due to price constraints.");
                isValid = false;
            }

            // Validate mandatory components
            if (!context.HasRequiredComponents(this, _components))
            {
                foreach (IProductComponent mandatory in MandatoryComponents)
                {
                    errors.Add($"Composite '{Name}' is missing mandatory component '{mandatory.Name}'.");
                }
                isValid = false;
            }

            // Validate child components
            foreach (IProductComponent component in _components)
            {
                if (!component.ValidateConfiguration(context, out List<string>? componentErrors ))
                {
                    errors.AddRange(componentErrors);
                    isValid = false;
                }
            }

            return isValid;
        }

        public IEnumerable<IProductComponent> GetComponents()
        {
            return _components;
        }
    }
}
