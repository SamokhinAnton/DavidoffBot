using System;
using System.Collections.Generic;
using System.Text;

namespace DavidoffBot.Models
{
    public class BotMessageKeyword
    {
        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }

        public int BotMessageId { get; set; }
        public BotMessage BotMessage { get; set; }

    }
}
