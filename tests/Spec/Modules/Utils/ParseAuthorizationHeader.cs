using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class ParseAuthorizationHeaderTests
    {
        [Fact]
        public void Successfully_Parses_Raw_DIDT_From_Bearer_Authorization_Header()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var token = sdk.Utils.ParseAuthorizationHeader($"Bearer {Constants.VALID_DIDT}");
            token.Should().Be(Constants.VALID_DIDT);
        }

        [Fact]
        public void Raises_Error_If_Header_Is_In_Wrong_Format()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicExpectedBearerException();
            Action act = () => sdk.Utils.ParseAuthorizationHeader($"Ooops {Constants.VALID_DIDT}");
            act.Should()
                .ThrowExactly<MagicExpectedBearerException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }
    }
}