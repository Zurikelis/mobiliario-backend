var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins(
                            "https://mobiliario-frontend.vercel.app/"
                         )
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowReact");

app.MapControllers();

app.Run();