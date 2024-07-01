using Catalog.gRPC.Models;
using Catalog.gRPC.Services;
using Microsoft.EntityFrameworkCore;
using Marten;
var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
});
builder.Services.AddGrpc();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProductService>();

app.Run();
