using DBI.Application;
using DBI.Application.Commands;
using DBI.Application.MapperProfiles;
using DBI.Application.Queries;
using DBI.Application.Services;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true)); //-bullseye - slim - arm64v8
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("RemoteConnection")));

//builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MapperProfile).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IBreedIdentificationService, BreedIdentificationService>();
builder.Services.AddTransient<IDogBreedService, DogBreedService>();
builder.Services.AddTransient<IHistoryService, HistoryService>();

builder.Services.AddTransient<IDogBreedQuery, DogBreedQuery>();
builder.Services.AddTransient<IHistoryQuery, HistoryQuery>();

builder.Services.AddTransient<IDogBreedCommand, DogBreedCommand>();
builder.Services.AddTransient<IHistoryCommand, HistoryCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
