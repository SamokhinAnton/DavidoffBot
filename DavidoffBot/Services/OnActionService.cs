using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DavidoffBot.Interface;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DavidoffBot.Services
{
    public class OnActionService : IOnActionService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger _logger;
        private readonly IBaseRepository _repository;

        public OnActionService(ILogger<OnActionService> logger, ITelegramBotClient botClient,
            IBaseRepository repository)
        {
            _botClient = botClient;
            _logger = logger;
            _repository = repository;
        }

        public async void OnMessage(object sender, MessageEventArgs e)
        {
            var members = await _botClient.GetChatAsync(e.Message.Chat.Id);
            //var stickers = _botClient.GetStickerSetAsync();
            switch (e.Message.Type)
            {
                case MessageType.Text:
                    OnTextMessage(e);
                    return;
                case MessageType.Sticker:
                    return;
                case MessageType.Photo:
                    return;
                case MessageType.Video:
                    return;
                default:
                    return;
            }
        }

        private async Task OnTextMessage(MessageEventArgs e)
        {
            var message = e.Message.Text;
            if (e.Message.Text != null)
            {
                var isCommand = message.StartsWith("/");
                if (isCommand)
                {
                    await OnCommandAsync(message, e.Message.Entities.Where(entity => entity.User != null).Select(entity => entity.User));
                }

                //var parts = e.Message.Text.Split();
                //foreach (var item in parts)
                //{
                //    var message = await _repository.Get(item);
                //    _logger.LogInformation(e.Message.Text);
                //    _logger.LogInformation(message);

                //    if (!string.IsNullOrEmpty(message))
                //    {
                //        //await _botClient.SendTextMessageAsync(
                //        //    e.Message.Chat,
                //        //    message,
                //        //    replyToMessageId: e.Message.MessageId
                //        //);
                //        return;
                //    }
                //}

                //await botClient.SendStickerAsync(
                //  chatId: e.Message.Chat,
                //  sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
                //);
            }
        }

        private async Task OnCommandAsync(string message, IEnumerable<User> users)
        {
            if (string.Equals(message, "/add"))
            {
                var messageRegEx = new Regex("message:([^&]*)users:");
                var usersRegEx = new Regex("users:([^&]*)keywords:");
                var keywordsRegEx = new Regex("keywords:([^&]*)");

                var messageText = messageRegEx.Match(message).Groups[1].Value.Trim();
                

            } else if (string.Equals(message, "/remove"))
            {

            }
        }
    }
}