using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersistenceLayer.Base.NoSQLs.UnitOfWork
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database;
        private MongoClient _mongoClient;
        private readonly List<Func<Task>> _commands;
        private IClientSessionHandle _session;

        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _commands = new List<Func<Task>>();
            _configuration = configuration;

            ConnectToMongo();
        }

        private void ConnectToMongo()
        {
            if (_mongoClient != null)
                return;

            _mongoClient = new MongoClient(_configuration.GetSection("MongoSettings").GetSection("Connection").Value);
            _database = _mongoClient.GetDatabase(_configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
        }

        public void AddCommand(Func<Task> func) => _commands.Add(func);

        public async Task<int> CommitChanges()
        {
            using (_session = await _mongoClient.StartSessionAsync().ConfigureAwait(false))
            {
                _session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks).ConfigureAwait(false);

                await _session.CommitTransactionAsync().ConfigureAwait(false);
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
