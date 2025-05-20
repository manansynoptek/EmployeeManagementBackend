using ElmahCore.Mvc;
using Employee.BusinessLogic.BusinessLogic;
using Employee.BusinessLogic.IBusinessLogic;
using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Employee.Data.Repositories;
using Employee.Model.Models;
using Employee.WebApi.Helpers;
using Employee.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
var connectionString = configuration.GetConnectionString("EmployeeConnection");

builder.Services.AddHealthChecks();
builder.Services.AddDbContext<EmployeeManagementContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeBusinessLogic, EmployeeBusinessLogic>();
builder.Services.AddScoped<IDepartmentBusinessLogic, DepartmentBusinessLogic>();
builder.Services.AddScoped<IJwtUtilsLogic, JwtUtilsLogic>();

builder.Services.AddElmah<SqlErrorLog>(options =>
{
    options.Path = @"elmah";
    options.ConnectionString = configuration.GetConnectionString("EmployeeConnection");
    options.LogPath = "~/Logs/errors.xml";
});

builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(c =>
    {
        c.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management",
        Version = "v1",
        Description = "Employee Management",
    });

    //Jwt bearer token authentication in swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
          },
          new List<string>()}
    });
});

// For JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var app = builder.Build();

app.MapHealthChecks("/healthz");

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseElmah();
app.UseCors();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
