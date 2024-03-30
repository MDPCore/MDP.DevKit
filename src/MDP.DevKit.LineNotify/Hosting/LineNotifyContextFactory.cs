using MDP.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MDP.Network.Http;

namespace MDP.DevKit.LineNotify
{
    public class LineNotifyContextFactory : Factory<IServiceCollection, LineNotifyContextFactory.Setting>
    {
        // Constructors
        public LineNotifyContextFactory() : base("MDP.DevKit.LineNotify", "LineNotifyContext") { }


        // Methods
        public override void ConfigureService(IServiceCollection serviceCollection, Setting setting)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");
            if (setting == null) throw new ArgumentException($"{nameof(setting)}=null");

            #endregion

            // AddHttpClientFactory
            serviceCollection.AddHttpClientFactory();
        }


        // Class
        public class Setting
        {
            // Properties
        }
    }
}

