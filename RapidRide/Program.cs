using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RapidRide;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//new
var key = Encoding.ASCII.GetBytes("your secret key here");

// Add services to the container.
builder.Services.AddScoped<RapidRide.Service.AuthService>();
builder.Services.AddScoped<RapidRide.Service.RechargeCardService>();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RapidRideDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("defultConnectionString")));
//new
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your issuer",
            ValidAudience = "your audience",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var webRootPath = builder.Environment.WebRootPath;
if (webRootPath == null)
{
    throw new Exception("Web root path is null.");
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(webRootPath, "ProfilePictures")),
    RequestPath = "/ProfilePictures"
});


app.UseHttpsRedirection();
//app.UseCors("AllowAllOrigins");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
