using System;

namespace Magic
{
    public class Claim : IEquatable<Claim>
    {
        /// <summary>
        /// Issued At Timestamp
        /// </summary>
        public ulong? Iat { get; set; }

        /// <summary>
        /// Expiration Timestamp
        /// </summary>
        public ulong? Ext { get; set; }

        /// <summary>
        /// Issuer of DID Token
        /// </summary>
        public string Iss { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string Aud { get; set; }

        /// <summary>
        /// Not Before Timestamp
        /// </summary>
        public ulong? Nbf { get; set; }

        /// <summary>
        /// DID Token ID
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// Encrypted signature of arbitrary data
        /// </summary>
        public string Add { get; set; }

        public bool Equals(Claim claim)
            => Add == claim.Add &&
            Aud == claim.Aud &&
            Ext == claim.Ext &&
            Iat == claim.Iat &&
            Iss == claim.Iss &&
            Nbf == claim.Nbf &&
            Sub == claim.Sub &&
            Tid == claim.Tid;
    }
}