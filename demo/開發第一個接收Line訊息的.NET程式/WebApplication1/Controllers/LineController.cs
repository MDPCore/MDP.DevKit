using MDP.DevKit.LineMessaging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication1
{
    public partial class LineController : Controller
    {
        // Fields                
        private readonly LineMessageContext _lineMessageContext;


        // Constructors
        public LineController(LineMessageContext lineMessageContext)
        {
            #region Contracts

            if (lineMessageContext == null) throw new ArgumentException($"{nameof(lineMessageContext)}=null");

            #endregion

            // Default
            _lineMessageContext = lineMessageContext;
        }


        // Methods
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Hook-Line", Name = "Hook-Line")]
        public async Task<ActionResult> Hook()
        {
            try
            {
                // Content
                var content = string.Empty;
                using (var reader = new StreamReader(this.Request.Body))
                {
                    content = await reader.ReadToEndAsync();
                }
                if (string.IsNullOrEmpty(content) == true) return this.BadRequest();

                // Signature 
                var signature = string.Empty;
                if (this.Request.Headers.TryGetValue("X-Line-Signature", out var signatureHeader) == true)
                {
                    signature = signatureHeader.FirstOrDefault();
                }
                if (string.IsNullOrEmpty(signature) == true) return this.BadRequest();

                // HandleHook
                var eventList = _lineMessageContext.HandleHook(content, signature);
                if (eventList == null) return this.BadRequest();

                // Display
                var serializerSettings = new JsonSerializerSettings();
                {
                    serializerSettings.Converters.Add(new StringEnumConverter());
                }
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(eventList, Newtonsoft.Json.Formatting.Indented, serializerSettings));
                Console.WriteLine();

                // Return
                return this.Ok();
            }
            catch (Exception ex)
            {
                // Display
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine();

                // Return
                return this.Ok();
            }
        }
    }
}
