using System;

namespace Magic
{
    public class TokenModule : BaseModule
    {
        private readonly MagicAdminSDK SDK;

        public TokenModule(MagicAdminSDK sdk) : base(sdk) 
        {
            SDK = sdk;
        }

        public void Validate(string didToken, string attachment = "none")
        {
            var tokenSigner = "";
            var attachmentSigner = "";
            var claimedIssuer = "";
            Claim parsedClaim;
            string proof;
            string claim;

            try {
                var tokenParseResult = Parse.ParseDIDToken(didToken);
                proof = tokenParseResult.Raw.Proof;
                claim = tokenParseResult.Raw.Claim;
                parsedClaim = tokenParseResult.ParsedClaim.Claim;
                claimedIssuer = Issuer.ParsePublicAddressFromIssuer(parsedClaim.Iss);
            } catch {
                throw new MagicMalformedTokenException();
            }

            try {
                // Recover the token signer
                tokenSigner = Ec.Recover(claim, proof).ToLower();

                // Recover the attachment signer
                attachmentSigner = Ec.Recover(attachment, parsedClaim.Add).ToLower();
            } 
            catch 
            {
                throw new MagicFailedRecoveringProofException();
            }

            // Assert the expected signer
            if (claimedIssuer != tokenSigner || claimedIssuer != attachmentSigner) 
            {
                throw new MagicIncorrectSignerException();
            }

            ulong timeSecs = (ulong)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            ulong nbfLeeway = 300; // 5 min grace period

            // Assert the token is not expired
            if (parsedClaim.Ext < timeSecs) 
            {
                throw new MagicTokenExpiredException();
            }

            // Assert the token is not used before allowed.
            if (parsedClaim.Nbf - nbfLeeway > timeSecs) 
            {
                throw new MagicTokenCannotBeUsedYetException();
            }
        }

        public ParsedClaim Decode(string didToken)
        {
            var parsedToken = Parse.ParseDIDToken(didToken);
            return parsedToken.ParsedClaim;
        }

        public string GetPublicAddress(string didToken)
        {
            var claim = Decode(didToken).Claim;
            var claimedIssuer = claim.Iss.Split(":")[2];
            return claimedIssuer;
        }

        public string GetIssuer(string didToken)
            => Decode(didToken).Claim.Iss;
    }
}