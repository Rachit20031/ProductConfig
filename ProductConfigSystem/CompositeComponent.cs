/******************************************************************************
 * Filename    = CompositeComponent.cs
 *
 * Author      = rachit
 *
 * Product     = ProductConfigurationSystem
 * 
 * Project     = ProductConfigurationSystem
 *
 * Description = Represents a composite component in a product configuration system.
 *               Defines the properties and methods for adding, removing, and 
 *               managing child components, calculating the total price, and 
 *               validating the configuration of the composite component.
 *****************************************************************************/

namespace ProductConfigSystem
{
    /// <summary>
    /// Represents a composite component in a product configuration system.
    /// This class allows for the creation of complex components composed of 
    /// multiple child components, and it provides methods for adding, removing,
    /// and managing these child components, as well as calculating their total
    /// price and validating their configuration.
    /// </summary>
    public class CompositeComponent : IProductComponent
    {
        /// <summary>
        /// Gets the name of the composite component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Stores the child components of this composite component.
        /// </summary>
        private readonly List<IProductComponent> _components = new();

        /// <summary>
        /// Gets the list of mandatory components required by this composite component.
        /// </summary>
        public List<IProductComponent> MandatoryComponents { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeComponent"/> class.
        /// </summary>
        /// <param name="name">The name of the composite component.</param>
        public CompositeComponent( string name )
        {
            Name = name;
            MandatoryComponents = new List<IProductComponent>();
        }

        /// <summary>
        /// Gets the total price of the composite component, calculated based on its
        /// child components.
        /// </summary>
        public decimal Price => CalculatePrice();

        /// <summary>
        /// Adds a child component to this composite component.
        /// </summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent( IProductComponent component )
        {
            _components.Add( component );
        }

        /// <summary>
        /// Removes a child component from this composite component.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        public void RemoveComponent( IProductComponent component )
        {
            _components.Remove( component );
        }

        /// <summary>
        /// Calculates the total price of the composite component by summing up
        /// the prices of all its child components.
        /// </summary>
        /// <returns>The total price of the composite component.</returns>
        public decimal CalculatePrice()
        {
            decimal total = 0;
            foreach (IProductComponent component in _components)
            {
                total += component.CalculatePrice();
            }
            return total;
        }

        /// <summary>
        /// Validates the configuration of the composite component and its child components.
        /// </summary>
        /// <param name="context">The validation context used to validate components.</param>
        /// <param name="errors">A list of errors encountered during validation.</param>
        /// <returns><c>true</c> if the configuration is valid; otherwise, <c>false</c>.</returns>
        public bool ValidateConfiguration( ProductValidationContext context , out List<string> errors )
        {
            errors = new List<string>();
            bool isValid = true;

            // Validate the composite component itself
            if (!context.IsComponentValid( this ))
            {
                errors.Add( $"Composite '{Name}' is invalid due to price constraints." );
                isValid = false;
            }

            // Validate mandatory components
            if (!context.HasRequiredComponents( this , _components ))
            {
                foreach (IProductComponent mandatory in MandatoryComponents)
                {
                    errors.Add( $"Composite '{Name}' is missing mandatory component '{mandatory.Name}'." );
                }
                isValid = false;
            }

            // Validate child components
            foreach (IProductComponent component in _components)
            {
                if (!component.ValidateConfiguration( context , out List<string>? componentErrors ))
                {
                    errors.AddRange( componentErrors );
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Gets the child components of this composite component.
        /// </summary>
        /// <returns>An enumerable collection of child components.</returns>
        public IEnumerable<IProductComponent> GetComponents()
        {
            return _components;
        }
    }
}
