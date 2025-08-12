using Api.Security;
using Application.Abstractions;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Application.Profiles.Commands.UpsertProfileCommand).Assembly));

// Infrastructure (EF + Repo)
builder.Services.AddInfrastructure(builder.Configuration);

// UserContext (headers from Gateway)
//builder.Services.AddScoped<IUserContext, GatewayUserContext>();
//builder.Services.AddScoped<IUserContext, MockUserContext>();
builder.Services.AddScoped<IUserContext>(_ =>
    new MockUserContext(
        Guid.Parse("8A9DEFB6-E562-405B-9AC1-672E238BD20F"),
        "devuser@local",
        new List<string> { "DevRole" }
    ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<UserContextMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();