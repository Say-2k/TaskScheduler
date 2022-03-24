using Microsoft.EntityFrameworkCore;
using TaskScheduler.Data.EntityFramework;
using Common.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using TaskScheduler.Services.Interfaces;
using TaskScheduler.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITaskSchedulerService, TaskSchedulerService>();

builder.Services.AddDbContext<ApplicationContext>(options => options
    .UseNpgsql(Common.Configuration.ConfigurationManager.AppSettings["connectionString"]));

builder.Services.AddAuthentication();

builder.Services.AddCors();

builder.Services.AddMemoryCache();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.CustomSchemaIds(type => type.ToString());

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TaskScheduler API",
        Description = "ASP.NET Core Web API for TaskScheduler API"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
                        new List<string>()
                    }
                });

    var xmlCommentsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskScheduler.xml");
    c.IncludeXmlComments(xmlCommentsPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var pathBase = Common.Configuration.ConfigurationManager.AppSettings["pathBase"];
app.UsePathBase(pathBase);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(url: $"{pathBase}/swagger/v1/swagger.json", name: "PluginReporting API v1");
});

app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(x => x
    .MapControllers()
);

app.Run();
