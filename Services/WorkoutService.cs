using NetMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NetMongoDb.Services;

public class WorkoutService
{
    private readonly IMongoCollection<Workouts> _workoutCollection;

    public WorkoutService(IOptions<DatabaseSetting> workoutDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            workoutDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            workoutDatabaseSettings.Value.DatabaseName);

        _workoutCollection = mongoDatabase.GetCollection<Workouts>(
            workoutDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<Workouts>> GetAsync() =>
        await _workoutCollection.Find(_ => true).ToListAsync();

    public async Task<Workouts?> GetAsync(string id) =>
        await _workoutCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Workouts newWorkout) =>
        await _workoutCollection.InsertOneAsync(newWorkout);

    public async Task UpdateAsync(string id, Workouts updateWorkout) =>
        await _workoutCollection.ReplaceOneAsync(x => x.Id == id, updateWorkout);

    public async Task RemoveAsync(string id) =>
        await _workoutCollection.DeleteOneAsync(x => x.Id == id);
}

