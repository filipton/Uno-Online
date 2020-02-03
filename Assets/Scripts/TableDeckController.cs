using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public struct TableDeckCard
{
    //public Card DeckCard;
    public CardColor cardColor;
    public CardType cardType;

    public bool Active;
    public CardColor FakeColor;

    public TableDeckCard(CardColor cc, CardType ct, bool a, CardColor FC)
    {
        //DeckCard = c;
        cardColor = cc;
        cardType = ct;
        Active = a;
        FakeColor = FC;
    }
}

[System.Serializable]
public class SyncTableDeckCard : SyncList<TableDeckCard> { }

public class TableDeckController : NetworkBehaviour
{
    //public List<TableDeckCard> TableDeckCards = new List<TableDeckCard>(); 

    public CardList cardList;

    public CardDisplay TableDeckDisplay;

    public SyncTableDeckCard TableDeckCards;

    // Start is called before the first frame update
    void Start()
    {
        cardList = FindObjectOfType<CardList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFirst(CardColor color, bool active = true)
    {
        TableDeckCards[TableDeckCards.Count - 1] = new TableDeckCard(TableDeckCards[TableDeckCards.Count - 1].cardColor, TableDeckCards[TableDeckCards.Count - 1].cardType, active, color);
    }

    public CardColor GetFakeColor()
    {
        return TableDeckCards[TableDeckCards.Count - 1].FakeColor;
    }

    public TableDeckCard GetFirstCard()
    {
        return TableDeckCards[TableDeckCards.Count - 1];
    }
}