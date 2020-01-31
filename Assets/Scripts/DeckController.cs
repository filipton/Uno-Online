using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeckController : NetworkBehaviour
{
    [System.Serializable]
    public struct _AllCards
    {
        public int Times;
        public Card[] cards;
    }

    public List<Card> DeckCards = new List<Card>();

    public List<_AllCards> AllCards = new List<_AllCards>();

    public TableDeckController TBC;

    [ServerCallback]
    void Start()
    {
        StartCoroutine(CardsGetter());
    }

    [ServerCallback]
    public IEnumerator CardsGetter()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitForEndOfFrame();
        //tasowanie

        foreach (_AllCards allCards in AllCards)
        {
            for (int i = 0; i < allCards.Times; i++)
            {
                foreach (Card c in allCards.cards)
                {
                    DeckCards.Insert(Random.Range(0, (DeckCards.Count + 1)), c);
                }
            }
        }

        //rozdanie
        for (int o = 0; o < 7; o++)
        {
            //gracze
            print(NetworkServer.connections.Count);
            for(int oi = 0; oi < NetworkServer.connections.Count; oi++)
            {
                NetworkServer.connections[oi].identity.GetComponent<PlayerInventory>().TargetRpcAddCard(NetworkServer.connections[oi], DeckCards[0].cardType, DeckCards[0].cardColor);
                //FindObjectOfType<TableDeckController>().GetComponent<NetworkIdentity>().AssignClientAuthority(NetworkServer.connections[oi]);
                //print($"DODANE KARTE GRACZOWI: {inv.GetComponent<PlayerStats>().Nick}, O KOLORZE: {DeckCards[0].cardColor}, I TYPIE: {DeckCards[0].cardType}");
                DeckCards.RemoveAt(0);
            }
        }


        //tabel deck first card
        FindObjectOfType<PlayerInventory>().NonCmdAddToDeck(DeckCards[0].cardType, DeckCards[0].cardColor);
        DeckCards.RemoveAt(0);

        NetworkServer.connections[0].identity.GetComponent<TurnController>().RpcMyTurn(true, false);
    }

    [Server]
    public void AddCardToPlayer(NetworkIdentity id)
    {
        bool needcards = false;
        if(DeckCards.Count <= 5)
        {
            needcards = true;
        }
        if (needcards)
        {
            print(TBC.TableDeckCards.Count);
            print(TBC.TableDeckCards.Count - 1);
            /*for (int i = 0; i < (TBC.TableDeckCards.Count - 1); i++)
            {
                int x = (TBC.TableDeckCards.Count - 1);
                print("XDD");
                DeckCards.Insert(Random.Range(5, (DeckCards.Count + 1)), TBC.cardList.GetCard(TBC.TableDeckCards[x].cardColor, TBC.TableDeckCards[x].cardType));
                TBC.TableDeckCards.RemoveAt(x);
            }*/

            int w = TBC.TableDeckCards.Count - 1;
            while (w > 0)
            {
                print("XDD");
                DeckCards.Insert(Random.Range(5, (DeckCards.Count + 1)), TBC.cardList.GetCard(TBC.TableDeckCards[w].cardColor, TBC.TableDeckCards[w].cardType));
                TBC.TableDeckCards.RemoveAt(w);

                w--;
            }
        }


        //add cards to player
        for (int oi = 0; oi < NetworkServer.connections.Count; oi++)
        {
            if (NetworkServer.connections[oi].identity == id)
            {
                NetworkServer.connections[oi].identity.GetComponent<PlayerInventory>().TargetRpcAddCard(NetworkServer.connections[oi], DeckCards[0].cardType, DeckCards[0].cardColor);
                DeckCards.RemoveAt(0);
            }
        }
    }

    [ServerCallback]
    public void AddCardsToDeck(List<Card> cards)
    {
        AddCardsToDeck(cards.ToArray());
    }

    [ServerCallback]
    public void AddCardsToDeck(Card[] cards)
    {
        foreach (Card c in cards)
        {
            DeckCards.Insert(Random.Range(0, (DeckCards.Count + 1)), c);
        }
    }

    //button
    /*public void GetCardFromDeck()
    {
        print("XDDD123");
        foreach (PlayerInventory inv in FindObjectsOfType<PlayerInventory>())
        {
            if (inv.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (NetworkServer.active)
                    inv.TargetRpcAddCard(inv.GetComponent<NetworkIdentity>().connectionToClient, DeckCards[0].cardType, DeckCards[0].cardColor);
                else
                    inv.CmdAddCard(inv.GetComponent<NetworkIdentity>());

                DeckCards.RemoveAt(0);
            }
        }

        /*if(isLocalPlayer)
        {
            FindObjectOfType<PlayerInventory>().TargetRpcAddCard(FindObjectOfType<PlayerInventory>().GetComponent<NetworkIdentity>().connectionToClient, DeckCards[0].cardType, DeckCards[0].cardColor);
            DeckCards.RemoveAt(0);
        }
    }*/
}