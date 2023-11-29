using Contracts;
using MassTransit;

namespace AuctionService;

// prevent inconsitent DB between Auction and Search service
// will pulish change message model as FooBar so that Search consumer will not throw Exception
public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        Console.WriteLine("--> Consuming faulty creating");

        var exception = context.Message.Exceptions.First();

        Console.WriteLine("--> exception: " + exception);

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "FooBar";
            await context.Publish(context.Message.Message);
        } else {
            Console.WriteLine("Not an argument exception - updating error somewhere...");
        }
    }
}
