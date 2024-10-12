using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Domain.Model;
using NotificationService.Infrastructure.MongoDB;

namespace NotificationService.Service.Services;

public class MongoService
{
    private readonly IMongoCollection<TotalPrice> _userCollection;
    private readonly IMongoCollection<BsonDocument> _adminCollection;

    public MongoService(MongoConfig config)
    {
        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);
        _userCollection = database.GetCollection<TotalPrice>(config.UserNotification);
        _adminCollection = database.GetCollection<BsonDocument>(config.AdminNotification);

        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexKeysDefinition = Builders<TotalPrice>.IndexKeys.Ascending(tp => tp.Id);
        var indexModel = new CreateIndexModel<TotalPrice>(indexKeysDefinition);
        _userCollection.Indexes.CreateOne(indexModel);
    }

    public async Task AddTotalPriceAsync(TotalPrice totalPrice)
    {
        await _userCollection.InsertOneAsync(totalPrice);
    }

    public async Task AddAdminNotificationAsync(string notification)
    {
        var document = new BsonDocument { { "Notification", notification } };
        await _adminCollection.InsertOneAsync(document);
    }

    public async Task<TotalPrice> UserNotificationsByIdAsync(string id)
    {
        var filter = Builders<TotalPrice>.Filter.Eq(tp => tp.Id, id);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<string>> GetAdminNotificationsAsync()
    {
        var documents = await _adminCollection.Find(new BsonDocument()).ToListAsync();
        return documents.Select(d => d["Notification"].AsString).ToList();
    }
}