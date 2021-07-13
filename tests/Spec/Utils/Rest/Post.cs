using Xunit;
using FluentAssertions;
using Magic;
using Moq;
using System.Net.Http;
using System.Net;
using Moq.Protected;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace MagicAdminTests
{
    public class PostTests
    {
        [Fact]
        public void Successfully_POSTs_To_The_Given_Endpoint_And_Stringifies_Body()
        {
            // doesn't throw
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""data"": ""hello world"", ""status"": ""ok""}"),
            };

            mock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(mock.Object);
            var rest = new Rest(Constants.API_KEY, client);

            var result = rest.Post<object, object>("https://example.com/hello/world", new object [] { "anything" });

            mock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Post && 
                    req.Headers.GetValues("X-Magic-Secret-Key").First() == Constants.API_KEY &&
                    req.RequestUri == new System.Uri("https://example.com/hello/world")),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}