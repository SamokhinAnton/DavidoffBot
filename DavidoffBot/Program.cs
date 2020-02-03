using DavidoffBot.Interface;
using DavidoffBot.Repository;
using DavidoffBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.IO;
using System.Threading;
using DavidoffBot.Models;
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

            //setup our DI
            _serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddTransient<IOnActionService, OnActionService>()
                .AddTransient<IBaseRepository, LiteDbRepository>()
                .AddSingleton<LiteDbContext>()
                .AddSingleton(_configuration)
                .AddSingleton<ITelegramBotClient>(new TelegramBotClient("990656579:AAHUdw-Vk9DKWmpPXMWqQZ-xGOED5jc361I"))
                .BuildServiceProvider();


            _logger = _serviceProvider.GetService<ILogger<Program>>();
            _botClient = _serviceProvider.GetService<ITelegramBotClient>();
            _actionService = _serviceProvider.GetService<IOnActionService>();
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
