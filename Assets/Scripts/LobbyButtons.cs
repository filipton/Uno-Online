using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButtons : MonoBehaviour
{
    public CustomNetworkManager CNM;
    public GameObject NetworkManagerPrefab;

    public void Start()
    {
        if(FindObjectsOfType<CustomNetworkManager>().Length < 1)
            Instantiate(NetworkManagerPrefab);
    }

    public void ConnectToServer(string ip, ushort port)
    {
        CNMGetter();
        if(CNM != null)
            CNM.ConnectToServer(ip, port);
    }

    public void ConnectToLocalServer(int port)
    {
        CNMGetter();
        if (CNM != null)
            CNM.ConnectToServer("192.168.1.107", ushort.Parse(port.ToString()));
    }

    public void HostGame(ushort port, ushort maxPlayers = 4)
    {
        CNMGetter();
        if (CNM != null)
            CNM.HostGame(port, maxPlayers);
    }
    public void HostGame(int port)
    {
        CNMGetter();
        if (CNM != null)
            CNM.HostGame(port);
    }


    public void SetupServer(ushort port, ushort maxPlayers = 4)
    {
        CNMGetter();
        if (CNM != null)
            CNM.SetupServer(port, maxPlayers);
    }
    public void SetupServer(int port)
    {
        CNMGetter();
        if (CNM != null)
            CNM.SetupServer(port);
    }

    public void SetNick(InputField IF)
    {
        CNMGetter();
        if (CNM != null)
            CNM.SetNick(IF);
    }


    public void CNMGetter()
    {
        if (CNM == null)
            CNM = FindObjectOfType<CustomNetworkManager>();
    }
}