using System;
using System.Collections.Generic;

namespace DavidoffBot.Models
{
    public class BotMessage
    {
        public BotMessage()
        {
            BotMessageKeywords = new List<BotMessageKeyword>();
            BotMessageUsers = new List<BotMessageUser>();
        }

        public int Id { get; set; }

        public List<BotMessageUser> BotMessageUsers { get; set; }

        public List<BotMessageKeyword> BotMessageKeywords { get; set; }

        public string Message { get; set; }
    }
}