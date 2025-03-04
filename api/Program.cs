using api.Data;
using api.Repositories;
using api.Services;
using api.Services.Passwords;
using api.Data;


using api.Services;
using api.Services.Passwords;
using api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using api.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssql")));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//app.UseCors(options => options
//    .WithOrigins("http://localhost:3000")
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    .AllowAnyOrigin()
//);

app.UseCors(options => options
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            );

app.UseAuthorization();

app.MapControllers();



app.Run();






/////////////////////
//using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
//{
//    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
//}
