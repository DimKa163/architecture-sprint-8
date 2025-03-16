using Api.Endpoints;
using Api.Extensions;
using Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.AddCors(op =>
{
    op.AddPolicy("web-app", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"];
        options.TokenValidationParameters = new()
        {
            ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
            ValidateAudience = false
        };
    });
    
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

app.UseCors("web-app");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<RoleMiddleware>();
app.UseAuthorization();
app.MapReports();
app.MapUsers();
app.Run();