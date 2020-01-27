using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStats : NetworkBehaviour
{
    public CustomNetworkManager CNM;

    [SyncVar]
    public string Nick;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            if (CNM == null)
                CNM = FindObjectOfType<CustomNetworkManager>();

            CmdSetNick(CNM.Nick);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdSetNick(string nick)
    {
        Nick = nick;
        RpcSetNick(nick);
    }

    [ClientRpc]
    public void RpcSetNick(string nick)
    {
        Nick = nick;
    }
}