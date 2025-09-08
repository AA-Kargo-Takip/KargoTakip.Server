using KargoTakip.Server.WebAPI;
using KargoTakip.Server.WebAPI.Installers;
using KargoTakip.Server.WebAPI.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer();
builder.Services.AddAuthorization();
builder.AddServiceDefaults();

builder.Services.AddInternalServices(builder.Configuration);

builder.Services.AddExternalServices();


var app = builder.Build();


app.MapOpenApi();

app.MapScalarApiReference();

app.MapDefaultEndpoints();

app.AddMiddlewares();

app.MapControllers().RequireRateLimiting("fixed").RequireAuthorization();

app.RegisterRoutes();
ExtensionsMiddleware.CreateFirstUser(app);

app.Run();