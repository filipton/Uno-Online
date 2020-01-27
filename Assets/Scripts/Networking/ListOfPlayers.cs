using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public class SyncNick : SyncList<string> { }

/*[System.Serializable]
public class SyncNetworkIdentity : SyncList<NetworkIdentityMessage> { }*/

/*[System.Serializable] 
public class SyncPlayerStats : SyncList<NetworkPlayerStats> { }*/

public class ListOfPlayers : NetworkBehaviour
{
    [SyncVar]
    public int playersConnected = 0;

    public SyncNick Nick;

    public List<NetworkIdentity> NIS = new List<NetworkIdentity>();

    private void Start()
    {

    }

    [ServerCallback]
    private void Update()
    {
        if (isServer)
        {
            playersConnected = CustomNetworkManager.singleton.numPlayers;

            for (int i = 0; i < NetworkServer.connections.Count; i++)
            {
                if (!NIS.Contains(NetworkServer.connections[i].identity) && NetworkServer.connections[i].identity != null)
                {
                    NIS.Add(NetworkServer.connections[i].identity);
                }
            }

            for (int x = 0; x < NIS.Count; x++)
            {
                if (NIS[x] != null)
                {
                    if (NIS[x].GetComponent<PlayerStats>() != null)
                    {
                        string _n = NIS[x].GetComponent<PlayerStats>().Nick;
                        if (!Nick.Contains(_n) && !string.IsNullOrEmpty(_n))
                        {
                            Nick.Add(_n);
                        }
                    }
                }
                else
                {
                    NIS.RemoveAt(x);
                }
            }
        }
    }
}