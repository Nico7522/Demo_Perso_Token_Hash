using demoToken.API.Infrastructure;
using demoToken.DAL.Interfaces;
using demoToken.DAL.Repositories;
using DemoToken.BLL.Interfaces;
using DemoToken.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    //builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    //{
    //    //builder.WithOrigins("http://localhost:4200");

    //    builder.AllowAnyOrigin()
    //           .AllowAnyMethod()
    //           .AllowCredentials();
    //}));
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoToken", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Description",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });


    OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Scheme = "oAuth2",
        Name = "Bearer",
        In = ParameterLocation.Header
    };

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        [openApiSecurityScheme] = new List<string>()
    });
    
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var tokenManager = builder.Services.BuildServiceProvider().GetService<TokenManager>();
    options.TokenValidationParameters = new TokenValidationParameters() { 
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManager.secret)),
        ValidateIssuer = true,
        ValidIssuer = tokenManager.issuer,
        ValidateAudience = true,
        ValidAudience = tokenManager.audience,
    };
});

builder.Services.AddSingleton(sp => new Connection(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<TokenManager>();

builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

// Add services to the container.

builder.Services.AddControllers();
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
app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
