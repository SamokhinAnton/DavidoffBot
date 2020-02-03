using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DavidoffBot.Interface;
using DavidoffBot.Models;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace DavidoffBot.Repository
{
    public class LiteDbRepository : IBaseRepository
    {
        private readonly LiteDatabase _db;
        private readonly ILogger _logger;

        public LiteDbRepository(LiteDbContext context, ILogger<LiteRepository> logger)
        {
            _db = context.Database;
            _logger = logger;
        }

        public Task<string> Get(string keyword)
        {
            try
            {
                var messages = _db.GetCollection<BotMessage>("messages");

                var user1 = new User {Id = Guid.NewGuid(), FirstName = "Гриша"};
                var user2 = new User {Id = Guid.NewGuid(), FirstName = "Кирюша"};

                var keys = new List<string> {"один", "два", "три"};

                var message = new BotMessage
                {
                    Id = Guid.NewGuid(),
                    Keywords = keys,
                    Users = new List<User> {user1, user2},
                    Message = "сообщение"
                };
                var users = _db.GetCollection<User>("users");
                users.Insert(new List<User> { user1, user2 });
                messages.Insert(message);
                messages = _db.GetCollection<BotMessage>("messages");
                return Task.FromResult(messages.FindAll().FirstOrDefault()?.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }
        }
    }
}