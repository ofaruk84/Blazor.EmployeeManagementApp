using Microsoft.EntityFrameworkCore;
using Server.Business.Abstract;
using Server.Business.Concrete;
using Server.Business.Security.JWT;
using Server.DataAccess.Abstract;
using Server.DataAccess.Concrete.EntityFramework;
using Server.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
var tokenOptions = builder.Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = tokenOptions.Issuer!,
        ValidAudience = tokenOptions.Audience!,
    };
});
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
        throw new InvalidOperationException("DB Connection not found!"));
});



//Data Object Layer
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<ISystemRoleDal, EfSystemRoleDal>();
builder.Services.AddScoped<IUserRoleDal, EfUserRoleDal>();
builder.Services.AddScoped<IRefreshTokenDal, EfRefreshTokenDal>();

//Services
builder.Services.AddScoped<IUserService, UserManager>();


//utilities
builder.Services.AddSingleton<JWTHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
