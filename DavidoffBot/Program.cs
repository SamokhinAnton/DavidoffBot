using DavidoffBot.Interface;
using DavidoffBot.Repository;
using DavidoffBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using DavidoffBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace DavidoffBot
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;
        private static ILogger _logger;
        private static ITelegramBotClient _botClient;
        private static IOnActionService _actionService;
        private static IConfiguration _configuration;

        static Program()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = Enum.Parse<LogLevel>(_configuration.GetSection("Logging:LogLevel:MinLevel").Value, true))
                .AddTransient<IOnActionService, OnActionService>()
                .AddTransient<IBaseRepository, MessageRepository>()
                .AddDbContext<BotContext>(opt =>
                {
                    if (opt.IsConfigured) return;
                    var startPath = Environment.CurrentDirectory.Split("bin").First();
                    var endPath = _configuration.GetConnectionString("SQLDbStorage");
                    Console.WriteLine("program" + startPath);
                    Console.WriteLine("program" + endPath);
                    opt.UseSqlite($"Data Source={startPath}\\{endPath}");
                })
                .AddSingleton(_configuration)
                .AddSingleton<ITelegramBotClient>(new TelegramBotClient(_configuration.GetSection("ConnectionTelegram:Token").Value))
                .BuildServiceProvider();

            _logger = _serviceProvider.GetService<ILogger<Program>>();
            _botClient = _serviceProvider.GetService<ITelegramBotClient>();
            _actionService = _serviceProvider.GetService<IOnActionService>();
            var context = _serviceProvider.GetService<BotContext>();
            context.Database.Migrate();
        }

        static void Main(string[] args)
        {
            //_botClient = new TelegramBotClient("990656579:AAHUdw-Vk9DKWmpPXMWqQZ-xGOED5jc361I");

            //var actionService = new OnActionService(_botClient);
            //var me = botClient.GetMeAsync().Result;
            //Console.WriteLine(
            //  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            //);
            _logger.LogInformation("Понеслась моча по трубам!");
            _botClient.OnMessage += _actionService.OnMessage;
            _botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
    }
}
