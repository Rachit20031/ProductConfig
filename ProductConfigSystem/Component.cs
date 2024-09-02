/******************************************************************************
 * Filename    = Component.cs
 *
 * Author      = rachit
 *
 * Product     = ProductConfigurationSystem
 * 
 * Project     = ProductConfigurationSystem
 *
 * Description = Represents a simple component in a product configuration system.
 *               Defines the properties and methods for a component, including
 *               its name, price, and methods for adding, removing components,
 *               calculating price, and validating configuration.
 *****************************************************************************/

namespace ProductConfigSystem
{
    /// <summary>
    /// Represents a simple component in a product configuration system.
    /// </summary>
    public class Component : IProductComponent
    {
        /// <summary>
        /// Gets the name of the component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the price of the component.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="name">The name of the component.</param>
        /// <param name="price">The price of the component.</param>
        public Component( string name , decimal price )
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Throws a <see cref="NotImplementedException"/> because components cannot 
        /// contain other components.
        /// </summary>
        /// <param name="component">The component to add.</param>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public void AddComponent( IProductComponent component )
        {
            throw new NotImplementedException( "Cannot add a component to a simple component." );
        }

        /// <summary>
        /// Throws a <see cref="NotImplementedException"/> because components cannot 
        /// contain other components.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public void RemoveComponent( IProductComponent component )
        {
            throw new NotImplementedException( "Cannot remove a component from a simple component." );
        }

        /// <summary>
        /// Calculates the price of the component.
        /// </summary>
        /// <returns>The price of the component.</returns>
        public decimal CalculatePrice()
        {
            return Price;
        }

        /// <summary>
        /// Validates the configuration of the component.
        /// </summary>
        /// <param name="context">The validation context.</param>
        /// <param name="errors">A list of errors encountered during validation.</param>
        /// <returns><c>true</c> if the component configuration is valid; otherwise, <c>false</c>.</returns>
        public bool ValidateConfiguration( ProductValidationContext context , out List<string> errors )
        {
            errors = new List<string>();

            if (!context.IsComponentValid( this ))
            {
                errors.Add( $"Component '{Name}' is invalid due to price constraints." );
                return false;
            }

            return true;
        }
    }
}
