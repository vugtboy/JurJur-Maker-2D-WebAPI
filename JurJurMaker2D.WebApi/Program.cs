using JurJurMaker2D.WebApi;
using JurJurMaker2D.WebApi.Interfaces;
using JurJurMaker2D.WebApi.Repositories;
using JurJurMaker2D.WebApi.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton(TimeProvider.System);
var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddTransient<IEnvironment2DRepository, Environment2DRepository>(o => new Environment2DRepository(sqlConnectionString));
builder.Services.AddTransient<IObject2DRepository, Object2DRepository>(o => new Object2DRepository(sqlConnectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();
//voor authorisatie
builder.Services.AddAuthorization();
builder.Services
.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
})
.AddDapperStores(options =>
{
    options.ConnectionString = sqlConnectionString;
});

builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(value: 60);
    });
var app = builder.Build();
app.UseAuthorization();
app.MapGroup(prefix: "/account")
    .MapIdentityApi<IdentityUser>();


app.MapPost(pattern: "account/logout",
    async (SignInManager<IdentityUser> singInManager,
    [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await singInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();

app.MapControllers().RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello world, the API is up ");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
