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

namespace MagicAdminTests
{
    public class GetMetadataByIssuerTests
    {
        public static MagicUserMetadata Success = new MagicUserMetadata { Issuer = "foo", PublicAddress = "bar", Email = "baz" };
        public static MagicUserMetadata Null = new MagicUserMetadata { Issuer = null, PublicAddress = null, Email = null };
        public static MagicUserMetadata MissingProperty = new MagicUserMetadata { Issuer = "foo", PublicAddress = null };

        [Fact]
        public async void Successfully_GETs_To_Metadata_Eendpoint_Via_Issuer()
        {
            var mock = new Mock<IRest>();

            mock
                .Setup<Task<MagicUserMetadata>>(
                    x => x.Get<MagicUserMetadata>(
                        "https://example.com/v1/admin/auth/user/get", 
                        new Dictionary<string, string> { { "issuer", "did:ethr:0x1234" } }))
                .ReturnsAsync(Success);

            var sdk = Factories.CreateMagicAdminSDK("https://example.com");
            var usersModule = new UsersModule(sdk, mock.Object);

            var result = await usersModule.GetMetadataByIssuer("did:ethr:0x1234");
            // Issuer = "foo", PublicAddress = "bar", Email = "baz"
            result.Issuer.Should().Be("foo");
            result.PublicAddress.Should().Be("bar");
            result.Email.Should().Be("baz");
        }

        [Fact]
        public async void Successfully_GETs_Null_Metadata_Eendpoint_Via_Issuer()
        {
            var mock = new Mock<IRest>();

            mock
                .Setup<Task<MagicUserMetadata>>(
                    x => x.Get<MagicUserMetadata>(
                        "https://example.com/v1/admin/auth/user/get", 
                        new Dictionary<string, string> { { "issuer", "did:ethr:0x1234" } }))
                .ReturnsAsync(Null);

            var sdk = Factories.CreateMagicAdminSDK("https://example.com");
            var usersModule = new UsersModule(sdk, mock.Object);

            var result = await usersModule.GetMetadataByIssuer("did:ethr:0x1234");
            result.Issuer.Should().BeNull();
            result.PublicAddress.Should().BeNull();
            result.Email.Should().BeNull();
        }

        [Fact]
        public async void Successfully_GETs_MissingProperty_Metadata_Endpoint_Via_Issuer()
        {
            var mock = new Mock<IRest>();

            mock
                .Setup<Task<MagicUserMetadata>>(
                    x => x.Get<MagicUserMetadata>(
                        "https://example.com/v1/admin/auth/user/get", 
                        new Dictionary<string, string> { { "issuer", "did:ethr:0x1234" } }))
                .ReturnsAsync(MissingProperty);

            var sdk = Factories.CreateMagicAdminSDK("https://example.com");
            var usersModule = new UsersModule(sdk, mock.Object);

            var result = await usersModule.GetMetadataByIssuer("did:ethr:0x1234");
            result.Issuer.Should().Be("foo");
            result.PublicAddress.Should().BeNull();
            result.Email.Should().BeNull();
        }
    }
}