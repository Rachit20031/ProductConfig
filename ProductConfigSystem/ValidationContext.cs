
namespace ProductConfigSystem
{
    public class ProductValidationContext
    {
        public Func<IProductComponent, bool> IsComponentValid { get; set; }
        public Func<IProductComponent, List<IProductComponent>, bool> HasRequiredComponents { get; set; }

        public ProductValidationContext()
        {
            IsComponentValid = component => component.CalculatePrice() > 0;

            HasRequiredComponents = (component, components) =>
            {
                if (component is CompositeComponent composite)
                {
                    foreach (IProductComponent mandatory in composite.MandatoryComponents)
                    {
                        bool found = ContainsComponent(components, mandatory.Name);
                        if (!found)
                        {
                            return false;
                        }
                    }
                }
                return true;
            };
        }

        private static bool ContainsComponent(IEnumerable<IProductComponent> components, string name)
        {
            foreach (IProductComponent component in components)
            {
                if (component.Name == name)
                {
                    return true;
                }

                // If the component is a composite, check its children recursively
                if (component is CompositeComponent composite)
                {
                    if (ContainsComponent(composite.GetComponents(), name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
