using Microsoft.EntityFrameworkCore;
using UserShared.Lib.Models;
using UserWrite.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserWriteDB")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// SEED DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDBContext>();
    db.Database.EnsureCreated(); // Ensures DB and tables exist
    List<User> seededUsers = UserSeeder.Seed(db);

    ProfileSeeder.Seed(db, seededUsers);
}

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
