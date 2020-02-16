using System;
using System.Collections.Generic;
using System.Text;

namespace DavidoffBot.Models
{
    public class BotMessageUser
    {
        public BotMessage Message { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public int MessageId { get; set; }
    }
}
