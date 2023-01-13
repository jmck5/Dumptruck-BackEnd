using Dumptruck_v4;
using Dumptruck_v4.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Pomelo.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dumptruck_v4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Inject my stuff
builder.Services.AddScoped<TokenService>(); // This should really have an interface

///////////////////////

//////////////



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string someConnection = "Server=localhost; UserId=root; password=secret-pw; database=skeletontest";

builder.Services.AddDbContext<ScoobyContext>(options => options.UseMySql(connectionString: someConnection, serverVersion: ServerVersion.AutoDetect(someConnection)));
// Note adding the entityFrameworkStores is important
builder.Services.AddIdentity<ScoobyUser, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ScoobyContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters() {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // In demo this was false. Why?
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
string scoobyCorsPolicy = "AllowStuff";
builder.Services.AddCors(op => op.AddPolicy(name: scoobyCorsPolicy, policy => { policy.AllowAnyOrigin().AllowAnyHeader(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(scoobyCorsPolicy);
app.MapControllers();

app.Run();


