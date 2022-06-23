using Microsoft.EntityFrameworkCore;
using Pizza.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PizzaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
