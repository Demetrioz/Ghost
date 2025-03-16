using Ghost.Api;
using Ghost.Api.Services.X;

var builder = WebApplication.CreateBuilder(args);

//////////////////////////////////////////
//          Configure Settings          //
//////////////////////////////////////////

builder.Services.Configure<XSettings>(
    builder.Configuration.GetSection(Configuration.X));

//////////////////////////////////////////
//               Akka.Net               //
//////////////////////////////////////////



//////////////////////////////////////////
//          Additional Services         //
//////////////////////////////////////////

builder.Services
    .AddHttpClient()
    .AddTransient<IXService, XService>();

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