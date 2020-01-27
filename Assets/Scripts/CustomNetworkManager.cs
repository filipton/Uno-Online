using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using UnityEngine.UI;

public class CustomNetworkManager : LiteNetLib4MirrorNetworkManager
{
    [Header("Custom Fields")]
    //public IgnoranceTransport Transport;
    public string Nick;

    private void Update()
    {
        //print(NetworkTime.rtt);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), NetworkTime.rtt.ToString("F2"));
    }

    //nick voids
    public void SetNick(InputField IF)
    {
        Nick = IF.text;
    }


    public void ConnectToServer(string ip, ushort port)
    {
        if (!NetworkServer.active && !NetworkClient.active)
        {
            singleton.StartClient(ip, port);
        }
        else
        {
            print("HOST IS ALREADY ACTIVE!");
        }
    }

    public void ConnectToLocalServer(int port)
    {
        ConnectToServer("192.168.1.107", ushort.Parse(port.ToString()));
    }

    public void HostGame(ushort port, ushort maxPlayers = 4)
    {
        if (!NetworkServer.active && !NetworkClient.active)
        {
            singleton.StartHost(port, maxPlayers);
        }
        else
        {
            print("CLIENT OR SERVER IS ALREADY ACTIVE");
        }
    }
    public void HostGame(int port)
    {
        if (!NetworkServer.active && !NetworkClient.active)
        {
            singleton.StartHost(ushort.Parse(port.ToString()), 4);
        }
        else
        {
            print("CLIENT OR SERVER IS ALREADY ACTIVE");
        }
    }


    public void SetupServer(ushort port, ushort maxPlayers = 4)
    {
        if (!NetworkServer.active && !NetworkClient.active)
        {
            singleton.StartServer(port, maxPlayers);
        }
        else
        {
            print("CLIENT OR SERVER IS ALREADY ACTIVE");
        }
    }
    public void SetupServer(int port)
    {
        if (!NetworkServer.active && !NetworkClient.active)
        {
            singleton.StartServer(ushort.Parse(port.ToString()), 4);
        }
        else
        {
            print("CLIENT OR SERVER IS ALREADY ACTIVE");
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        print($"CLIENT CONNECTED! {conn.address}");
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        base.OnServerChangeScene(newSceneName);
    }
}