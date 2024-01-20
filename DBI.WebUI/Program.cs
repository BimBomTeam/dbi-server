using DBI.Application;
using DBI.Application.Commands;
using DBI.Application.MapperProfiles;
using DBI.Application.Queries;
using DBI.Application.Services;
using DBI.Application.Services.Authorization;
using DBI.Application.Services.MlNet;
using DBI.Domain.Helpers;
using DBI.Infrastructure.Commands;
using DBI.Infrastructure.Queries;
using DBI.Infrastructure.Services;
using DBI.Infrastructure.Services.Authorization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using static Community.CsharpSqlite.Sqlite3;
using static Tensorflow.RewriterConfig.Types;
using System.Resources;
using Tensorflow.Operations.Initializers;
using Firebase.Auth;
using DBI.Infrastructure.Providers;
using DBI.Application.Providers;
using Microsoft.Extensions.DependencyInjection;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

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

builder.Services.AddHttpClient<IJwtProvider, JwtProvider>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["Authentification:TokenUri"]);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("RemoteConnection")));

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "service-account-key.json"));

//builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MapperProfile).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddSingleton(FirebaseApp.Create());

builder.Services.AddSingleton<IAiModelService, MlNetService>();
builder.Services.AddTransient<IBreedIdentificationService, BreedIdentificationService>();
builder.Services.AddTransient<IDogBreedService, DogBreedService>();
builder.Services.AddTransient<IHistoryService, HistoryService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
{
    ApiKey = "AIzaSyBS2mCxeFs_o8aNUG3-vjrClC_4TsEJkdM",
    AuthDomain = $"dogbreed-488c5.firebaseapp.com",
    Providers = new FirebaseAuthProvider[]
    {
        new EmailProvider()
    }
}));

builder.Services.AddTransient<IFirebaseAuthService, FirebaseAuthService>();

builder.Services.AddTransient<IDogBreedQuery, DogBreedQuery>();
builder.Services.AddTransient<IHistoryQuery, HistoryQuery>();
builder.Services.AddTransient<IAuthQuery, AuthQuery>();

builder.Services.AddTransient<IDogBreedCommand, DogBreedCommand>();
builder.Services.AddTransient<IHistoryCommand, HistoryCommand>();
builder.Services.AddTransient<IAuthCommand, AuthCommand>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
    {

        jwtOptions.Authority = builder.Configuration["Authentification:ValidIssuer"];
        jwtOptions.Audience = builder.Configuration["Authentification:Audience"];
        jwtOptions.TokenValidationParameters.ValidIssuer = builder.Configuration["Authentification:ValidIssuer"];
    });


FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "service-account-key.json")),
    //ServiceAccountId = "118361261080229976644@dogbreed-488c5.iam.gserviceaccount.com"
});

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.Seed();

app.Run();
