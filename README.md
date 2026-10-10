# Envidan case

Three assignments in one solution (`EnvidanCase.sln`), all targeting .NET 9.

| Folder | What it is |
|---|---|
| `SOLID/` | Assignment 1, console app |
| `SOLID.Tests/` | xUnit tests for assignment 1 |
| `Onion/` | Assignment 3, REST API split into Domain, Application, Infrastructure and API, plus tests |

Assignment 2 is answered in this file only.

## Requirements

- .NET SDK 9.0 (`dotnet --version` should print 9.x)

## How to build and test

```bash
dotnet build
dotnet test
```

## How to run

```bash
# Assignment 1
dotnet run --project SOLID/SOLID.Assignment.csproj

# Assignment 3
dotnet run --project Onion/Onion.API/Onion.API.csproj
curl "http://localhost:5242/hello?name=Ana"
# -> Hello, Ana!
```

Swagger UI is available at `http://localhost:5242/swagger` while the API is running.

____________________________________________
# Assignment 1
Add support for Squares to the project in the `SOLID.Assignment` folder, and refactor it to adhere to the SOLID principles.
Describe which SOLID principles were relevant, and how you used them.
____________________________________________

### What changed

The project did not compile as delivered: `Circle` and `RightAngledTriangle` declared `IShape` but did not implement `GetArea()`, and the area formulas lived in `Program.cs`. The refactor moved each formula into its shape, added `Square`, and split the rest of `Program.cs` into two small classes.

```
SOLID/
  Program.cs              orchestration only
  Shapes/
    IShape.cs             Name + GetArea()
    Circle.cs
    RightAngledTriangle.cs
    Square.cs             new
  LargestShapeFinder.cs   finds the largest shape(s), handles ties
  ShapeReport.cs          writes the report to a TextWriter
SOLID.Tests/
  ShapeTests.cs
  LargestShapeFinderTests.cs
  ShapeReportTests.cs
```

The visible behaviour is the same as before: three shapes with random dimensions, one line per area, one line with the verdict.

Areas are computed in `double` from the first multiplication, so large dimensions do not overflow `int` on the way to the result.

### SOLID Principles

#### **Single Responsibility Principle**

Before refactoring, `Program` was responsible for creating shapes, computing their areas, comparing them, and printing the result. This violated the Single Responsibility Principle.

I separated these responsibilities among:

- the shape classes, which know how to calculate their own areas;
- `LargestShapeFinder`, which only compares shapes; and
- `ShapeReport`, which only writes the report.

Each class now has one reason to change.

#### **Open/Closed Principle**

Before refactoring, the area calculation and comparison logic were tightly coupled to specific shape types. This made the application harder to extend without modifying existing code.

After refactoring, adding a `Square` required a new file and one line in `Program.cs`. `LargestShapeFinder` and `ShapeReport` did not need to change because they depend on `IShape`.

#### **Liskov Substitution Principle**

All shapes implement `IShape` directly and validate their dimensions in their constructors, so a shape is either valid or does not exist. As a result, each shape can be used wherever an `IShape` is expected and will always return a valid area.

I deliberately did not model `Square` as a subtype of a rectangle. That is the textbook LSP violation: a square that forces width and height to be equal breaks any code that sets them independently.

#### **Interface Segregation Principle**

The `IShape` interface is intentionally small and focused on the behaviour required by the application.

Because the report needs each shape's name, I included `Name` in the `IShape` interface. This allows the report to access the name of any shape through the abstraction. I resisted adding things like perimeter or drawing methods that no consumer needs yet.

#### **Dependency Inversion Principle**

`ShapeReport` depends on the `TextWriter` abstraction rather than on `Console`, which makes it testable with a `StringWriter`.

`LargestShapeFinder` works with the `IShape` abstraction rather than depending on concrete shape classes.

I did not introduce interfaces for `LargestShapeFinder` or `ShapeReport` because the application has no need to substitute alternative implementations of either class.

____________________________________________
# Assignment 2
Name your 3 most commonly used Design Patterns, and briefly explain what they do
____________________________________________

**My choices**: Strategy, Factory Method, and Repository. They are the ones I reach for most often, so I describe each in general terms.

#### Strategy

**What it does.** Lets me swap an algorithm or a policy without the calling code knowing which variant it is talking to.

**How it works.** The behaviour that varies gets an interface. Each variant is a class that implements it. The client holds the interface and calls it; choosing the variant happens somewhere else, through configuration, a factory or DI.

**Where I see it.** `IShape.GetArea()` in Assignment 1 is a small one: each shape is a strategy for computing an area and `LargestShapeFinder` never knows which it has.

#### Factory Method

**What it does.** Separates the decision of *which* object to create from the code that needs the object.

**How it works.** A creator exposes a method that returns an abstraction. Subclasses, or the concrete creator picked at runtime, override that method to build the right product. Callers never write `new` against a concrete type, so adding a product does not touch them.

**Where I see it.** An exemple is a static factory method such as `Greeting.For(person)` in Assignment 3: a named creator that hides a constructor and keeps validation in one place.

#### Repository

**What it does.** Gives the business logic a collection-like way to store and load domain objects without knowing whether the data lives in SQL, a file, memory or another service.

