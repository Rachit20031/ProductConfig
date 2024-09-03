# Product Configuration System

## Overview
**Product Configuration System** is a software application designed to manage and validate complex product configurations using the Composite Design Pattern. The system allows users to create individual components, group them into composite components, and validate product configurations based on mandatory components and pricing rules. The system also allows for calculating the price of a product using the Composite Design Pattern.

The **Composite Design Pattern** is used to treat individual and composite components uniformly, allowing flexibility in defining and validating complex product structures. This pattern ensures that both individual parts (like a CPU) and composite parts (like a motherboard) can be handled consistently.

## Features
- **Component Creation:** Define individual components (e.g., CPU, RAM) with specific properties such as name and price.
- **Composite Components:** Group individual components into composite components (e.g., a motherboard consisting of a CPU, RAM, etc.).
- **Validation:** Validate product configurations to ensure all mandatory components are present and the configuration adheres to defined rules.
- **Pricing Calculation:** Calculate the total price of the configured product based on the components and composite components included.

### Attributes and Functions

- `Component`: Represents an individual component with attributes like name and price.
- `CompositeComponent`: Represents a group of components, allowing hierarchical product configurations.
- `AddComponent()`: Adds a component to a composite component.
- `RemoveComponent()`: Removes a component from a composite component.
- `ValidateConfiguration()`: Validates the configuration, ensuring all mandatory components are included.
- `CalculatePrice()`: Calculates the total price of the configuration.

### Example Components

- **CPU:** An individual component with a specific price.
- **RAM:** Another individual component with a specific price.
- **Motherboard:** A composite component that includes CPU, RAM, and other necessary components.

### Configuration Example

- **Gaming Computer:** A composite component that includes a motherboard (which itself is a composite component) and other necessary components like SSD and CPU.

### Validation Logic

- **Mandatory Components:** The system ensures that all required components for a composite component are present.
- **Pricing Rules:** Validates that the configuration does not exceed or fall below certain pricing thresholds.

## Technologies Used
- **C#**: The primary programming language used to implement the system.
- **.NET Core**: The framework that provides the runtime and libraries for building and running the application.
- **MSTest**: Used for testing the validation logic and ensuring the correctness of the implementation.

## How to Run the Project
1. **Clone the Repository**: Clone the project repository from the version control system.
    ```bash
    git clone https://github.com/your-repo-url/product-configuration-system.git
    ```
2. **Open in Visual Studio**: Open the solution file (`.sln`) in Visual Studio 2022.
3. **Build the Solution**: Build the solution by selecting `Build > Build Solution` from the menu.
4. **Run the Application**: Run the application using the `Start` button or by pressing `F5`.

## Design

### Composite Design Pattern
- **Uniformity:** Treats both individual and composite components uniformly.
- **Hierarchy:** Supports nested composite components, allowing for complex product structures.

### Class Diagram
A visual representation of the system's structure, showing the relationships between components and composite components.

## Environment
The project builds and runs with Visual Studio Community 2022.
