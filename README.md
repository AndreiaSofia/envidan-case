# Assignment 1
Add support for Squares to the project in the `SOLID.Assignment` folder, and refactor it to adhere to the SOLID principles.
Describe which SOLID principles were relevant, and how you used them.

## SOLID Principles

### Single Responsibility Principle
Before refactoring, the Program was responsible for create shapes, compute areas, compare them and print the result. This violated the
Single Responsibility Principle. 
I split this into: 
	- shapes that know their own area,
	- a LargestShapeFinder that only compares, 
	- and a ShapeReport that only writes. 

Each class now has one reason to change.


### Open/Closed Principle

Before refactoring, the area calculation and comparison logic were tightly coupled 
to the specific shape types. This violated the Open/Closed Principle. 

After refactoring, adding Square required one new file and one line in Program.cs. 
The finder and the report did not change, because they only depend on IShape.


### Liskov Substitution Principle

All shapes implement IShape directlyand validate their dimensions in the constructor, 
so any IShape instance is guaranteed to return a valid area.


### Interface Segregation Principle

The IShape interface is intentionally small and focused on the behavior required by 
the application. 

As the report needs the name of the shape I assumed that the name should be part of the IShape interface.
This allows the report to access the name of any shape.

### Dependency Inversion Principle

ShapeReport depends on TextWriter, not on Console, which makes it testable with a StringWriter. 
LargestShapeFinder depends on the IShape abstraction, never on concrete shapes. 
I did not introduce interfaces for the finder or the report because there is no second implementation and no test needs to replace them.

# Assignment 2
Name your 3 most commonly used Design Patterns, and briefly explain what they do

# Assignment 3
Refactor the `Onion.Assignment` REST API into Onion Architecture.