**How it works.** An interface in the inner layer describes the operations in domain terms (`Save(Greeting)`, `GetById(id)`), never in storage terms. The implementation lives in the outer layer and is wired in at startup, so swapping the storage does not touch the business logic.

**Where I see it.** In Assignment 3, `IGreetingRepository` sits in the Application layer and `InMemoryGreetingRepository` in Infrastructure, which is also the clearest example of the Onion dependency rule: interface inside, implementation outside.

____________________________________________
# Assignment 3
Refactor the `Onion.Assignment` REST API into Onion Architecture.
____________________________________________

### Structure

The single `Onion.Assignment` project became four projects plus three test projects, one per layer that has logic worth testing.

```
Onion/
  Onion.Domain/                     entities and domain rules, no dependencies
    Entities/Person.cs              a name that cannot be blank
    Entities/Greeting.cs            how a greeting is worded
    Exceptions/DomainException.cs
  Onion.Application/                use cases and the interfaces they need
    Interfaces/IGreetingService.cs
    Interfaces/IGreetingRepository.cs
    Models/CreateGreetingRequest.cs
    Models/GreetingResult.cs
    Services/GreetingService.cs
    DependencyInjection.cs          AddApplication()
  Onion.Infrastructure/             implementations of the application interfaces
    Repositories/InMemoryGreetingRepository.cs
    DependencyInjection.cs          AddInfrastructure()
  Onion.API/                        HTTP in, HTTP out, composition root
    Controllers/GreetingController.cs
    Filters/DomainExceptionFilter.cs
    Program.cs
  Onion.Domain.Tests/               unit tests for the domain rules
  Onion.Application.Tests/          unit tests for the use case, with a fake repository
  Onion.API.Tests/                  integration tests through WebApplicationFactory
```

### Dependency rule

Every reference points inwards. The compiler enforces it: there is no `ProjectReference` going the other way, so the domain physically cannot call the database or the web framework.

| Project | References | NuGet packages |
|---|---|---|
| Onion.Domain | nothing | none |
| Onion.Application | Domain | `Microsoft.Extensions.DependencyInjection.Abstractions` |
| Onion.Infrastructure | Application, Domain | `Microsoft.Extensions.DependencyInjection.Abstractions` |
| Onion.API | Application, Infrastructure, Domain | `Microsoft.AspNetCore.OpenApi`, `Swashbuckle.AspNetCore` |

`Onion.API` is the only project that knows every layer. It is the composition root: `Program.cs` calls `AddApplication()` and `AddInfrastructure()` and that is the only place where an interface is tied to an implementation.

### Request flow

`GET /hello?name=Ana`

1. `GreetingController` receives the query string and builds a `CreateGreetingRequest`. It knows nothing about how a greeting is made.
2. `GreetingService` (Application) creates a `Person` and a `Greeting`, saves the greeting through `IGreetingRepository`, and returns a `GreetingResult`.
3. `Person` (Domain) rejects a blank name with a `DomainException`. `Greeting.For(person)` (Domain) is where the text `Hello, Ana!` comes from.
4. `InMemoryGreetingRepository` (Infrastructure) keeps the greeting in memory.
5. Back in the API, `DomainExceptionFilter` turns any `DomainException` into a `400` with a `ProblemDetails` body.

### Decisions

- **The contract is unchanged.** The original API answered `GET /hello?name=Ana` with `Hello, Ana!` as `text/plain`. The refactored one does the same. A refactor should not change behaviour.
- **One improvement.** The original threw an `ArgumentException` for a missing name, which the framework reported as a `500`. Now the same case is a `400` with a readable message. The rule itself moved into the domain (`Person`), so it is tested without HTTP and applies to any future entry point, not just this endpoint.
- **Validation lives in one place.** The query parameter is nullable on purpose so the framework does not pre-validate it. Null, empty and whitespace all reach `Person`, which is the single source of truth for that rule.
- **Controllers instead of minimal API.** Either works with Onion. I used a controller because the global exception filter and the explicit DI wiring read more clearly.
- **In-memory repository as a singleton.** It has to outlive a single request. A database-backed implementation would be scoped. Swapping it is a one-line change in `AddInfrastructure()`.
- **The in-memory store grows without limit.** Fine for a sample, not for a service that runs for days. A real one would need persistence or a retention policy, and that would be a change inside Infrastructure only.
- **One package reference removed.** The original project referenced both `Swashbuckle.AspNetCore` and `Swashbuckle.AspNetCore.SwaggerGen`. The second is already a dependency of the first, so it is gone. Everything else about OpenAPI and Swagger is as delivered.
- **This is more structure than a `/hello` endpoint needs.** Four projects for one endpoint is overkill on purpose: the assignment is about showing the layering and the dependency rule, not about the endpoint. In a real service this layout starts to pay off as soon as there is a second use case or a real database.

### Tests

```
Onion.Domain.Tests          Person validation, Greeting wording
Onion.Application.Tests     GreetingService with a fake repository
Onion.API.Tests             GET /hello?name=Ana -> 200 text/plain, missing/blank name -> 400
```

The domain and application tests run with no web host and no infrastructure, which is the practical payoff of the architecture.
