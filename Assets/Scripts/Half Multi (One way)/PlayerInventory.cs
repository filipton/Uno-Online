using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class PlayerInventory : NetworkBehaviour
{
    public List<Card> cards = new List<Card>();

    [SyncVar]
    public int CardsAmount;

    public GameObject Parent;
    public GameObject CardPrefab;

    public TableDeckController tableDeck;
    public DeckController deckController;

    public CardController cardController;
    public CardList cardList;

    // Start is called before the first frame update
    void Start()
    {
        cardController = FindObjectOfType<CardController>();
        cardList = FindObjectOfType<CardList>();
        tableDeck = FindObjectOfType<TableDeckController>();
        deckController = FindObjectOfType<DeckController>();

        Parent = GameObject.FindGameObjectWithTag("CardsParrent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    [TargetRpc]
    public void TargetRpcAddAuth(NetworkConnection conn)
    {
        GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
    }

    [Command]
    public void CmdAddCard(NetworkIdentity id)
    {
        if(deckController == null)
            deckController = FindObjectOfType<DeckController>();

        deckController.AddCardToPlayer(id);
    }

    [TargetRpc]
    public void TargetRpcAddCard(NetworkConnection conn, CardType cardType, CardColor cardColor)
    {
        Card card = cardList.GetCard(cardColor, cardType);
        cards.Add(card);
        GameObject c = Instantiate(CardPrefab, Parent.transform);
        c.GetComponent<CardDisplay>().ChangeCard(card);
        c.transform.localScale = new Vector3(0.6f, 0.6f);
        CmdAddCardsAmount(cards.Count);
    }

    [TargetRpc]
    public void TargetRpcCostam(NetworkConnection conn, string msg)
    {
        print(msg);
    }


    [Command]
    public void CmdUseCard(CardType cardType, CardColor cardColor)
    {
        if(cardController.CheckCard(cardList.GetCard(cardColor, cardType)))
        {
            RpcUseCard(cardType, cardColor);
        }
    }

    [ClientRpc]
    public void RpcUseCard(CardType cardType, CardColor cardColor)
    {
        if(isLocalPlayer)
        {
            if (cardController.CheckCard(cardList.GetCard(cardColor, cardType)))
            {
                //add to deck
                print("usuwanie z decka");
                cards.Remove(cardList.GetCard(cardColor, cardType));
                CmdAddToDeck(cardType, cardColor);
                CmdAddCardsAmount(cards.Count);
            }
        }
    }

    [Command]
    public void CmdAddCardsAmount(int amount)
    {
        CardsAmount = amount;
        RpcAddCardsAmount(amount);
        if (amount == 0)
        {
            print(1);
        }
    }

    [ClientRpc]
    public void RpcAddCardsAmount(int amount)
    {
        CardsAmount = amount;
    }



    [Command]
    public void CmdAddToDeck(CardType cardType, CardColor cardColor)
    {
        tableDeck.TableDeckCards.Add(new TableDeckCard(cardColor, cardType, true, cardColor));
        RpcAddToDeck(cardType, cardColor);
    }

    public void NonCmdAddToDeck(CardType cardType, CardColor cardColor)
    {
        tableDeck.TableDeckCards.Add(new TableDeckCard(cardColor, cardType, true, cardColor));
        RpcAddToDeck(cardType, cardColor);
    }

    [ClientRpc]
    public void RpcAddToDeck(CardType cardType, CardColor cardColor)
    {
        Card c = cardList.GetCard(cardColor, cardType);
        tableDeck.TableDeckDisplay.card = c;
        tableDeck.TableDeckDisplay.image.texture = c.ImageTexture();
    }
}
