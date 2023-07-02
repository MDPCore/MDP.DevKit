using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.DevKit.LineMessaging
{
    public interface ContentService
    {
        // Methods
        Task<Stream> CreatePreviewContentAsync(ContentProvider contentProvider);

        Task<Stream> CreateOriginalContentAsync(ContentProvider contentProvider);
    }
}
