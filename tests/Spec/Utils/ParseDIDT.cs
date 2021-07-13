using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class ParseDIDTTests
    {
        [Fact]
        public void Successfully_Parses_DIDT()
        {
            var result = Parse.ParseDIDToken(Constants.VALID_DIDT);
            result.ParsedClaim.Proof.Should().Be(Constants.VALID_DIDT_DECODED.Proof);
            result.ParsedClaim.Claim.Equals(Constants.VALID_DIDT_DECODED.Claim).Should().BeTrue();
        }

        [Fact]
        public void Throws_If_Token_Malformed()
        {
            var expected = new MagicMalformedTokenException();

            Action act = () => Parse.ParseDIDToken(Constants.INVALID_DIDT_MALFORMED_CLAIM);
            act.Should()
                .ThrowExactly<MagicMalformedTokenException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }
    }
}
