using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PixelBattles.API.Server.Web;

namespace PixelBattles.API.Client.Tests.Integration
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected IWebHost Host { get; set; }

        protected IApiClient ApiClient { get; set; }

        public IntegrationTestBase()
        {
            Host = Program.BuildWebHost(new string[] { });
            Host.Start();

            ApiClient = new ApiClient(Options.Create(new ApiClientOptions { BaseUrl = "http://localhost:5000" }));
        }

        public void Dispose()
        {
            Host?.Dispose();
            ApiClient?.Dispose();
        }
    }
}
