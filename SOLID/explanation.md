## SOLID Principles

### Single Responsibility Principle
Before refactoring, the Program was responsible for both calculating the area of the 
shape and comparing it to other shapes. This violated the
Single Responsibility Principle. After refactoring, the area calculation and comparison 
logic were separated into different classes.

Each shape is responsible for calculating its own area. The program itself is responsible 
only for orchestrating the application flow and comparing shapes.

### Open/Closed Principle

Before refactoring, the area calculation and comparison logic were tightly coupled 
to the specific shape types. This violated the Open/Closed Principle. 
After refactoring, the area calculation and comparison logic were 
abstracted into interfaces and implemented by each shape class.

If a new shape type is added, the existing area calculation and comparison logic does 
not need to be modified. 
Instead, the new shape class can implement the IShape interface and provide its own 
area calculation logic.
We just need to instantiate the new shape class and add it to the list of shapes to be compared.

### Liskov Substitution Principle

All shapes implement IShape and can be treated uniformly by the
application through the GetArea() contract.


### Interface Segregation Principle

The IShape interface is intentionally small and focused on the behavior required by 
the application. 

As the comparison method uses the name of the shape I assumed that the name should be part
of the IShape interface.
This allows the comparison method to access the name of any shape without needing to
know its concrete type.
I did not use the GetType() because the triangle has a different name than the class name,
so I added a Name property to the IShape interface.

### Dependency Inversion Principle

The comparison logic depends on the IShape abstraction rather than
concrete shape types. Full dependency inversion is not particularly
necessary for this small console application, since there are no
external dependencies or infrastructure concerns.