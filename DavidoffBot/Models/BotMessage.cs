using System;
using System.Collections.Generic;

namespace DavidoffBot.Models
{
    public class BotMessage
    {
        public Guid Id { get; set; }

        public List<User> Users { get; set; }

        public List<string> Keywords { get; set; }

        public string Message { get; set; }
    }
}