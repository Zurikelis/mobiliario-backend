var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("https://mobiliario-frontend.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              // .AllowCredentials() // descomenta sólo si necesitas cookies/credenciales
    );
});

var app = builder.Build();

app.UseRouting();            // <- importante
app.UseCors("AllowReact");   // <- aplicar CORS antes de MapControllers
app.UseAuthorization();

app.MapControllers();

app.Run();