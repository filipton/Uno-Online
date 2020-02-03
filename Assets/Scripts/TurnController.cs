using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class TurnController : NetworkBehaviour
{
    [SyncVar]
    public bool MyTurn;

    public bool TurnStarted;

    public float TurnTime;

    public float MaxTurnTime = 10;

    [Header("SERVER FIELD")]
    public List<NetworkIdentity> PlayersTurn = new List<NetworkIdentity>();

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
        TableDeckCard actualcard = FindObjectOfType<TableDeckController>().GetFirstCard();
        if (actualcard.Active && actualcard.cardType == CardType.PlusFour)
        {
            for(int i = 0; i < 4; i++)
            {
                if(isLocalPlayer)
                    FindObjectOfType<GetDeckCards>().GetCardButton();

                yield return new WaitForSeconds(0.250f);
            }

            FindObjectOfType<TableDeckController>().ChangeFirst(actualcard.FakeColor, false);
            CmdNextPlayer(CardType.Three);
        }

        while(TurnTime < MaxTurnTime)
        {
            TurnTime += Time.deltaTime;

            yield return null;
        }

        if(TurnTime >= MaxTurnTime)
        {
            //use card or draw

            CmdNextPlayer(CardType.Three);
        }
    }

    [ClientRpc]
    public void RpcMyTurn(bool turnBool, bool force)
    {
        if (isLocalPlayer)
        {
            MyTurn = turnBool;

            if(force && MyTurn)
            {
                StopAllCoroutines();
                TurnTime = 0;
                StartCoroutine(MyTurnCortine());
            }

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
        RpcMyTurn(turnBool, false);
    }

    [Command]
    public void CmdNextPlayer(CardType type)
    {
        if(PlayersTurn.Count == 0)
        {
            foreach(NetworkConnectionToClient conn in NetworkServer.connections.Values)
            {
                PlayersTurn.Add(conn.identity);
            }
        }

        int countPlayer = 1;
        if (type == CardType.Stop)
        {
            countPlayer++;
        }
        else if (type == CardType.Reverse)
        {
            PlayersTurn.Reverse();

            if (PlayersTurn.Count == 2)
            {
                for (int i = 0; i < PlayersTurn.Count; i++)
                {
                    if (PlayersTurn[i].GetComponent<TurnController>().MyTurn)
                    {
                        PlayersTurn[i].GetComponent<TurnController>().MyTurn = true;
                        PlayersTurn[i].GetComponent<TurnController>().RpcMyTurn(true, true);
                        return;
                    }
                }
            }
        }


        for(int i = 0; i < PlayersTurn.Count; i++)
        {
            if (PlayersTurn[i].GetComponent<TurnController>().MyTurn)
            {
                int nextPlayer = i + countPlayer;
                if (nextPlayer >= PlayersTurn.Count)
                {
                    nextPlayer = 0 + (nextPlayer - PlayersTurn.Count);
                    //nextPlayer = 0 + (PlayersTurn.Count - countPlayer);
                    print($"FDSGHJFGDSHKJFGDSHJKFDESGHJKGFHDSJFGDSHJGFDSHJGFDHSJGFHSJGFHDSJKGFDSHJKDSHJ DSHJGFHDSJKGFHJDSGFJHDSGFHJDSJFDS: {nextPlayer}");
                }

                if(i == nextPlayer)
                {
                    PlayersTurn[i].GetComponent<TurnController>().MyTurn = true;
                    PlayersTurn[i].GetComponent<TurnController>().RpcMyTurn(true, true);
                }
                else
                {
                    PlayersTurn[i].GetComponent<TurnController>().MyTurn = false;
                    PlayersTurn[i].GetComponent<TurnController>().RpcMyTurn(false, false);

                    PlayersTurn[nextPlayer].GetComponent<TurnController>().MyTurn = true;
                    PlayersTurn[nextPlayer].GetComponent<TurnController>().RpcMyTurn(true, false);
                }
                break;
            }
        }
    }
}