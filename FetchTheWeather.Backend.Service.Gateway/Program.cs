var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(corsBuilder =>
        {
            corsBuilder.WithOrigins("http://localhost:3000")
                .WithHeaders("Content-Type", "Authorization", "Accept", "Origin", "X-Requested-With",
                    "X-SignalR-User-Agent")
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                .AllowCredentials();
        });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(corsBuilder =>
        {
            corsBuilder.WithOrigins("https://pietr.space", "https://ftw.pietr.space")
                .WithHeaders("Content-Type", "Authorization", "Accept", "Origin", "X-Requested-With",
                    "X-SignalR-User-Agent")
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                .AllowCredentials();
        });
    });
}

var app = builder.Build();

app.UseCors();
app.MapReverseProxy();

app.Run();