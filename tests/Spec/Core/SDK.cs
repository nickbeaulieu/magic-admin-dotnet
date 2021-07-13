using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class SDKTests
    {
        [Fact]
        public void Initialize_MagicAdminSDK()
        {
            var magic = new MagicAdminSDK(Constants.API_KEY);

            magic.SecretApiKey.Should().Be(Constants.API_KEY);
            magic.ApiBaseUrl.Should().Be(Constants.API_FULL_URL);
            magic.Token.Should().BeOfType(typeof(TokenModule));
            magic.Utils.Should().BeOfType(typeof(UtilsModule));
            magic.Users.Should().BeOfType(typeof(UsersModule));
        }

        [Fact]
        public void Initialize_MagicAdminSDK_With_Custom_Endpoint()
        {
            var magic = new MagicAdminSDK(Constants.API_KEY, new MagicAdminSDKAdditionalConfiguration { Endpoint = "https://example.com" });

            magic.SecretApiKey.Should().Be(Constants.API_KEY);
            magic.ApiBaseUrl.Should().Be("https://example.com");
            magic.Token.Should().BeOfType(typeof(TokenModule));
            magic.Utils.Should().BeOfType(typeof(UtilsModule));
            magic.Users.Should().BeOfType(typeof(UsersModule));
        }

        [Fact]
        public void Strips_Trailing_Slashes_From_Custom_Endpoint_Argument()
        {
            var magic1 = new MagicAdminSDK(Constants.API_KEY , new MagicAdminSDKAdditionalConfiguration { Endpoint = "https://example.com/" });
            var magic2 = new MagicAdminSDK(Constants.API_KEY , new MagicAdminSDKAdditionalConfiguration { Endpoint = "https://example.com//" });
            var magic3 = new MagicAdminSDK(Constants.API_KEY , new MagicAdminSDKAdditionalConfiguration { Endpoint = "https://example.com///" });

            magic1.ApiBaseUrl.Should().Be("https://example.com");
            magic2.ApiBaseUrl.Should().Be("https://example.com");
            magic3.ApiBaseUrl.Should().Be("https://example.com");
        }

        [Fact]
        public void Initialize_MagicAdminSDK_Throws_Without_API_Key()
        {
            var expected = new MagicApiKeyMissingException();
            Action act = () => new MagicAdminSDK(null);
            act.Should()
                .ThrowExactly<MagicApiKeyMissingException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }
    }
}
