using DavidoffBot.Interface;
using Microsoft.Extensions.Logging;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace DavidoffBot.Services
{
    public class OnActionService : IOnActionService
    {
        private ITelegramBotClient _botClient;
        private ILogger _logger;
        private IBaseRepository _repository;

        public OnActionService(ILogger<OnActionService> logger, ITelegramBotClient botClient, IBaseRepository repository)
        {
            _botClient = botClient;
            _logger = logger;
            _repository = repository;
        }

        public async void OnMessage(object sender, MessageEventArgs e)
        {
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

        private async void OnTextMessage(MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                var parts = e.Message.Text.Split();
                foreach (var item in parts)
                {
                    var message = await _repository.Get(item);
                    _logger.LogInformation(e.Message.Text);
                    _logger.LogInformation(message);
                    
                    if (!string.IsNullOrEmpty(message))
                    {
                        await _botClient.
                            SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: message,
                          replyToMessageId: e.Message.MessageId
                        );
                        return;
                    }
                }

                //await botClient.SendStickerAsync(
                //  chatId: e.Message.Chat,
                //  sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
                //);
            }
        }
    }
}
