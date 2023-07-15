---
layout: default
title: 開發第一個發送Line訊息的.NET程式
parent: 快速開始(QuickStart)
nav_order: 1
---


# 開發第一個發送Line訊息的.NET程式

從零開始，開發第一個發送Line訊息的.NET程式，其實步驟不多但是常常會遺忘順序。寫了一個快速開始，提醒以後的自己之外，也分享給有需要的開發人員。


## 範例源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging/demo/](https://github.com/Clark159/MDP.DevKit.LineMessaging/demo/)


## 套件源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging](https://github.com/Clark159/MDP.DevKit.LineMessaging)


## 說明文件

- [https://clark159.github.io/MDP.DevKit.LineMessaging](https://clark159.github.io/MDP.DevKit.LineMessaging)


## 操作步驟

1.註冊並登入[Line Developers Console](https://developers.line.biz/console/)網頁。於首頁，點擊「Create New Provider」按鈕，依照頁面提示建立一個Provider。
![01.建立 Provider01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/01.建立 Provider01.png)
![01.建立 Provider02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/01.建立 Provider02.png)

2.於Porvider頁面，點擊「Create a Messaging API channel」按鈕，依照頁面提示建立一個Messaging API Channel。
![02.建立 Messaging API Channel01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/02.建立 Messaging API Channel01.png)
![02.建立 Messaging API Channel02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/02.建立 Messaging API Channel02.png)

3.於Messaging API Channel頁面，進入Basic settings頁簽，取得「Channel Secret」、「Your User ID」；進入Messaging API頁簽，取得「QRCode」。
![03.取得參數01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/03.取得參數01.png)
![03.取得參數02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/03.取得參數02.png)

4.於Messaging API Channel頁面，進入Messaging API頁簽，點擊「Issue」按鈕，取得「Channel Access Token」。
![04.簽發Token01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/04.簽發Token01.png)
![04.簽發Token02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/04.簽發Token02.png)

5.開啟手機上的Line APP,掃描先前取得的「QRCode」，來加入好友。
![05.加入好友01.jpg](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/05.加入好友01.jpg)
![05.加入好友02.jpg](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/05.加入好友02.jpg)

6.開啟Visual Studio 2022，建立「空的ASP.NET Core」專案。
![06.建立專案01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/06.建立專案01.png)
![06.建立專案02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/06.建立專案02.png)

7.於專案內，加入NuGet套件：「MDP.AspNetCore」、「MDP.DevKit.LineMessaging」。
![07.加入套件01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/07.加入套件01.png)

8.於專案內，改寫Program.cs

```csharp
using MDP.AspNetCore;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Builder
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureDefault(); // 掛載MDP

            // App
            var app = builder.Build();
            app.MapDefaultControllerRoute();

            // Run
            app.Run();
        }
    }
}
```

9.於專案內，改寫appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  "MDP.DevKit.LineMessaging": {
    "LineMessageContextFactory": {
      "channelSecret": "XXXXX", // 填入先前取得的「Channel Secret」
      "channelAccessToken": "XXXXX" // 填入先前取得的「Channel Access Token」
    }
  }
}

```

10.於專案內，加入Controllers\HomeController.cs

```csharp
using MDP.DevKit.LineMessaging;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public partial class HomeController : Controller
    {
        // Fields                
        private readonly LineMessageContext _lineMessageContext;


        // Constructors
        public HomeController(LineMessageContext lineMessageContext)
        {
            #region Contracts

            if (lineMessageContext == null) throw new ArgumentException($"{nameof(lineMessageContext)}=null");

            #endregion

            // Default
            _lineMessageContext = lineMessageContext;
        }


        // Methods
        public async Task<ActionResult> Index()
        {
            // Variables
            var userId = "XXXXX"; // 填入先前取得的「Your user ID」
            var message = new TextMessage() { Text = "Hello World" };

            // PushMessage
            var result = await _lineMessageContext.MessageService.PushMessageAsync(message, userId);

            // Return
            return this.Json(result);
        }
    }
}
```

11.執行專案，就可以在Line APP裡面，收到此.NET程式發送的文字訊息「Hello World」
![11.執行結果01.jpg](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發第一個發送Line訊息的.NET程式/11.執行結果01.jpg)
