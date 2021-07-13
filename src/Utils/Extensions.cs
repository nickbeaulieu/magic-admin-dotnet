namespace Magic
{
    public static class MagicExtensions
    {
        public static bool IsDIDTClaim(this Claim claim)
        {
            if (claim == null)
            {
                return false;
            }

            return claim.Iat != null &&
                claim.Ext != null &&
                claim.Iss != null &&
                claim.Sub != null &&
                claim.Aud != null &&
                claim.Nbf != null &&
                claim.Tid != null &&
                claim.Add != null;
        }
    }
}