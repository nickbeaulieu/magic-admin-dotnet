using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class TokenModule_Decode
    {
        [Fact]
        public void Successfully_Decodes_DIDT()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var decodedToken = sdk.Token.Decode(Constants.VALID_DIDT);
            decodedToken.Proof.Should().Be(Constants.VALID_DIDT_DECODED.Proof);
            decodedToken.Claim.Equals(Constants.VALID_DIDT_DECODED.Claim).Should().BeTrue();
        }

        [Fact]
        public void Throws_If_Token_Malformed()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicMalformedTokenException();

            Action act = () => sdk.Token.Decode(Constants.INVALID_DIDT_MALFORMED_CLAIM);
            act.Should()
                .ThrowExactly<MagicMalformedTokenException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }
    }
}