using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardDisplay : NetworkBehaviour
{
    public Card card;
    public RawImage image;

    public PlayerInventory playerInventory;
    public TurnController TC;

    void Start()
    {
        //print(card.cardColor + " : " + card.cardType);
        //GetComponent<RawImage>().texture = card.ImageTexture();
        
        foreach(PlayerInventory inv in FindObjectsOfType<PlayerInventory>())
        {
            if(inv.isLocalPlayer)
            {
                playerInventory = inv;
                TC = inv.GetComponent<TurnController>();
            }
        }
    }

    public void ChangeCard(Card c)
    {
        card = c;
        //print(card.cardColor + " : " + card.cardType);
        image.texture = card.ImageTexture();
    }

    public void OnClick()
    {
        if(TC.MyTurn)
        {
            //print(card.cardColor + " : " + card.cardType);
            if (FindObjectOfType<CardController>().CheckCard(card))
            {
                playerInventory.CmdUseCard(card.cardType, card.cardColor);
                TC.CmdNextPlayer();

                Destroy(this.gameObject);
            }
        }
    }
}