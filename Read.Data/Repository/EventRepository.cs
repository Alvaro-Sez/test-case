using MongoDB.Driver;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Read.Data.Models;
using Read.Data.Models.Utils;

namespace Read.Data.Repository;

public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<EventModel> _collection;
    
    public EventRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<EventModel>("events");
    }
    public async Task<IEnumerable<EventRecord>> GetByLockId(Guid id)
    {
        var events = await _collection.FindAsync(c=> c.LockId == id);
        return Map.ToDomain(await events.ToListAsync());
    }

    public async Task<IEnumerable<EventRecord>> GetByUserId(Guid id)
    {
        var events = await _collection.FindAsync(c=> c.UserId== id);
        return Map.ToDomain(await events.ToListAsync());
    }

    public async Task SetAsync(EventRecord entity)
    {
        await _collection.InsertOneAsync(Map.ToModel(entity));
    }
}