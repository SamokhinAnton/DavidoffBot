using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DavidoffBot.Models
{
    public class LiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(
            IConfiguration configuration, 
            ILogger<LiteDatabase> logger)
        {
            logger.LogInformation("Start context");

            //var mapper = new BsonMapper();

            //mapper.Entity<BotMessage>()
            //    .DbRef(x => x.Users, "users");
            var startPath = Environment.CurrentDirectory.Split("bin").First();
            var endPath = configuration.GetConnectionString("LightDbStorage");
            Database = new LiteDatabase(string.Format($"{startPath}{endPath}"));

            var mapper = BsonMapper.Global;

            mapper.Entity<BotMessage>()
                .Id(x => x.Id)
                .DbRef(x => x.Users, "_users")
                .Field(x => x.Keywords, "_keywords");
        }
    }
}
