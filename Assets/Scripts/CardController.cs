using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public string Color;
    public TableDeckController TableDeckController;

    public CardList cardList;

    // Start is called before the first frame update
    void Start()
    {
        cardList = FindObjectOfType<CardList>();
        TableDeckController = FindObjectOfType<TableDeckController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(string color)
    {
        Color = color;
    }

    IEnumerator ColorChanger()
    {
        while(string.IsNullOrWhiteSpace(Color))
        {
            yield return new WaitForEndOfFrame();
        }

        if(!string.IsNullOrWhiteSpace(Color))
        {
            CardColor colorr = CardColor.Red;

            if (Color == "red")
                colorr = CardColor.Red;
            if (Color == "green")
                colorr = CardColor.Green;
            if (Color == "blue")
                colorr = CardColor.Blue;
            if (Color == "yellow")
                colorr = CardColor.Yellow;

            TableDeckController.ChangeFirst(colorr, false);
        }

        Color = string.Empty;
    }

    public bool CheckCard(Card cardToPut)
    {
        Card cardOnTable = cardList.GetCard(TableDeckController.TableDeckCards[TableDeckController.TableDeckCards.Count - 1].cardColor, TableDeckController.TableDeckCards[TableDeckController.TableDeckCards.Count - 1].cardType);

        if(cardOnTable.cardType == CardType.PlusFour || cardOnTable.cardType == CardType.ChangeColor)
        {
            if(TableDeckController.GetFakeColor() != CardColor.All)
            {
                if (TableDeckController.GetFakeColor() == cardToPut.cardColor)
                {
                    return true;
                }
            }
        }

        if (cardOnTable.cardColor == cardToPut.cardColor)
        {
            return true;
        }

        if (cardOnTable.cardType == cardToPut.cardType)
        {
            return true;
        }

        if (cardToPut.cardType == CardType.PlusFour && cardOnTable.cardType != CardType.PlusTwo)
        {
            StartCoroutine(ColorChanger());
            return true;
        }

        if (cardToPut.cardType == CardType.ChangeColor)
        {
            if (FindObjectOfType<TableDeckController>().TableDeckCards[FindObjectOfType<TableDeckController>().TableDeckCards.Count - 1].Active && cardOnTable.cardType == CardType.PlusTwo || cardOnTable.cardType == CardType.PlusFour && FindObjectOfType<TableDeckController>().TableDeckCards[FindObjectOfType<TableDeckController>().TableDeckCards.Count - 1].Active)
            {
                return false;
            }

            StartCoroutine(ColorChanger());
            return true;
        }


        return false;
    }
}