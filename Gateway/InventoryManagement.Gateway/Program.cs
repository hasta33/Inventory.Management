using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();

#region CORS Settings
//builder.Services.AddCors(options => {
//    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("https://localhost:4200", "http://localhost:4200") //"https://localhost:4200", "http://localhost:4200"
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithMethods("Get", "Post", "Put", "Delete", "Options"));
});
#endregion

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);



#region Keycloak Server -> JWT Auth
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.Authority = builder.Configuration["KeycloakServerUrl"];
//    options.Audience = "InventoryManagementGateway";
//});
#endregion


ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
await app.UseOcelot();

app.Run();
