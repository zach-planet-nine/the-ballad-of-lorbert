using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class URLOpener : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void DeepLinkReceiverIsAlive();

    [System.Serializable]
    public class StringEvent : UnityEvent { }
    public StringEvent urlOpenedEvent;
    public bool dontDestroyOnLoad = true;

    void Start()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
        DeepLinkReceiverIsAlive(); // Let the App Controller know it's ok to call URLOpened now.
    }

    public void URLOpened(string url)
    {
        //urlOpenedEvent.Invoke(url);
        Debug.Log("Should open url: ");
        Debug.Log(url);
        //string successURL = "pntbltest://ongoing/details?success=true&userId=41";
        ParsedURL parsed = new ParsedURL(url);
        Debug.Log(parsed.path);
        switch(parsed.path)
        {
            case "ongoing/details": HandleOngoing(parsed);
                break;
        }
    }

    private void HandleOngoing(ParsedURL parsedURL)
    {
        Debug.Log("Should handle the parsed url here");
        if(parsedURL.queries["success"] == "true")
        {
            Debug.Log("Success was true");
            //NineumManager.manager.ConnectUser(Int32.Parse(parsedURL.queries["userId"]));
            CharacterStats.characterStats.ConnectUser(Int32.Parse(parsedURL.queries["userId"]));
        }
    }
}

class ParsedURL
{
    public string host;
    public string path;
    public Dictionary<string, string> queries = new Dictionary<string, string>();

    public ParsedURL(string url)
    {
        string[] hostAndPath = url.Split(new string[] { "://" }, StringSplitOptions.None);
        host = hostAndPath[0];
        string[] pathAndQuery = hostAndPath[1].Split('?');
        path = pathAndQuery[0];
        string[] queryKeyValues = pathAndQuery[1].Split('&');
        for(int i = 0; i < queryKeyValues.Length; i++)
        {
            string[] keyAndValue = queryKeyValues[i].Split('=');
            queries[keyAndValue[0]] = keyAndValue[1];
        }
    }
}
