using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.DevKit.LineNotify.Lab
{
    public class AuthController : Controller
    {
        // Fields
        private readonly HttpClient _httpClient = null;


        // Constructors
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            #region Contracts

            if (httpClientFactory == null) throw new ArgumentException($"{nameof(httpClientFactory)}=null");

            #endregion

            // HttpClient
            _httpClient = httpClientFactory.CreateClient();
            if (_httpClient == null) throw new InvalidOperationException($"{nameof(_httpClient)}=null");
        }

        // Methods
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Hook-LineNotify")]
        public async Task<ActionResult> Index()
        {
            // ContentString
            var contentString = string.Empty;
            using (var reader = new StreamReader(this.Request.Body))
            {
                contentString = await reader.ReadToEndAsync();
            }
            if (string.IsNullOrEmpty(contentString) == true) return this.BadRequest();
            if (string.IsNullOrEmpty(contentString) == false) Console.WriteLine(contentString);

            // ContentList
            var contentList = new Dictionary<string, string>();
            foreach (var argumentString in contentString.Split('&'))
            {
                var argumentPair = argumentString.Split('=');
                if (argumentPair.Length == 2)
                {
                    contentList.Add(argumentPair[0], argumentPair[1]);
                }
            }

            // AccessToken
            string accessToken = null;
            {
                // RequestUrl
                var requestUri = @"https://notify-bot.line.me/oauth/token";

                // ResultFactory
                var resultFactory = (JsonElement resultElement) =>
                {
                    // Result
                    dynamic resultModel = new ExpandoObject();
                    resultModel.status = resultElement.GetProperty<int>("status");
                    resultModel.message = resultElement.GetProperty<string>("message") ?? string.Empty;
                    resultModel.accessToken = resultElement.GetProperty<string>("access_token") ?? string.Empty;

                    // Return
                    return resultModel;
                };

                // ResultModel
                var resultModel = await _httpClient.PostAsync<dynamic>(requestUri, resultFactory: resultFactory, query: new
                {
                    grant_type = "authorization_code",
                    redirect_uri = "https://mvp-app.wonderfulsmoke-418e59bf.eastasia.azurecontainerapps.io/Hook-LineNotify",
                    client_id = "swTiI2bwOil2FcdtaD5Pli",
                    client_secret = "WcbGMacHD2vPvBhtx1sysSrpNHbUtxfrFhNFlKE43H7",
                    code = contentList["code"],
                });
                if (resultModel==null) throw new InvalidOperationException($"{nameof(resultModel)}= null");

                // accessToken
                accessToken = resultModel.accessToken;
                if (string.IsNullOrEmpty(accessToken) == true) throw new InvalidOperationException($"{nameof(resultModel)}= null");
            }

            // Message
            {
                // RequestUrl
                var requestUri = @"https://notify-api.line.me/api/notify";

                // requestHeader
                var requestHeader = new
                {
                    Authorization = "Bearer " + accessToken
                };

                // RequestContent
                var requestContent = new Dictionary<string, string>();
                requestContent.Add("message", "\n感謝您預約「裱室Bios workshop框物研究室」\n預約時間：2024/01/31 15:00.\n預約內容：整批展覽設計\n有任何問題都可以透過官方Line帳號詢問：https://liff.line.me/1645278921-kWRPP32q/?accountId=519iihxl");
                requestContent.Add("stickerPackageId", "1");
                requestContent.Add("stickerId", "1");

                // ResultModel
                var resultModel = await _httpClient.PostAsync<string>(requestUri, headers: requestHeader, content: requestContent                );
                if (resultModel == null) throw new InvalidOperationException($"{nameof(resultModel)}= null");
            }

            // Return
            return View();
        }
    }
}
