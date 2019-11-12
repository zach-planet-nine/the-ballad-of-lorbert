using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GatewayModel
{
    public string gatewayName;
    public string publicKey;
    public string timestamp;

    public GatewayModel(string name, string key)
    {
        gatewayName = name;
        publicKey = key;
        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}

public class GatewayModelWithSignature
{
    public string gatewayName;
    public string publicKey;
    public string timestamp;
    public string signature;

    public GatewayModelWithSignature(GatewayModel gateway, string sig)
    {
        gatewayName = gateway.gatewayName;
        publicKey = gateway.publicKey;
        timestamp = gateway.timestamp;
        signature = sig;
    }
}

[Serializable]
public class GatewayTimestampTuple
{
    public string gatewayName;
    public string timestamp;

    public GatewayTimestampTuple(string name)
    {
        gatewayName = name;
        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}

public class GatewayTimestampTupleWithSignature
{
    public string gatewayName;
    public string timestamp;
    public string signature;

    public GatewayTimestampTupleWithSignature(GatewayTimestampTuple gateway, string sig)
    {
        gatewayName = gateway.gatewayName;
        timestamp = gateway.timestamp;
        signature = sig;
    }
}

[Serializable]
public class UsePower
{
    public int totalPower;
    public string partnerName;
    public string gatewayName;
    public int userId;
    public string publicKey;
    public int ordinal;
    public string timestamp;

    public UsePower(int power, string partner, string gateway, int id, string key, int powerOrdinal)
    {
        totalPower = power;
        partnerName = partner;
        gatewayName = gateway;
        userId = id;
        publicKey = key;
        ordinal = powerOrdinal;
        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}

public class UsePowerWithSignature
{
    public int totalPower;
    public string partnerName;
    public string gatewayName;
    public int userId;
    public string publicKey;
    public int ordinal;
    public string description;
    public string timestamp;
    public string signature;

    public UsePowerWithSignature(UsePower usePower, string desc, string sig)
    {
        totalPower = usePower.totalPower;
        partnerName = usePower.partnerName;
        gatewayName = usePower.gatewayName;
        userId = usePower.userId;
        publicKey = usePower.publicKey;
        ordinal = usePower.ordinal;
        description = desc;
        timestamp = usePower.timestamp;
        signature = sig;
    }
}
