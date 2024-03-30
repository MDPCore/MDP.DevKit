using MDP.Network.Rest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using MDP.Registration;

namespace MDP.DevKit.OpenAI.Accesses
{
    public class RestClientFactoryFactory : ServiceFactory<IServiceCollection, RestClientFactoryFactory.Setting>
    {
        // Constructors
        public RestClientFactoryFactory() : base("MDP.DevKit.OpenAI", "RestClientFactory") { }


        // Methods
        public override void ConfigureService(IServiceCollection serviceCollection, Setting setting)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");
            if (setting == null) throw new ArgumentException($"{nameof(setting)}=null");

            #endregion

            // RestClientFactory
            serviceCollection.AddRestClientFactory("MDP.DevKit.OpenAI", setting.Endpoints);
        }


        // Class
        public class Setting
        {
            // Properties
            public Dictionary<string, RestClientEndpoint> Endpoints { get; set; } = null;
        }
    }
}
