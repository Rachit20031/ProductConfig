using ProductConfigSystem;

// Create individual components
var cpu = new Component( "Intel i9 CPU" , 500 );
var ram = new Component( "16GB RAM" , 150 );
var ssd = new Component( "1TB SSD" , 200 );

// Create a composite component (e.g., a motherboard)
var motherboard = new CompositeComponent( "Motherboard" );
motherboard.AddComponent( cpu );
motherboard.AddComponent( ram );
//motherboard.MandatoryComponents.Add(ssd); // Motherboard needs SSD

// Create the final product (e.g., a computer)
var computer = new CompositeComponent( "Gaming Computer" );
computer.AddComponent( motherboard );
computer.AddComponent( ssd );

// Set mandatory components for the computer
computer.MandatoryComponents.Add( cpu ); // Computer needs CPU

// Create validation context
var context = new ProductValidationContext();

// Validate configuration and collect errors
bool isValid = computer.ValidateConfiguration( context , out List<string>? errors );
if (isValid)
{
    Console.WriteLine( "Configuration is valid." );
    // Calculate the total price
    decimal totalPrice = computer.CalculatePrice();
    Console.WriteLine( $"Total Price: ${totalPrice}" );
}
else
{
    Console.WriteLine( "Configuration is invalid:" );
    foreach (string error in errors)
    {
        Console.WriteLine( $"- {error}" );
    }
}
