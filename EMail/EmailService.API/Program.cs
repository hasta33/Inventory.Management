using Email.API.Consumers;
using Email.API.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, EmailService>();



#region RabbitMQ connection conf.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<InventoryEmbezzledConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:Username"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });


        cfg.ReceiveEndpoint("inventory-embezzled", e =>
        {
            e.ConfigureConsumer<InventoryEmbezzledConsumer>(context);
        });
    });
});
//builder.Services.AddMassTransitHostedService();
#endregion




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
