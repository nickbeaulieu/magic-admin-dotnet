using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Magic
{
    public class ParsedDIDTokenResult 
    {
        public RawClaim Raw { get; set; }
        public ParsedClaim ParsedClaim { get; set; }
    }

    public class ParsedClaim
    {
        public string Proof { get; set; }

        public Claim Claim { get; set; }
    }

    public class RawClaim
    {
        public string Proof { get; set; }
        public string Claim { get; set; }
    }

    public static class Parse 
    {
        public static ParsedDIDTokenResult ParseDIDToken(string didToken)
        {
            try
            {
                // fix
                var bytes = Convert.FromBase64String(didToken);
                var str = Encoding.ASCII.GetString(bytes);
                var claim = JsonConvert.DeserializeObject<List<string>>(str);
                var proof = claim[0];
                var parsedClaim = JsonConvert.DeserializeObject<Claim>(claim[1]) as Claim;
                if (!parsedClaim.IsDIDTClaim())
                {
                    throw new MagicException();
                } 
                return new ParsedDIDTokenResult { Raw = new RawClaim { Proof = proof, Claim = claim[1]}, ParsedClaim = new ParsedClaim { Proof = proof, Claim = parsedClaim} };
            }
            catch (System.Exception)
            {
                throw new MagicMalformedTokenException();
            }
        }
    }
}