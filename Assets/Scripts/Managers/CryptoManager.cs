using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cryptography.ECDSA;
using System.Text;

public class CryptoManager
{
    public static string publicKey = "02ad1a20223c89519ae7fe334eb227541e8bb2e5a4c441b19fbb9205148eb44793";
    private static string privateKey = "b2153a3dd5b21eac6ec61a2780676a8f3d064171548c36dac3afc18fabdff70e"; //TODO: MAKE SURE TO CHANGE THIS BEFORE PRODUCTION

    public static string signMessage(string message)
    {

        Debug.Log("Trying to sign the following:");
        Debug.Log(message);

        byte[] msg = Encoding.UTF8.GetBytes(message);
        var seckey = Hex.HexToBytes(privateKey);
        var data = Sha256Manager.GetHash(msg);
        var recoveryId = 24;
        var sig = Secp256K1Manager.SignCompact(data, seckey, out recoveryId);
        var signature = Hex.ToString(sig);

        return signature;
    }
}
