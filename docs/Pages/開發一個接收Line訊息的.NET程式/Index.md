---
layout: default
title: 開發一個接收Line訊息的.NET程式
parent: 快速開始(QuickStart)
nav_order: 2
---


# 開發一個**接收**Line訊息的.NET程式

從零開始，開發一個接收Line訊息的.NET程式，其實步驟不多但是常常會遺忘順序。本篇內容協助開發人員，逐步完成必要的設計和實作。


## 套件源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging](https://github.com/Clark159/MDP.DevKit.LineMessaging)


## 範例源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging/Demo](https://github.com/Clark159/MDP.DevKit.LineMessaging/tree/master/demo/開發一個接收Line訊息的.NET程式/WebApplication1)


## 操作步驟

0.使用Visual Studio 2022，開啟「[開發一個發送Line訊息的.NET程式](https://clark159.github.io/MDP.DevKit.LineMessaging/Pages/開發一個發送Line訊息的.NET程式/Index.html)」所開發的範例專案。
![00.開啟專案01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/00.開啟專案01.png)

1.於專案內，建立並使用開發人員通道。(通道類型：永續性、存取：公用)
![01.開發通道01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/01.開發通道01.png)
![01.開發通道02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/01.開發通道02.png)

2.於專案內，加入NuGet套件：「Newtonsoft.Json」。
![02.安裝套件01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/02.安裝套件01.png)

3.於專案內，加入Controllers\LineController.cs

```csharp
using MDP.DevKit.LineMessaging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication1
{
    public partial class LineController : Controller
    {
        // Fields                
        private readonly LineMessageContext _lineMessageContext;


        // Constructors
        public LineController(LineMessageContext lineMessageContext)
        {
            #region Contracts

            if (lineMessageContext == null) throw new ArgumentException($"{nameof(lineMessageContext)}=null");

            #endregion

            // Default
            _lineMessageContext = lineMessageContext;
        }


        // Methods
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Hook-Line", Name = "Hook-Line")]
        public async Task<ActionResult> Hook()
        {
            try
            {
                // Content
                var content = string.Empty;
                using (var reader = new StreamReader(this.Request.Body))
                {
                    content = await reader.ReadToEndAsync();
                }
                if (string.IsNullOrEmpty(content) == true) return this.BadRequest();

                // Signature 
                var signature = string.Empty;
                if (this.Request.Headers.TryGetValue("X-Line-Signature", out var signatureHeader) == true)
                {
                    signature = signatureHeader.FirstOrDefault();
                }
                if (string.IsNullOrEmpty(signature) == true) return this.BadRequest();

                // HandleHook
                var eventList = _lineMessageContext.HandleHook(content, signature);
                if (eventList == null) return this.BadRequest();

                // Display
                var serializerSettings = new JsonSerializerSettings();
                {
                    serializerSettings.Converters.Add(new StringEnumConverter());
                }
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(eventList, Newtonsoft.Json.Formatting.Indented, serializerSettings));
                Console.WriteLine();

                // Return
                return this.Ok();
            }
            catch (Exception ex)
            {
                // Display
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine();

                // Return
                return this.Ok();
            }
        }
    }
}
```

4.執行專案，複製取得「程式執行網址」。(**後續步驟，請保持專案執行**)
![04.執行專案01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/04.執行專案01.png)
![04.執行專案02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/04.執行專案02.png)
![04.執行專案03.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/04.執行專案03.png)

5.登入[Line Developers Console](https://developers.line.biz/console/)。於Messaging API Channel頁面，進入Messaging API頁簽，編輯「Webhook URL」、並開啟「Use webhook」。(**Webhook URL=「程式執行網址」+「/Hook-Line」**)
![05.開啟Hook01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/05.開啟Hook01.png)
![05.開啟Hook02.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/05.開啟Hook02.png)

6.登入[Line Official Account Manager](https://manager.line.biz/)。進入首頁\自動回應訊息\自動回應訊息的頁面，關閉「Default」回應訊息。
![06.關閉回應01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/06.關閉回應01.png)

7.開啟手機上的Line APP，發送文字訊息「吃飽沒?」。
![07.發送訊息01.jpg](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/07.發送訊息01.jpg)

8.回到步驟4專案執行的Console視窗內，可以看到由Line APP所發送的文字訊息「吃飽沒?」。
![08.接收訊息01.png](https://raw.githubusercontent.com/Clark159/MDP.DevKit.LineMessaging/master/docs/Pages/開發一個接收Line訊息的.NET程式/08.接收訊息01.png)
