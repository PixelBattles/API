using PixelBattles.API.DataTransfer.Images;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PixelBattles.API.Client.Tests.Integration
{
    public class CommonIntegrationTests : IntegrationTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void AlwaysTrueTest()
        {
            Assert.True(true);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ApiClient_CanCreate_Image()
        {
            var result = ApiClient.CreateImageAsync(new CreateImageDTO { Name = "TestName", Description = "TestDescription" }, new byte[] { 1, 2, 3 }, "image.png", "image/png");
        }
    }
}
