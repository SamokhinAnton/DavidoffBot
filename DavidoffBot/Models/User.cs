using System;
using System.Collections.Generic;
using System.Text;

namespace DavidoffBot.Models
{
    public class User
    {
        public User()
        {
            BotMessageUsers = new List<BotMessageUser>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string TelegramId { get; set; }

        public List<BotMessageUser> BotMessageUsers { get; set; }
    }
}
