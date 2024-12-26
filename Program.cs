using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapitest;
using webapitest.Business.ArtifactGeneration;
using webapitest.Data;
using webapitest.Middlewares.JwtDataExtraction;
using webapitest.Repository;
using webapitest.Repository.ArtifactGeneration;
using webapitest.Repository.ArtifactGeneration.Interfaces;
using webapitest.Repository.Fragment;
using webapitest.Repository.Fragment.Interfaces;
using webapitest.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(true);

#region DI Configuration

// Add services to the DI container.
builder.Services.AddControllers();

// User Service DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRepository>();

// Artifact Generation DI
builder.Services.AddScoped<ArtifactGenerationBusiness>();
builder.Services.AddScoped<IArtifactGenerationRepository, ArtifactGenerationRepository>();

// Fragment DI
builder.Services.AddScoped<IFragmentRepository, FragmentRepository>();

// ChatLLM DI
builder.Services.AddScoped<LLMChatRepository>();

#endregion

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

#region Authentication and Authorization Config

// Add Authentication middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "my-secret-issuer", // Replace with your actual issuer
            ValidAudience = "my-secret-issuer", // Use the same as issuer if you don't have a separate audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hello-there-friend-this-is-the-key"))
        };
    });

// Add authorization services and policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database Configuration

// Access connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Configure DbContext for dependency injection
builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString)));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Allowing cross-origin requests in development environment
if (app.Environment.IsDevelopment())
    app.UseCors(builder => builder
        .AllowAnyOrigin() // You can also restrict origins if needed
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
else // Configure CORS for production environment
    app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

// custom request logging middleware
app.UseMiddleware<RequestLoggerMiddleware>();
app.UseMiddleware<UserInfo>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();