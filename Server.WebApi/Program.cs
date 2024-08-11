using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Business.Abstract;
using Server.Business.Concrete;
using Server.DataAccess.Abstract;
using Server.DataAccess.Concrete.EntityFramework;
using Server.DataAccess.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
        throw new InvalidOperationException("DB Connection not found!"));
});

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

//Data Object Layer
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<ISystemRoleDal, EfSystemRoleDal>();
builder.Services.AddScoped<IUserRoleDal, EfUserRoleDal>();

//Services
builder.Services.AddScoped<IUserService, UserManager>();




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

app.Run();
