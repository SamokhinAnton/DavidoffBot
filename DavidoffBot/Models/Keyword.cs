using System;
using System.Collections.Generic;
using System.Text;

namespace DavidoffBot.Models
{
    public class Keyword
    {
        public Keyword()
        {
            BotMessageKeywords = new List<BotMessageKeyword>();
        }
        public int Id { get; set; }
        public string Text { get; set; }

        public ICollection<BotMessageKeyword> BotMessageKeywords { get; set; }
    }
}
