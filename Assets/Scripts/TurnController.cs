using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnController : NetworkBehaviour
{
    [SyncVar]
    public bool MyTurn;

    public bool TurnStarted;

    public float TurnTime;

    public float MaxTurnTime = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MyTurnCortine()
    {
        while(TurnTime < MaxTurnTime)
        {
            TurnTime += Time.deltaTime;

            yield return null;
        }

        if(TurnTime >= MaxTurnTime)
        {
            CmdNextPlayer();
        }
    }

    [ClientRpc]
    public void RpcMyTurn(bool turnBool)
    {
        if (isLocalPlayer)
        {
            MyTurn = turnBool;
            if (MyTurn && !TurnStarted)
            {
                StartCoroutine(MyTurnCortine());
            }
            else if (!MyTurn)
            {
                TurnTime = 0;
                StopAllCoroutines();
            }
        }
    }

    [Command]
    public void CmdMyTurn(bool turnBool)
    {
        MyTurn = turnBool;
        RpcMyTurn(turnBool);
    }

    [Command]
    public void CmdNextPlayer()
    {
        for(int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (NetworkServer.connections[i].identity.GetComponent<TurnController>().MyTurn)
            {
                int nextPlayer = i + 1;
                print($"NEXT PLAYER IS: {nextPlayer} BUT CONNECTIONS COUNT IS: {NetworkServer.connections.Count}");
                if (nextPlayer >= NetworkServer.connections.Count)
                {
                    nextPlayer = 0;
                }

                NetworkServer.connections[i].identity.GetComponent<TurnController>().MyTurn = false;
                NetworkServer.connections[i].identity.GetComponent<TurnController>().RpcMyTurn(false);

                NetworkServer.connections[nextPlayer].identity.GetComponent<TurnController>().MyTurn = true;
                NetworkServer.connections[nextPlayer].identity.GetComponent<TurnController>().RpcMyTurn(true);
                break;
            }
        }
    }
}