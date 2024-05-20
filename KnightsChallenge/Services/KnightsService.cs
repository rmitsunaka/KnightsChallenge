using MongoDB.Driver;
using KnightsChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightsChallenge.Services
{
    public interface IKnightService
    {
        Task<List<Knight>> GetAsync();
        Task<Knight> GetAsync(string id);
        Task CreateAsync(Knight knight);
        Task UpdateAsync(string id, Knight knight);
        Task RemoveAsync(string id);
    }

    public class KnightService : IKnightService
    {
        private readonly IMongoCollection<Knight> _knights;

        public KnightService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _knights = database.GetCollection<Knight>(settings.KnightsCollectionName);
        }

        public async Task<List<Knight>> GetAsync() =>
            await _knights.Find(k => true).ToListAsync();

        public async Task<Knight> GetAsync(string id) =>
            await _knights.Find<Knight>(k => k.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Knight knight) =>
            await _knights.InsertOneAsync(knight);

        public async Task UpdateAsync(string id, Knight knight) =>
            await _knights.ReplaceOneAsync(k => k.Id == id, knight);

        public async Task RemoveAsync(string id) =>
            await _knights.DeleteOneAsync(k => k.Id == id);
    }
}
