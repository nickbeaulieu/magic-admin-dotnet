// import ethSigUtil from 'eth-sig-util';
// import * as ethUtil from 'ethereumjs-util';

// /**
//  * Recover the signer from an Elliptic Curve signature.
//  */
// export function ecRecover(data: string, signature: string) {
//   // Use ecRecover on the Proof, to validate if it recovers to the expected
//   // Claim, and expected Signer Address.
//   const ecRecoverMsgParams = {
//     data: ethUtil.bufferToHex(Buffer.from(data, 'utf8')),
//     sig: signature,
//   };

//   return ethSigUtil.recoverPersonalSignature(ecRecoverMsgParams);
// }

using Nethereum.Signer;

namespace Magic
{
    // https://docs.nethereum.com/en/latest/nethereum-signing-messages/#2-verifying-a-signed-message-encoded-in-utf8-using-encodeutf8andecrecover
    public static class Ec
    {
        private static EthereumMessageSigner Signer = new EthereumMessageSigner();
        public static string Recover(string data, string signature)
            => Signer.EncodeUTF8AndEcRecover(data, signature);
    }
}