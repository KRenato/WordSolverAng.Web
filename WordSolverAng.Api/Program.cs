using WordSolverAng.Api.Services;
using WordSolverAng.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IWordRepositoryService, WordRepositoryService>();
builder.Services.AddSingleton<IConfigService, ConfigService>();
builder.Services.AddTransient<ILetterOptionService, LetterOptionService>();
builder.Services.AddTransient<IWordSolverService, WordSolverService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
