using System.Linq;

namespace Magic
{
    public static class Issuer
    {
        public static string GenerateIssuerFromPublicAddress(string publicAddress, string method = "ethr")
            => $"did:{method}:{publicAddress}";

        public static string ParsePublicAddressFromIssuer(string issuer)
            => issuer?.Split(":")?.ElementAtOrDefault(2)?.ToLower() ?? "";
    }
}