var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins(
                            "mobiliario-frontend-qthrev0cr-zurikelis-projects.vercel.app" // La puedes poner aunque no exista aún
                         )
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowReact");

app.MapControllers();

app.Run();