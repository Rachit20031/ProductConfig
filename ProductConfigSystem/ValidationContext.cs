/******************************************************************************
 * Filename    = ProductValidationContext.cs
 *
 * Author      = rachit
 *
 * Product     = ProductConfigurationSystem
 * 
 * Project     = ProductConfigurationSystem
 *
 * Description = Provides validation context for product components.
 *               Includes functions to check component validity and
 *               ensure required components are present in a configuration.
 *****************************************************************************/

namespace ProductConfigSystem
{
    /// <summary>
    /// Provides validation context for product components, including functions to
    /// validate individual components and ensure that all required components
    /// are present in a configuration.
    /// </summary>
    public class ProductValidationContext
    {
        /// <summary>
        /// A function to determine if a given component is valid.
        /// </summary>
        public Func<IProductComponent , bool> IsComponentValid { get; set; }

        /// <summary>
        /// A function to check if the given list of components contains all
        /// required components for a composite component.
        /// </summary>
        public Func<IProductComponent , List<IProductComponent> , bool> HasRequiredComponents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductValidationContext"/> class
        /// with default validation functions.
        /// </summary>
        public ProductValidationContext()
        {
            // Default validation function to check if a component has a positive price
            IsComponentValid = component => component.CalculatePrice() > 0;

            // Default function to check if all mandatory components are present
            HasRequiredComponents = ( component , components ) =>
            {
                if (component is CompositeComponent composite)
                {
                    foreach (IProductComponent mandatory in composite.MandatoryComponents)
                    {
                        bool found = ContainsComponent( components , mandatory.Name );
                        if (!found)
                        {
                            return false;
                        }
                    }
                }
                return true;
            };
        }

        /// <summary>
        /// Checks if a component with the specified name exists within the given
        /// collection of components, including recursively checking within nested composites.
        /// </summary>
        /// <param name="components">The collection of components to search within.</param>
        /// <param name="name">The name of the component to search for.</param>
        /// <returns><c>true</c> if the component is found; otherwise, <c>false</c>.</returns>
        private static bool ContainsComponent( IEnumerable<IProductComponent> components , string name )
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
                    if (ContainsComponent( composite.GetComponents() , name ))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
