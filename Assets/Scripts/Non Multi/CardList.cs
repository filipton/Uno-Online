using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public Card GetCard(CardColor color, CardType type)
    {
        return cards.Find(x => x.cardColor == color && x.cardType == type);
    }
    public Card GetCard(CardType type, CardColor color)
    {
        return cards.Find(x => x.cardColor == color && x.cardType == type);
    }
}