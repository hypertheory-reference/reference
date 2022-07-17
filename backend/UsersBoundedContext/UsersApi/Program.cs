
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using UsersApi.Adapters;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<AuthApiAdapter>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
});

builder.Services.AddAuthorization(options =>
{

});
builder.Services.AddAuthentication(options =>
{
   
}).AddOpenIdConnect(options =>
{

    //Keycloak server
    options.Authority = "http://localhost:8080/";
    //Keycloak client ID
    options.ClientId = "users-api";
    //Keycloak client secret
    options.ClientSecret = "6m6a1pEgUllLbqmYLf00lE7ZKECMqqbj";
    //Keycloak .wellknown config origin to fetch config
    options.MetadataAddress = "http://localhost:8080/realms/hypertheory/.well-known/openid-configuration";
    //Require keycloak to use SSL
    options.RequireHttpsMetadata = false;
    //options.GetClaimsFromUserInfoEndpoint = true;
    //options.Scope.Add("openid");
    //options.Scope.Add("profile");
    //Save the token
   // options.SaveTokens = true;
    //Token response type, will sometimes need to be changed to IdToken, depending on config.
    //options.ResponseType = OpenIdConnectResponseType.Code;
    ////SameSite is needed for Chrome/Firefox, as they will give http error 500 back, if not set to unspecified.
    //options.NonceCookie.SameSite = SameSiteMode.Unspecified;
    //options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;

    //options.TokenValidationParameters = new TokenValidationParameters
    //{
    //    NameClaimType = "name",
    //    RoleClaimType = ClaimTypes.Role,
    //    ValidateIssuer = true
    //};

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
