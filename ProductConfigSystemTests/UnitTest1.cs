/******************************************************************************
 * Filename    = ProductConfigurationSystemTests.cs
 *
 * Author      = rachit
 *
 * Product     = ProductConfigurationSystem
 * 
 * Project     = ProductConfigurationSystemTests
 *
 * Description = Contains unit tests for the Product Configuration System.
 *               Tests include validation and price calculation for
 *               Component and CompositeComponent classes, as well as
 *               validation context functionality.
 *****************************************************************************/

using ProductConfigSystem;

namespace ProductConfigSystemTests
{
    /// <summary>
    /// Contains unit tests for the Product Configuration System.
    /// Tests include validation and price calculation for Component
    /// and CompositeComponent classes, as well as validation context functionality.
    /// </summary>
    [TestClass]
    public class ProductConfigurationSystemTests
    {
        #region Tests for Component class

        /// <summary>
        /// Tests that the CalculatePrice method returns the correct price for a component.
        /// </summary>
        [TestMethod]
        public void Component_CalculatePrice_ReturnsCorrectPrice()
        {
            var component = new Component( "TestComponent" , 100m );
            decimal price = component.CalculatePrice();
            Assert.AreEqual( 100m , price );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns true for a valid component.
        /// </summary>
        [TestMethod]
        public void Component_ValidateConfiguration_ValidComponent_ReturnsTrue()
        {
            var component = new Component( "TestComponent" , 100m );
            var context = new ProductValidationContext();
            bool result = component.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsTrue( result );
            Assert.AreEqual( 0 , errors.Count );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns false for an invalid component.
        /// </summary>
        [TestMethod]
        public void Component_ValidateConfiguration_InvalidComponent_ReturnsFalse()
        {
            var component = new Component( "TestComponent" , -10m );
            var context = new ProductValidationContext();
            bool result = component.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsFalse( result );
            Assert.AreEqual( 1 , errors.Count );
            Assert.AreEqual( "Component 'TestComponent' is invalid due to price constraints." , errors[0] );
        }

        /// <summary>
        /// Tests that attempting to add a component to a simple component throws NotImplementedException.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void Component_AddComponent_ThrowsNotImplementedException()
        {
            var component = new Component( "TestComponent" , 100m );
            component.AddComponent( new Component( "SubComponent" , 50m ) );
        }

        /// <summary>
        /// Tests that attempting to remove a component from a simple component throws NotImplementedException.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void Component_RemoveComponent_ThrowsNotImplementedException()
        {
            var component = new Component( "TestComponent" , 100m );
            component.RemoveComponent( new Component( "SubComponent" , 50m ) );
        }

        #endregion

        #region Tests for CompositeComponent class

        /// <summary>
        /// Tests that the CalculatePrice method returns the correct total price for a composite component.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_CalculatePrice_ReturnsCorrectTotalPrice()
        {
            var composite = new CompositeComponent( "Composite" );
            composite.AddComponent( new Component( "Component1" , 100m ) );
            composite.AddComponent( new Component( "Component2" , 200m ) );
            decimal price = composite.Price;
            Assert.AreEqual( 300m , price );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns true for a valid composite component.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_ValidateConfiguration_ValidComposite_ReturnsTrue()
        {
            var composite = new CompositeComponent( "Composite" );
            var mandatoryComponent = new Component( "Component1" , 100m );
            composite.AddComponent( mandatoryComponent );
            composite.MandatoryComponents.Add( mandatoryComponent );
            var context = new ProductValidationContext();
            bool result = composite.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsTrue( result );
            Assert.AreEqual( 0 , errors.Count );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns false for a composite component with invalid price.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_ValidateConfiguration_InvalidCompositePrice_ReturnsFalse()
        {
            var composite = new CompositeComponent( "Composite" );
            var invalidComponent = new Component( "Component1" , -50m );
            composite.AddComponent( invalidComponent );
            composite.MandatoryComponents.Add( invalidComponent );
            var context = new ProductValidationContext();
            bool result = composite.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsFalse( result );
            Assert.AreEqual( 2 , errors.Count );
            Assert.AreEqual( "Composite 'Composite' is invalid due to price constraints." , errors[0] );
            Assert.AreEqual( "Component 'Component1' is invalid due to price constraints." , errors[1] );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns false when a mandatory component is missing in a composite.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_ValidateConfiguration_MissingMandatoryComponent_ReturnsFalse()
        {
            var composite = new CompositeComponent( "Composite" );
            var mandatoryComponent1 = new Component( "MandatoryComponent1" , 100m );
            var mandatoryComponent2 = new Component( "MandatoryComponent2" , 100m );
            composite.AddComponent( mandatoryComponent1 );
            composite.MandatoryComponents.Add( mandatoryComponent2 );
            var context = new ProductValidationContext();
            bool result = composite.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsFalse( result );
            Assert.AreEqual( 1 , errors.Count );
            Assert.AreEqual( "Composite 'Composite' is missing mandatory component 'MandatoryComponent2'." , errors[0] );
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method returns false for an invalid child component in a composite.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_ValidateConfiguration_InvalidChildComponent_ReturnsFalse()
        {
            var composite = new CompositeComponent( "Composite" );
            var validComponent = new Component( "Component1" , 100m );
            var invalidComponent = new Component( "Component2" , -20m );
            composite.AddComponent( validComponent );
            composite.AddComponent( invalidComponent );
            composite.MandatoryComponents.Add( validComponent );
            var context = new ProductValidationContext();
            bool result = composite.ValidateConfiguration( context , out List<string>? errors );
            Assert.IsFalse( result );
            Assert.AreEqual( 1 , errors.Count );
            Assert.AreEqual( "Component 'Component2' is invalid due to price constraints." , errors[0] );
        }

        /// <summary>
        /// Tests that adding a component to a composite component succeeds.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_AddComponent_Succeeds()
        {
            var composite = new CompositeComponent( "Composite" );
            var component = new Component( "Component1" , 100m );
            composite.AddComponent( component );
            Assert.AreEqual( 1 , composite.GetComponents().Count() );
            Assert.IsTrue( composite.GetComponents().Contains( component ) );
        }

        /// <summary>
        /// Tests that removing a component from a composite component succeeds.
        /// </summary>
        [TestMethod]
        public void CompositeComponent_RemoveComponent_Succeeds()
        {
            var composite = new CompositeComponent( "Composite" );
            var component = new Component( "Component1" , 100m );
            composite.AddComponent( component );
            composite.RemoveComponent( component );
            Assert.AreEqual( 0 , composite.GetComponents().Count() );
            Assert.IsFalse( composite.GetComponents().Contains( component ) );
        }

        #endregion

        #region Tests for ProductValidationContext class

        /// <summary>
        /// Tests that the IsComponentValid method returns true for a valid component.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_IsComponentValid_ReturnsTrueForValidComponent()
        {
            var component = new Component( "TestComponent" , 100m );
            var context = new ProductValidationContext();
            bool isValid = context.IsComponentValid( component );
            Assert.IsTrue( isValid );
        }

        /// <summary>
        /// Tests that the IsComponentValid method returns false for an invalid component.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_IsComponentValid_ReturnsFalseForInvalidComponent()
        {
            var component = new Component( "TestComponent" , 0m );
            var context = new ProductValidationContext();
            bool isValid = context.IsComponentValid( component );
            Assert.IsFalse( isValid );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method returns true when all mandatory components are present.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_HasRequiredComponents_ReturnsTrueWhenAllMandatoryComponentsPresent()
        {
            var context = new ProductValidationContext();
            var composite = new CompositeComponent( "Composite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            composite.MandatoryComponents.Add( mandatoryComponent );
            composite.AddComponent( mandatoryComponent );
            var components = composite.GetComponents().ToList();
            bool hasRequiredComponents = context.HasRequiredComponents( composite , components );
            Assert.IsTrue( hasRequiredComponents );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method returns false when a mandatory component is missing.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_HasRequiredComponents_ReturnsFalseWhenMandatoryComponentMissing()
        {
            var context = new ProductValidationContext();
            var composite = new CompositeComponent( "Composite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            composite.MandatoryComponents.Add( mandatoryComponent );
            var components = composite.GetComponents().ToList();
            bool hasRequiredComponents = context.HasRequiredComponents( composite , components );
            Assert.IsFalse( hasRequiredComponents );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method returns true for nested mandatory components.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_HasRequiredComponents_ReturnsTrueForNestedMandatoryComponents()
        {
            var context = new ProductValidationContext();
            var parentComposite = new CompositeComponent( "ParentComposite" );
            var childComposite = new CompositeComponent( "ChildComposite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            childComposite.MandatoryComponents.Add( mandatoryComponent );
            childComposite.AddComponent( mandatoryComponent );
            parentComposite.AddComponent( childComposite );
            parentComposite.MandatoryComponents.Add( mandatoryComponent );
            var components = parentComposite.GetComponents().ToList();
            bool hasRequiredComponents = context.HasRequiredComponents( parentComposite , components );
            Assert.IsTrue( hasRequiredComponents );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method returns false for missing nested mandatory components.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_HasRequiredComponents_ReturnsFalseForMissingNestedMandatoryComponents()
        {
            var context = new ProductValidationContext();
            var parentComposite = new CompositeComponent( "ParentComposite" );
            var childComposite = new CompositeComponent( "ChildComposite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            childComposite.MandatoryComponents.Add( mandatoryComponent );
            parentComposite.AddComponent( childComposite );
            parentComposite.MandatoryComponents.Add( mandatoryComponent );
            var components = parentComposite.GetComponents().ToList();
            bool hasRequiredComponents = context.HasRequiredComponents( parentComposite , components );
            Assert.IsFalse( hasRequiredComponents );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method finds a component recursively.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_ContainsComponent_FindsComponentRecursively_ReturnsTrue()
        {
            var context = new ProductValidationContext();
            var parentComposite = new CompositeComponent( "ParentComposite" );
            var childComposite = new CompositeComponent( "ChildComposite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            childComposite.MandatoryComponents.Add( mandatoryComponent );
            childComposite.AddComponent( mandatoryComponent );
            parentComposite.AddComponent( childComposite );
            parentComposite.MandatoryComponents.Add( childComposite );
            var components = parentComposite.GetComponents().ToList();
            bool contains = context.HasRequiredComponents( parentComposite , components );
            Assert.IsTrue( contains );
        }

        /// <summary>
        /// Tests that the HasRequiredComponents method does not find a component when it is missing.
        /// </summary>
        [TestMethod]
        public void ProductValidationContext_ContainsComponent_DoesNotFindComponent_ReturnsFalse()
        {
            var context = new ProductValidationContext();
            var parentComposite = new CompositeComponent( "ParentComposite" );
            var childComposite = new CompositeComponent( "ChildComposite" );
            var mandatoryComponent = new Component( "MandatoryComponent" , 100m );
            childComposite.MandatoryComponents.Add( mandatoryComponent );
            parentComposite.AddComponent( childComposite );
            parentComposite.MandatoryComponents.Add( mandatoryComponent );
            var components = new List<IProductComponent> { childComposite }; // Not adding mandatoryComponent here
            bool contains = context.HasRequiredComponents( parentComposite , components );
            Assert.IsFalse( contains );
        }

        #endregion
    }
}
