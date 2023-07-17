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

4.執行專案，並且複製取得「程式執行網址」。

5.

5. 註冊並登入Line Developers Console。於Messaging API Channel頁面，進入Messaging API頁簽，開啟WebHook、並且設定WebHook URL(先前取得的「程式執行網址」+「/Hook-Line」)
6. 註冊並登入Line Official Account Manager。於首頁的XXXXX/自動回復頁面內，找到關閉自動回復
7. 到LienAPP裡面發送訊息
