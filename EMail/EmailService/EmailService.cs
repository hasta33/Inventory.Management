/*namespace EmailService
{
    public class EmailService
    {
        
    }
}
*/

using MassTransit;
using Newtonsoft.Json;
using SharedModels;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.ReceiveEndpoint("inventory-created-event", e =>
    {
        e.Consumer<InventoryCreatedConsumer>();
    });

});

await busControl.StartAsync(new CancellationToken());

try
{
    Console.WriteLine("Devam etmek için basın");

    await Task.Run(() => Console.ReadLine());
}
finally
{
    await busControl.StopAsync();
}

class InventoryCreatedConsumer : IConsumer<CreatedInventory>
{
    public async Task Consume(ConsumeContext<CreatedInventory> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"Envanter oluşturuldu : {jsonMessage}");
    }
}


