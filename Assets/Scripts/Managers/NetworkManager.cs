using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{

    IEnumerator Get(string url, Action<string, string> callback)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
            callback(req.error, null);
        }
        else
        {
            Debug.Log(req.downloadHandler.text);
            Debug.Log("Upload complete");
            callback(null, req.downloadHandler.text);
        }
    }

    IEnumerator Put(string url, string data, Action<string, string> callback)
    {
        UnityWebRequest req = UnityWebRequest.Put(url, data);
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();

        if(req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
            Debug.Log(req.downloadHandler.text);
            callback(req.error, req.downloadHandler.text);
        } else
        {
            Debug.Log(req.downloadHandler.text);
            Debug.Log("Upload complete");
            callback(null, req.downloadHandler.text);
        }
    }

    public void RefreshUser(int userId, GatewayTimestampTupleWithSignature gateway, Action<string, string> callback)
    {
        string path = "https://www.plnet9.com/user/userId/" + userId + "/gateway/" + gateway.gatewayName +
            "/signature/" + gateway.signature + "/timestamp/" + gateway.timestamp;

        Debug.Log("About to get user at " + path);

        StartCoroutine(Get(path, callback));
    }

    public void UsePower(UsePowerWithSignature upws, Action<string, string> callback)
    {
        string path = "https://www.plnet9.com/user/userId/" + upws.userId + "/power/gateway/" + upws.gatewayName + "/ongoing";

        Debug.Log(path);
        Debug.Log("Sending the following: ");
        Debug.Log(JsonUtility.ToJson(upws));

        StartCoroutine(Put(path, JsonUtility.ToJson(upws), callback));
    }
}
