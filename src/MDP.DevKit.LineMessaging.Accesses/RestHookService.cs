using MDP.Network.Rest;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.DevKit.LineMessaging.Accesses
{
    [MDP.Registration.Service<HookService>()]
    public partial class RestHookService : RestBaseService, HookService
    {
        // Constructors
        public RestHookService(RestClientFactory restClientFactory) : base(restClientFactory) { }


        // Methods
        public Task SetEndpointAsync(string endpoint)
        {
            #region Contracts

            if (string.IsNullOrEmpty(endpoint) == true) throw new ArgumentException($"{nameof(endpoint)}=null");

            #endregion

            // RequestUrl
            var requestUri = $"/channel/webhook/endpoint";

            // RequestContent
            dynamic requestContent = new ExpandoObject();
            {
                // Endpoint
                requestContent.endpoint = endpoint;
            }

            // ResultFactory
            {
                
            }

            // Execute
            {
                // GetAsync
                return this.GetAsync(requestUri, content: requestContent);
            }
        }

        public Task<bool> VerifyEndpointAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Hook> GetHookAsync()
        {
            // RequestUrl
            var requestUri = $"/channel/webhook/endpoint";

            // RequestContent
            {

            }

            // ResultFactory
            var resultFactory = (JsonElement resultElement) => 
            {
                // Result
                var result = new Hook
                {
                    Endpoint = resultElement.GetProperty<string>("endpoint") ?? string.Empty,
                    Active = resultElement.GetProperty<bool>("active")
                };

                // Return
                return result;
            };

            // Execute
            {
                // GetAsync
                var resultModel = await this.GetAsync<Hook>(requestUri, resultFactory: resultFactory);
                if (resultModel == null) throw new InvalidOperationException($"{nameof(resultModel)}=null");

                // Return
                return resultModel;
            }
        }        
    }
}
