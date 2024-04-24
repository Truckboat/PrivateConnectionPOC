using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebStorageSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string DisplayWords { get; private set; }

        public string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string content = string.Format("Hello Service Connector! UTC Now: {0} {1}.", DateTimeOffset.UtcNow.ToString(), GetIPAddress);

            StorageHelper.UploadBlob(Environment.GetEnvironmentVariable(Const.ENDPOINT_ENV_KEY), Const.CONTAINER_NAME, Const.BLOB_NAME, content).Wait();
            DisplayWords = StorageHelper.GetBlob(Environment.GetEnvironmentVariable(Const.ENDPOINT_ENV_KEY), Const.CONTAINER_NAME, Const.BLOB_NAME).Result;
        }
    }
}
