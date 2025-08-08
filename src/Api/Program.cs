using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // ��Ǫ����Դ metadata ����Ѻ Swagger
builder.Services.AddSwaggerGen();             // �Դ��ҹ Swagger

// Register DI
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<ProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // �Դ endpoint /swagger/v1/swagger.json
    app.UseSwaggerUI();     // �Դ UI /swagger/index.html
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();