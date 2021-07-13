using Xunit;
using FluentAssertions;
using Magic;
using System;
using System.Collections.Generic;

namespace MagicAdminTests
{
    public class IsDIDTClaimTests
    {
        [Theory]
        [MemberData(nameof(ClaimsToCheck))]
        public void Returns_False_Unless_All_Fields_Populated(bool output, Claim claim)
            => claim.IsDIDTClaim().Should().Be(output);

        public static IEnumerable<object[]> ClaimsToCheck
        {
            get
            {
                yield return new object[] { false, null };

                yield return new object[] { false, new Claim { Ext = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Sub = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Sub = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Tid = "asdf", Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Nbf = 123, Add = "0x0123" } };
                yield return new object[] { false, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf" } };
                
                yield return new object[] { true, new Claim { Iat = 123, Ext = 123, Iss = "asdf", Sub = "asdf", Aud = "asdf", Nbf = 123, Tid = "asdf", Add = "0x0123" } };

            }
        }
    }
}