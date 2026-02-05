var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins(
                            "http://localhost:3000", // Tu URL local actual
                            "https://tu-proxima-url-de-vercel.vercel.app" // La puedes poner aunque no exista aún
                         )
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowReact");

app.MapControllers();

app.Run();