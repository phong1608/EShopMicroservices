using MassTransit;
using Ordering.API;
using Ordering.Application;
using Ordering.Application.Orders.EventHandlers.Intergration;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutEventHandler>();
    

    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), host =>
        {
            host.Username(builder.Configuration["MessageBroker:UserName"]);
            host.Password(builder.Configuration["MessageBroker:Password"]);
        });
        configurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddApplicationServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices(builder.Configuration);





var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.Run();
