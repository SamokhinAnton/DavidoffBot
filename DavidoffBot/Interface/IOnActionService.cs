using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace DavidoffBot.Interface
{
    public interface IOnActionService
    {
        void OnMessage(object sender, MessageEventArgs e);
    }
}
