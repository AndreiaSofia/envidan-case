var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application / Infrastructure DI
builder.Services.AddScoped<Onion.Application.Interfaces.IGreetingService, Onion.Application.Services.GreetingService>();
builder.Services.AddSingleton<Onion.Application.Interfaces.IGreetingRepository, Onion.Infrastructure.Repositories.GreetingRepository>();

// API layer (controllers) + register domain exception filter globally
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(Onion.API.Filters.DomainExceptionFilter));
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
