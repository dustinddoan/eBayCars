﻿using System.Text.Json;
using MongoDB.Entities;

namespace SearchService;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoDB.Driver.MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection"))
        );

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

        var items = await httpClient.GetItemsForSearchDB();

        Console.WriteLine("DUSTIN items: " + items.Count);

        if (items.Count > 0) await DB.SaveAsync(items);

        // if (count == 0)
        // {
        //     Console.WriteLine("No DATA - will attempt to seed");
        //     var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "auctions.json");

        //     var itemData = await File.ReadAllTextAsync(path);

        //     var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

        //     var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

        //     await DB.SaveAsync(items);
        // }
    }
}
