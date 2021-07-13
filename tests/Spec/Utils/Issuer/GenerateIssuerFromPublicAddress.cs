using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class GenerateIssuerFromPublicAddressTests
    {
        [Fact]
        public void Successfully_Builds_Issuer_String_From_Public_Address()
            => Issuer.GenerateIssuerFromPublicAddress("0x1234").Should().Be("did:ethr:0x1234");

        [Fact]
        public void Successfully_Builds_Issuer_String_From_Public_Address_With_Overrided_Method()
            =>Issuer.GenerateIssuerFromPublicAddress("0x1234", "test").Should().Be("did:test:0x1234");
    }
}