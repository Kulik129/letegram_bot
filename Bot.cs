public struct  Bot
{
    static string token;
    static string baseUri;

    static HttpClient hc = new HttpClient();

    public static void Start()
    {
        int  offset = 0;
        while (true)
        {
            string url =  $"{baseUri}getUpdates?offset={offset}";
            string json = hc.GetStringAsync(url).Result;
            //  System.Console.WriteLine(json);

            JsonParse.Init(json);
            List<ModelMessage> msgs = JsonParse.Parse();

            foreach (ModelMessage msg in msgs)
            {
                
                System.Console.WriteLine(msg);
                offset = (Int32.Parse(msg.UpdateId) +1);
                Repository.Append(msg);
                string uid = msg.UserId;
                string InSMS = msg.MessageText;

                
               
                if (InSMS == "/start")
                {
                    SendMessage(uid, "Ну привет!🐓\nКожанный ублюдок, мать твою!", msg.MessageId);
                    SendMessage(uid, $"Я буду звать тебя Сэр!\nА не {msg.FirstName}\nПотому, что Сэр мне нравиться больше чем {msg.FirstName}", msg.MessageId);
                    SendMessage(uid, $"Нажми /Da если хочешь, чтоб я тебе подобрал фильм", msg.MessageId);
                }

                if (InSMS == "/Da")
                {
                    SendMessage(uid, $"Хочешь комедию?", msg.MessageId);
                    
                }
                if (InSMS == "/help")
                {
                    SendMessage(uid, $"Что-то не то ты пишешь\nЕсли не знаешь как нормально общаться, нажми /help", msg.MessageId);
                }



                Thread.Sleep(200);
                 

            }
            Repository.Save();
            // break;
            Thread.Sleep(2000);
        }
    }

    public static void Init(string publicToken)
    {
        token = publicToken;
        baseUri = "https://api.telegram.org/bot5583734191:AAE1GvqctoeO8RCCaGjiayIQrhMYx1ZukAY/";
    }
    
    public static void SendMessage(string id, string text, string replyToMessageId = "")
    {
        string url = $"{baseUri}sendMessage?chat_id={id}&text={text}&";
        var req = hc.GetStringAsync(url).Result;
    }

    // public static InlineKeyboardMarkup TestInlineKeyboard { get; } = new InlineKeyboardMarkup           
    // {
    //     InlineKeyboard = new []{new[] {new InlineKeyboardButton("Text1","Data1"), new InlineKeyboardButton("text1","data2")} }
    // };

    // public static void SendMessageStic(string id, string text, string sticker = "")
    // {
    //     string stic = "CAACAgIAAxkBAAEFIoRiuZ7CM4-Vi97Y0pa8EmXGbbb7UwACjAMAAsSraAt1V-wZ0Rp2sSkE";
    //     string url = $"{baseUri}sendMessage?chat_id={id}&text={text}&sticker{stic}";
    //     var req = hc.GetStringAsync(url).Result;
    // }

    // public static  Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup Category = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup
    // {
    //     InlineKeyboard = new[]
    //     {
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🐓 Птица ", "call_category ptiza 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍞 Хлеб ", "call_category hleb 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍗 Мясо ", "call_category myaso 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🥛 Молоко ", "call_category moloko 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🧀 Молочка ", "call_category moloko-i-syr 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🐟 Рыба ", "call_category ryba 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍯 Мёд/варенье ", "call_category varene-mjod 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍖 Мясные изделия ", "call_category mjasnye-izdelija 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍌 Фрукты и овощи ", "call_category frukty-i-ovoshhi 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍲 Полуфабрикаты ", "call_category polufabrikaty 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍹 Напитки ", "call_category napitki 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("⚱️ Соусы ", "call_category sousy 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍰 Кондитерка ", "call_category konditerka 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🌶 Специи ", "call_category specii 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍇 Сухофрукты ", "call_category suhofrukty 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍦 Мороженое ", "call_category morozhennoe 0") },
    //         new[]{new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🍚 Крупы и мука ", "call_category krupy-i-muka 0"), new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton("🏮 Консервация ", "call_category konservacija 0") },
    //     }
    // };

}