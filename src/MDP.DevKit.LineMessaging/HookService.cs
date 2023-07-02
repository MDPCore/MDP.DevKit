using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.DevKit.LineMessaging
{
    public interface HookService
    {
        // Methods
        Task SetEndpointAsync(string endpoint);

        Task<bool> VerifyEndpointAsync();

        Task<Hook> GetHookAsync();
    }
}
