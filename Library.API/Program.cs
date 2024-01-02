using Library.API.Extensions;
using Library.API.Middlewares;
using Library.Core.Utilities;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling
        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();;
builder.Services.AddDbContextExtension(builder.Configuration, builder.Environment);
builder.Services.AddServicesExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddAuthenticationExtension(builder.Configuration, builder.Environment);
builder.Services.AddAuthorizationExtension();
builder.Services.AddAutoMapper(typeof(LibraryProfile));


var app = builder.Build();

builder.Logging.AddConsole();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction => { setupAction.SwaggerEndpoint("/swagger/LibraryAPI/swagger.json", "Library API"); });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
