using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using APIAuthTest.Services.BoxServices;
using APIAuthTest.Services.AuthServices;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IBoxService, BoxService>();
    builder.Services.AddSingleton<IAuthService, AuthService>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;
        
        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes("changemechangemechangemechangeme");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "APIAuthTest",   // move this to .env
            ValidAudience = "APIAuthTest?",   // move this to .env
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

    builder.Services.AddAuthorization();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
