using System.Collections.Generic;
using System.Linq;
using DavidoffBot.Interface;
using DavidoffBot.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DavidoffBot.Repository
{
    public class MessageRepository : IBaseRepository
    {
        private readonly BotContext _db;
        public MessageRepository(BotContext db)
        {
            _db = db;
        }

        public async Task<string> Get(string keyword)
        {
            var message = new BotMessage
            {
                BotMessageKeywords = new List<BotMessageKeyword>(),
                BotMessageUsers = new List<BotMessageUser>(),
                Message = "message"
            };
            var user = new User() { FirstName = "first", Nickname = "nick", LastName = "last" };
            var keyword1 = new Keyword() { Text = "help" };
            var botMessageKeyword = new BotMessageKeyword() { BotMessage = message, Keyword = keyword1 };
            var botMessageUser = new BotMessageUser() { Message = message, User = user };
            message.BotMessageKeywords.Add(botMessageKeyword);
            message.BotMessageUsers.Add(botMessageUser);
            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();

            var message1 = await _db.Messages
                .Include(m => m.BotMessageUsers)
                .ThenInclude(mu => mu.User)
                .Include(m => m.BotMessageKeywords)
                .ThenInclude(mk => mk.Keyword)
                .SingleOrDefaultAsync(m => m.Id == 1);
            return "yes";
        }

        public async Task Add(string message, IEnumerable<string> keywords, IEnumerable<User> users)
        {

        }
    }
}
