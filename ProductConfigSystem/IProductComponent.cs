/******************************************************************************
 * Filename    = IProductComponent.cs
 *
 * Author      = rachit
 *
 * Product     = ProductConfigurationSystem
 * 
 * Project     = ProductConfigurationSystem
 *
 * Description = Defines the contract for product components in the
 *               product configuration system. Components can be
 *               individual items or composite items containing other
 *               components. Includes methods for calculating prices,
 *               validating configurations, and managing child components.
 *****************************************************************************/

namespace ProductConfigSystem
{
    /// <summary>
    /// Defines the contract for product components in the product configuration system.
    /// Components can be either individual items or composite items containing other components.
    /// </summary>
    public interface IProductComponent
    {
        /// <summary>
        /// Gets the name of the product component.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the price of the product component.
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Calculates the price of the product component.
        /// </summary>
        /// <returns>The price of the component.</returns>
        decimal CalculatePrice();

        /// <summary>
        /// Validates the configuration of the product component.
        /// </summary>
        /// <param name="context">The validation context used for validating the component.</param>
        /// <param name="errors">A list of errors encountered during validation.</param>
        /// <returns><c>true</c> if the component configuration is valid; otherwise, <c>false</c>.</returns>
        bool ValidateConfiguration( ProductValidationContext context , out List<string> errors );

        /// <summary>
        /// Adds a child component to the product component.
        /// This method is only applicable for composite components.
        /// </summary>
        /// <param name="component">The child component to add.</param>
        void AddComponent( IProductComponent component );

        /// <summary>
        /// Removes a child component from the product component.
        /// This method is only applicable for composite components.
        /// </summary>
        /// <param name="component">The child component to remove.</param>
        void RemoveComponent( IProductComponent component );
    }
}
