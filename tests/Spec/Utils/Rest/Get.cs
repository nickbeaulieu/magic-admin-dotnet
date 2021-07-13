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
using System.Collections.Generic;

namespace MagicAdminTests
{
    public class GetTests
    {
        [Fact]
        public void Successfully_GETs_To_The_Given_Endpoint_And_Stringifies_Query()
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

            var headers = new Dictionary<string, string>() { { "issuer", "did:ethr:0x1234" } };

            var result = rest.Get<object>("https://example.com/hello/world", headers);

            mock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && 
                    req.Headers.GetValues("X-Magic-Secret-Key").First() == Constants.API_KEY &&
                    req.RequestUri == new System.Uri("https://example.com/hello/world?issuer=did%3aethr%3a0x1234")),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public void Successfully_GETs_To_The_Given_Endpoint_Without_Query()
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

            var result = rest.Get<object>("https://example.com/hello/world");

            mock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && 
                    req.Headers.GetValues("X-Magic-Secret-Key").First() == Constants.API_KEY &&
                    req.RequestUri == new System.Uri("https://example.com/hello/world")),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}