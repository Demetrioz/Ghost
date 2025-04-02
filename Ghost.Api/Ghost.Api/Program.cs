using Akka.Hosting;
using Ghost.Api;
using Ghost.Api.Configurations;
using Ghost.Api.Core.Actors.Events;
using Ghost.Api.Services.Authentication;
using Ghost.Api.Services.X;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

//////////////////////////////////////////
//          Configure Settings          //
//////////////////////////////////////////

builder.Services.Configure<XSettings>(
    builder.Configuration.GetSection(Configuration.X));

builder.Services.Configure<AuthenticationSettings>(
    builder.Configuration.GetSection(Configuration.Authentication));

//////////////////////////////////////////
//               Akka.Net               //
//////////////////////////////////////////

builder.Services.AddAkka("Api", (akkaBuilder, provider) =>
{
    akkaBuilder.WithActors((system, registry, resolver) =>
    {
        var props = resolver.Props<EventManager>();
        var manager = system.ActorOf(props, nameof(EventManager));
        registry.Register<EventManager>(manager);
    });
});

//////////////////////////////////////////
//          Additional Services         //
//////////////////////////////////////////

builder.Services
    .AddHttpClient()
    .AddTransient<IXService, XService>()
    .AddTransient<IAuthenticationService, AuthenticationService>();

//////////////////////////////////////////
//            MVC Components            //
//////////////////////////////////////////

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//////////////////////////////////////////
//      Configure Request Pipeline      //
//////////////////////////////////////////

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();