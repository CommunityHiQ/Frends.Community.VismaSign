using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Frends.Community.VismaSign.Test
{
    public class Tests
    {
        private const string Identifier = "fill me";
        private const string Secret = "fill me";
        private const string BaseAddress = "https://sign.visma.net";

        [Fact]
        public async Task ShouldCreateDocument()
        {

            var settings = new DocumentCreateInput
            {
                Body = "{ \"document\": { \"name\": \"Test document\"}}"
            };

            var options = new ConnectionOption
            {
                Identifier = Identifier,
                Secret = Secret,
                BaseAddress = BaseAddress
            };

            HttpResponse result = await (dynamic) VismaSign.DocumentCreate(settings, options, new CancellationToken());

            Assert.StartsWith("https://sign.visma.net/api/v1/document/", result.Location);
        }

        [Fact]
        public async Task ShouldSearchDocuments()
        {

            var settings = new DocumentSearchInput()
            {
                Query = "status=cancelled"
            };

            var options = new ConnectionOption
            {
                Identifier = Identifier,
                Secret = Secret,
                BaseAddress = BaseAddress
            };

            HttpResponseWithBody result = await (dynamic)VismaSign.DocumentSearch(settings, options, new CancellationToken());

            Assert.StartsWith("{\"total\":", result.Body);
        }

        [Fact]
        public async Task ShouldGetDocument()
        {
            var settings = new DocumentGetInput()
            {
                DocumentUriId = "38ca4a5c-5080-4b95-9486-4055f226ecbd",
                Passphrase = ""
            };

            var options = new ConnectionOption
            {
                Identifier = Identifier,
                Secret = Secret,
                BaseAddress = BaseAddress
            };

            HttpResponseWithByteArrayBody result = await (dynamic)VismaSign.DocumentGet(settings, options, new CancellationToken());

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ShouldGetCategory()
        {
            var options = new ConnectionOption
            {
                Identifier = Identifier,
                Secret = Secret,
                BaseAddress = BaseAddress
            };

            HttpResponseWithBody result = await (dynamic)VismaSign.CategoriesGet(options, new CancellationToken());

            Assert.Equal(200, result.StatusCode);
        }
    }
}
