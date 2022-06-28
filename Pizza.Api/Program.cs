using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pizza.Api;
using Pizza.Api.Helpers;
using Pizza.Bll.Interfaces;
using Pizza.Bll.Services;
using Pizza.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddDbContext<PizzaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44375")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddScoped<IProductService, ProductService>();
services.AddScoped<ICategoryService, CategoryService>();

services.AddResponseCaching();
services.AddMemoryCache();

services.AddControllers();

services.AddEndpointsApiExplorer();

services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader =
        new HeaderApiVersionReader("X-API-Version");
});

services.AddVersionedApiExplorer(
    options => options.GroupNameFormat = "'v'VVV");
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

var app = builder.Build();

// Configure the HTTP request pipeline.
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    }
   );
}

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
    var migrations = dbContext.Database.GetMigrations().ToHashSet();
    if (dbContext.Database.GetAppliedMigrations().Any(a => !migrations.Contains(a)))
        throw new InvalidOperationException("The migration is already running on the database and has since been deleted from the project. Delete the database or fix the migrations and restart the application.");
    dbContext.Database.Migrate();
}

app.Run();
