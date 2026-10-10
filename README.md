# Assignment 1
Add support for Squares to the project in the `SOLID.Assignment` folder, and refactor it to adhere to the SOLID principles.
Describe which SOLID principles were relevant, and how you used them.
____________________________________________

### SOLID Principles

#### **Single Responsibility Principle**
	Before refactoring, Program was responsible for creating shapes, computing their areas, comparing them, and printing the result. This violated the Single Responsibility Principle.

	I separated these responsibilities among:

	- shape classes, which know how to calculate their own areas;

	- LargestShapeFinder, which only compares shapes; and

	- ShapeReport, which only writes the report.

	Each class now has one reason to change.


#### **Open/Closed Principle**

	Before refactoring, the area calculation and comparison logic were tightly coupled to specific shape types. This made the application harder to extend without modifying existing code.

	After refactoring, adding a Square required a new file and one line in Program.cs. LargestShapeFinder and ShapeReport did not need to change because they depend on IShape.


#### **Liskov Substitution Principle**

	All shapes implement IShape directly and validate their dimensions in their constructors. As a result, each shape can be used wherever an IShape is expected, provided it follows the interface’s contract and returns a valid area.


#### **Interface Segregation Principle**

	The IShape interface is intentionally small and focused on the behavior required by the application.

	Because the report needs each shape’s name, I included Name in the IShape interface. This allows the report to access the name of any shape through the abstraction.

#### **Dependency Inversion Principle**

	ShapeReport depends on the TextWriter abstraction rather than on Console, which makes it testable with a StringWriter.

	LargestShapeFinder works with the IShape abstraction rather than depending on concrete shape classes.

	I did not introduce interfaces for LargestShapeFinder or ShapeReport because the application has no need to substitute alternative implementations of either class.
____________________________________________
# Assignment 2
Name your 3 most commonly used Design Patterns, and briefly explain what they do
____________________________________________

**My choices**: Strategy, Factory Method, and Repository.

#### Strategy

	**Problem.** I have behavior that varies—an algorithm, a calculation, or a policy—and I don’t want `if` statements scattered throughout the code to choose between variants.

	**How it works.** I define an interface for the behavior. Each variant is a class that implements it. The client code receives the interface and calls it without knowing 
	which variant it has.

	**In this case.** `IShape` with `GetArea()` is a Strategy. 
	Each shape is a strategy for calculating area, and `LargestShapeFinder` is the client that doesn’t know which one it gets.


#### Factory Method

	**Problem.** Creating an object involves decisions—which concrete type to use, which dependencies it needs, and how it should be configured—and 
	I don’t want the code that uses the object to have to make those decisions.

	**How it works.** A method (or a small class) encapsulates the creation process. The client asks, “Give me an X,” and receives an abstraction. 
	Changing how the object is created does not affect the code that requests it.

	**In this case.** The `AddApplication()` and `AddInfrastructure()` extension methods in Onion register dependencies with the container. 
	When an endpoint requests a `GreetHandler`, the container knows how to construct it with the right repository and clock.
	There is no factory in the SOLID example, because I assume that three `new` expressions in `Program.cs` don’t justify having one.

#### Repository

	**Problem.** Business logic needs to store and retrieve domain objects, but I don’t want it to know whether the data is stored in SQL, a file, memory, or an HTTP service.

	**How it works.** An interface in the inner layer (`IGreetingRepository`) describes operations in domain terms (`AddAsync(Greeting)`, not `ExecuteSql`). 
	The implementation lives in the outer layer and is selected at startup through DI.

	**In this case.** `IGreetingRepository` is in the Application layer, and `InMemoryGreetingRepository` is in Infrastructure. 
	`GreetHandler` can be tested with a fake that stores greetings in a list. 

	This is also the clearest demonstration of the Onion Architecture’s dependency rule: the interface is on the inside, and the implementation is on the outside.

____________________________________________
# Assignment 3
Refactor the `Onion.Assignment` REST API into Onion Architecture.
____________________________________________
