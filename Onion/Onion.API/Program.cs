using Onion.API.Filters;
using Onion.Application;
using Onion.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inner layers. The API project is the composition root: the only place that
// knows which implementation backs each interface.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// API layer (controllers) + register domain exception filter globally
builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
