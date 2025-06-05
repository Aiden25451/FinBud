using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPIApplication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.Runtime;
using backend.Repositories;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";


bool isDeveloperMode = args.Contains("--developer");

if (isDeveloperMode)
{
  Console.WriteLine("Developer mode enabled!");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.Authority = domain;
  options.Audience = builder.Configuration["Auth0:Audience"];
  options.TokenValidationParameters = new TokenValidationParameters
  {
    NameClaimType = ClaimTypes.NameIdentifier
  };
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost",
      policy =>
      {
        policy.WithOrigins("http://localhost:3000")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "JWTToken_Auth_API",
    Version = "v1"
  });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddSingleton<IAmazonDynamoDB>(_ =>
{
  if (isDeveloperMode)
  {

    var fakeCreds = new BasicAWSCredentials("fake", "fake");
    var config = new AmazonDynamoDBConfig
    {
      RegionEndpoint = RegionEndpoint.USEast1,
      ServiceURL = "http://localhost:8000",
      UseHttp = true
    };

    return new AmazonDynamoDBClient(fakeCreds, config);
  }
  else
  {
    return new AmazonDynamoDBClient(RegionEndpoint.USEast1);
  }
}); builder.Services.AddSingleton<IClientRepository>(provider =>
    new ClientRepository(provider.GetRequiredService<IAmazonDynamoDB>(),
        config.GetValue<string>("Database:TableName")));
builder.Services.AddSingleton<IClientService, ClientService>();

var app = builder.Build();

app.UseCors("AllowLocalhost");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
