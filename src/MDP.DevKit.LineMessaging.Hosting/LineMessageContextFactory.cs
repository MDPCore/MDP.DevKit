using MDP.DevKit.LineMessaging.Accesses;
using MDP.Network.Rest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using MDP.Registration;

namespace MDP.DevKit.LineMessaging.Hosting
{
    public class LineMessageContextFactory: ServiceFactory<IServiceCollection, LineMessageContextFactory.Setting>
    {
        // Constructors
        public LineMessageContextFactory() : base("MDP.DevKit.LineMessaging", "LineMessageContextFactory") { }


        // Methods
        public override void ConfigureService(IServiceCollection serviceCollection, Setting setting)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");
            if (setting == null) throw new ArgumentException($"{nameof(setting)}=null");

            #endregion

            // Domain
            serviceCollection.TryAddSingleton<LineMessageContext>();

            // Accesses
            serviceCollection.TryAddTransient<EventService, RestEventService>();
            serviceCollection.TryAddTransient<SignatureService>(serviceProvider => { return new RestSignatureService(setting.ChannelSecret); });
            serviceCollection.TryAddTransient<HookService, RestHookService>();
            serviceCollection.TryAddTransient<UserService, RestUserService>();
            serviceCollection.TryAddTransient<ContentService, RestContentService>();
            serviceCollection.TryAddTransient<MessageService, RestMessageService>();

            // RestClientFactory
            serviceCollection.AddRestClientFactory("MDP.DevKit.LineMessaging", new List<RestClientEndpoint>()
            {
                new RestClientEndpoint(){ Name= "LineMessageService", BaseAddress=@"https://api.line.me/v2/bot/", Headers = new Dictionary<string, string>(){ {"Authorization", $"Bearer {setting.ChannelAccessToken}" } } },
                new RestClientEndpoint(){ Name= "LineContentService", BaseAddress=@"https://api-data.line.me/v2/bot/", Headers = new Dictionary<string, string>(){ {"Authorization", $"Bearer {setting.ChannelAccessToken}" } } }
            });
        }


        // Class
        public class Setting
        {
            // Properties
            public string ChannelSecret { get; set; }

            public string ChannelAccessToken { get; set; }
        }
    }
}
