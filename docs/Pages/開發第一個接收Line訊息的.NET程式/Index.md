---
layout: default
title: 開發第一個接收Line訊息的.NET程式
parent: 快速開始(QuickStart)
nav_order: 2
---


# 開發第一個**接收**Line訊息的.NET程式

從零開始，開發一個接收Line訊息的.NET程式，其實步驟不多但是常常會遺忘順序。寫了一個快速開始，提醒以後的自己之外，也分享給有需要的開發人員。


## 套件源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging](https://github.com/Clark159/MDP.DevKit.LineMessaging)


## 範例源碼

- [https://github.com/Clark159/MDP.DevKit.LineMessaging/Demo](https://github.com/Clark159/MDP.DevKit.LineMessaging/tree/master/demo/開發第一個接收Line訊息的.NET程式/WebApplication1)


## 操作步驟

0.使用Visual Studio 2022，開啟「[開發第一個發送Line訊息的.NET程式](https://clark159.github.io/MDP.DevKit.LineMessaging/Pages/開發第一個發送Line訊息的.NET程式/Index.html)」所開發的範例專案。

1.於專案內，建立開發人員通道。

2.於專案內，加入NuGet套件：「Newtonsoft.Json」。

3.於專案內，加入Controllers\LineController.cs

4.執行專案，複製取得「程式執行網址」。(**後續步驟，請保持專案執行**)

5.登入[Line Developers Console](https://developers.line.biz/console/)。於Messaging API Channel頁面，進入Messaging API頁簽，編輯「Webhook URL」、並開啟「Use webhook」。(**Webhook URL=「程式執行網址」+「/Hook-Line」**)

6.登入[Line Official Account Manager](https://manager.line.biz/)。進入首頁\自動回應訊息\自動回應訊息的頁面，關閉「Default」回應訊息。

7.開啟手機上的Line APP，發送文字訊息「吃飽沒?」。

8.回到步驟4專案執行的Console視窗內，可以看到此.NET程式接收到，由Line APP所發送的文字訊息「吃飽沒?」。
