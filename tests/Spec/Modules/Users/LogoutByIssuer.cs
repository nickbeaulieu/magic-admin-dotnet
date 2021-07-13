using Xunit;
using FluentAssertions;
using Magic;
using Moq;
using System.Net.Http;
using System.Net;
using Moq.Protected;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace MagicAdminTests
{
    public class LogoutByIssuerTests
    {
        public static MagicUserMetadata Success = new MagicUserMetadata { Issuer = "foo", PublicAddress = "bar", Email = "baz" };

        [Fact]
        public async void Successfully_POSTs_To_Logout_Endpoint_Via_DIDT()
        {
            var mock = new Mock<IRest>();

            mock
                .Setup<Task<object>>(
                    x => x.Post<object, object>(
                        "https://example.com/v2/admin/auth/user/logout", 
                        new Dictionary<string, string> { { "issuer", "did:ethr:0x1234" } }))
                .ReturnsAsync(null);

            var sdk = Factories.CreateMagicAdminSDK("https://example.com");
            var usersModule = new UsersModule(sdk, mock.Object);

            Func<Task> act = async () => await usersModule.LogoutByIssuer("did:ethr:0x1234");
            await act.Should().NotThrowAsync();
        }
    }
}