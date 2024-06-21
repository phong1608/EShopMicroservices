using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("fixed", o =>
    {
        o.Window = TimeSpan.FromSeconds(10);
        o.PermitLimit = 5;
    });
});
var app = builder.Build();
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
