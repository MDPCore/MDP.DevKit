using MDP.DevKit.LineMessaging;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public partial class HomeController : Controller
    {
        // Fields                
        private readonly LineMessageContext _lineMessageContext;


        // Constructors
        public HomeController(LineMessageContext lineMessageContext)
        {
            #region Contracts

            if (lineMessageContext == null) throw new ArgumentException($"{nameof(lineMessageContext)}=null");

            #endregion

            // Default
            _lineMessageContext = lineMessageContext;
        }


        // Methods
        public async Task<ActionResult> Index()
        {
            // Variables
            var userId = "XXXXX"; // 填入先前取得的「Your user ID」
            var message = new TextMessage() { Text = "Hello World" };

            // PushMessage
            var result = await _lineMessageContext.MessageService.PushMessageAsync(message, userId);

            // Return
            return this.Json(result);
        }
    }
}
