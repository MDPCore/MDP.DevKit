using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.DevKit.LineNotify
{
    [MDP.Registration.Service<LineNotifyContext>(singleton: true)]
    public class LineNotifyContext
    {
        // Methods
        public void HandleHook(string content)
        {
            #region Contracts

            if (string.IsNullOrEmpty(content) == true) throw new ArgumentException($"{nameof(content)}=null");

            #endregion
        }
    }
}
