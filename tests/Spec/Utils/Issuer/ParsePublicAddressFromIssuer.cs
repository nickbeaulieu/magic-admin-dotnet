using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class ParsePublicAddressFromIssuerTests
    {
        [Fact]
        public void Successfully_Parses_Public_Address_From_Issuer_String()
            => Issuer.ParsePublicAddressFromIssuer("did:ethr:0x1234").Should().Be("0x1234");

        [Theory]
        [InlineData("did:ethr", "")]
        [InlineData(null, "")]
        public void Returns_Empty_String_If_Public_Address_Fails_To_Parse(string input, string output)
            => Issuer.ParsePublicAddressFromIssuer(input).Should().Be(output);
    }
}