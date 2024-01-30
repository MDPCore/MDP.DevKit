using MDP.Network.Rest;
using MDP.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MDP.DevKit.LineMessaging.Accesses
{
    public class RestClientFactoryFactory : ServiceFactory<IServiceCollection, RestClientFactoryFactory.Setting>
    {
        // Constructors
        public RestClientFactoryFactory() : base("MDP.DevKit.LineMessaging", "RestClientFactory") { }


        // Methods
        public override void ConfigureService(IServiceCollection serviceCollection, Setting setting)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");
            if (setting == null) throw new ArgumentException($"{nameof(setting)}=null");

            #endregion

            // RestClientFactory
            serviceCollection.AddRestClientFactory("MDP.DevKit.LineMessaging", setting.Endpoints);
        }


        // Class
        public class Setting
        {
            // Properties
            public Dictionary<string, RestClientEndpoint> Endpoints { get; set; } = null;
        }
    }
}
